using System;
using System.Collections;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    [Space(10)] [Header("UI Elements")] public Text HealthText;
    public Text LevelText;
    public Text LastText;
    public Text LidersText;
    public Text BestRecordText;
    public Text CurrentRecordText;
    public Image HealthBar;
    public GameObject WinNotyfy;
    public GameObject DefeatNotyfy;
    [Space(10)] [Header("Game Elements")] public int Damage;
    public GameObject EnemyGameObject;
    public EnemyType[] EnemyType;
    public TimeHandler TimeHandle;
    [Space(10)] [Header("Setting")] public float TimeGame;
    public string URL;
    
    private NetWorking _netWorking;
    private Notyfy _notyfy;
    private DrawUI _drawUI;
    
    private Enemy _enemy;
    private EnemyHandler _enemyHandler;
    private RewardAD _rewardAD;
    private Animator _animatorDefeatNotyfy;
    private Animator _animatorWinNotyfy;
    private Animator _animatorWinNot;
    private Request _request;
    private float _startHP;
    private float _currentHP;
    private Liders[] _liders;
    private int Best = 0;
    private int CurrentRec;

    private void Start()
    {
        LoadComponents();
        SetEnemy(0);
    }

    private void LoadComponents()
    {
        _netWorking = new NetWorking();
        _notyfy = new Notyfy();
        _drawUI = new DrawUI();

        _drawUI.GameHandler = this;
        _enemy = EnemyGameObject.GetComponent<Enemy>();
        _enemyHandler = EnemyGameObject.GetComponent<EnemyHandler>();
        _animatorDefeatNotyfy = DefeatNotyfy.GetComponent<Animator>();
        _animatorWinNotyfy = WinNotyfy.GetComponent<Animator>();
        _rewardAD = GetComponent<RewardAD>();
        _enemyHandler.Damage = Damage;
        TimeHandle.Power(TimeGame);
        
        _enemyHandler.Notify += ChangeHealth;
        TimeHandle.OnChanges += SendNotifyEndTime;
        _rewardAD.Notify += Reward;
        
        if (PlayerPrefs.HasKey("Record")) Best = PlayerPrefs.GetInt("Record");
    }

    private void SetEnemy(float time)
    {
        if (_enemy.Level == EnemyType.Length)
        {
            SendNotifyWin();
            return;
        }

        LoadEnemy(_enemy.Level);
        StartCoroutine(ActivateUI(time));
    }

    private bool GetJsonObject(string URL)
    {
        try
        {
            _liders = _netWorking.GetLidersFromJson(URL);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void LoadEnemy(int startLevel)
    {
        _enemy.Health = EnemyType[startLevel].Health;
        _enemy.Level++;
        _startHP = _enemy.Health;
        _currentHP = _startHP;
        
        _drawUI.DrawUpUI(_currentHP, _startHP, _enemy.Level);
    }

    private IEnumerator ActivateUI(float time)
    {
        yield return new WaitForSeconds(time);
        _enemyHandler.ShowAnimation();
    }

    private void ChangeHealth(int Health)
    {
        if (Health <= 0)
        {
            SetEnemy(0.3f);
            return;
        }
        
        _currentHP = Health;

        _drawUI.DrawUpUI(_currentHP, _startHP, _enemy.Level);
    }

    private void Reward()
    {
        TimeHandle.Power(TimeGame / 2);
        _enemyHandler.ChangeBoxCollider(true);
        _animatorDefeatNotyfy.SetBool("Close", true);
    }

    private void SendNotifyEndTime()
    {
        _notyfy.Send(_enemyHandler,_animatorDefeatNotyfy);
    }

    private void SendNotifyWin()
    {
        _notyfy.Send(_enemyHandler,_animatorWinNotyfy);
        _liders = _netWorking.GetJsonObject(URL);
        _drawUI.DrawLiders(_liders);
        TimeHandle.Pause();
        CurrentRec = (int) TimeHandle.AllSeconds;
        if (Best > (int) TimeHandle.AllSeconds)
        {
            Best = (int) TimeHandle.AllSeconds;
            PlayerPrefs.SetInt("Record", (int) TimeHandle.AllSeconds);
        }
        BestRecordText.text = $"Best Time:{Best}";
        CurrentRecordText.text = $"Current Time:{CurrentRec}";
    }

    public void Restart()
    {
        if (!_animatorDefeatNotyfy.GetBool("Close"))
        {
            _animatorDefeatNotyfy.SetBool("Close", true);
        }

        if (!_animatorWinNotyfy.GetBool("Close"))
        {
            _animatorWinNotyfy.SetBool("Close", true);
        }
        _enemy.Level = 0;
        LoadEnemy(_enemy.Level);
        StartCoroutine(ActivateUI(1f));
        TimeHandle.Power(TimeGame);
    }
    
    private void OnDisable()
    {
        _enemyHandler.Notify -= ChangeHealth;
        TimeHandle.OnChanges -= SendNotifyEndTime;
        _rewardAD.Notify -= Reward;
    }
}