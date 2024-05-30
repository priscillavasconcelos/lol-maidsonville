using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class TimeWatcher : MonoBehaviour
{
    [SerializeField] TimeManager timeManager;
    [SerializeField] List<Schedule> schedules = new List<Schedule>();

    private void Start()
    {
        timeManager.OnTimeChanged += CheckSchedule;
    }

    private void OnDestroy()
    {
        timeManager.OnTimeChanged -= CheckSchedule;
    }

    private void CheckSchedule(object sender, TimeSpan newTime)
    {
        var schedule = schedules.FirstOrDefault(s => s.day == timeManager.day && s.hour == newTime.Hours && s.minute == newTime.Minutes);

        schedule?.action?.Initialize();
    }

    [Serializable]
    private class Schedule
    {
        public string eventName;
        public int day;
        public int hour;
        public int minute;
        public ActionSO action;
    }
}
