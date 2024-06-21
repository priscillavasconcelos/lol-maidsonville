using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] TopHudView topHudView;

    public int hour = 12;
    public int day;

    public int secondsPerDay = 86400; // Número de segundos em um dia do jogo
    public float timeMultiplier = 1f; // Velocidade do tempo do jogo em relação ao tempo real

    private TimeSpan currentTime;
    private float accumulatedTime = 0f;

    public bool timeRunning = false;

    // Evento único para o tempo
    public event EventHandler<TimeSpan> OnTimeChanged;

    void Start()
    {
        currentTime = new TimeSpan(hour, 0, 0);

        topHudView.UpdateTimer(GetFormattedTime());
    }

    void Update()
    {
        if(GameManager.Instance != null)
        {
            if (!GameManager.Instance.resultsScreen.activeInHierarchy)
            {
                if (!timeRunning)
                    return;

                accumulatedTime += Time.deltaTime * timeMultiplier;

                while (accumulatedTime >= 1f)
                {
                    accumulatedTime -= 1f;

                    currentTime += TimeSpan.FromMinutes(5);

                    OnTimeChanged?.Invoke(this, currentTime);

                    if (currentTime.TotalSeconds >= secondsPerDay)
                    {
                        currentTime = new TimeSpan(0, 0, 0);
                        OnTimeChanged?.Invoke(this, currentTime); // Trigger event at new day start
                    }

                    topHudView.UpdateTimer(GetFormattedTime());
                }
            }
        }
    }

    public string GetFormattedTime()
    {
        int _day = (int)(currentTime.TotalSeconds / secondsPerDay) + 1;
        day = _day;
        return string.Format("Day {0}, {1:D2}:{2:D2}", _day, currentTime.Hours, currentTime.Minutes);
    }

    public TimeSpan GetCurrentTime()
    {
        return currentTime;
    }
}
