using UnityEngine;
using UnityEngine.UI;

public class TimeHandler : MonoBehaviour
{
    public Text TimeText => GetComponent<Text>();
    
    public delegate void Counter();
    public event Counter OnChanges;
    public float AllSeconds;
    
    private bool StopGame = true;
    private float second;
    private void FixedUpdate()
    {
        if (StopGame) return;
        
        second -= Time.deltaTime;
        AllSeconds += Time.deltaTime;
        TimeText.text = $"TIME:{second.ToString("0")}s";

        if (second <= 0) Stop();
    }

    public void Power(float StartTime)
    {
        StopGame = !StopGame;
        second = StartTime;
        if (AllSeconds < 60) AllSeconds = 0;
    }

    public void Stop()
    {
        StopGame = true;
        OnChanges?.Invoke();
    }

    public void Pause()
    {
        StopGame = !StopGame;
    }
    
    
}