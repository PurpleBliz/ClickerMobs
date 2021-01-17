using UnityEngine;
using UnityEngine.UI;


public class DrawUI : MonoBehaviour
{
    public GameHandler GameHandler;

    private Text _lidersText => GameHandler.LidersText;
    private Text _lastText => GameHandler.LastText;
    private Text _levelText => GameHandler.LevelText;
    private Text _healthText => GameHandler.HealthText;
    private Image _healthBar => GameHandler.HealthBar;
    private int _enemyType => GameHandler.EnemyType.Length;

    public void DrawLiders(Liders[] liders)
    {
        _lidersText.text = "";

        if (liders != null)
        {
            foreach (var lider in liders)
            {
                _lidersText.text += $"{lider.name}: {lider.score}\r\n";
            }
        }
        else _lidersText.text += "Not found";
    }

    public void DrawUpUI(float currentHP, float startHP, int level)
    {
        float persent = currentHP / startHP * 100;
        
        _healthText.text = $"{persent.ToString("0")}%";
        _healthBar.fillAmount = persent / 100;
        
        _levelText.text = $"Level:{level}";
        _lastText.text = $"Last:{_enemyType - level}";
    }
}