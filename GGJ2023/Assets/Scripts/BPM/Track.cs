using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Track
{
    const float secondsInAMinute = 60f;
    float bpm = 60;
    List<JudgmentZone> judgmentZoneList;
    int totalBeats = 0;
    public Track(float bpm, List<int> judgmentList, int totalBeats)
    {
        this.bpm = bpm;
        var judgmentZoneInterval = secondsInAMinute / this.bpm;
        InitJudgmentZoneList(judgmentZoneInterval, judgmentList);
        this.totalBeats = totalBeats;
    }

    void InitJudgmentZoneList(float interval, List<int> judgmentList)
    {
        judgmentZoneList = new List<JudgmentZone>();
        for (int i = 0; i < judgmentList.Count; i++)
        {
            var startTime = judgmentList[i] * interval;
            JudgmentZone zone = new()
            {
                CenterPercent = 0.9f,
                EarlierRangePercent = 0.1f,
                LaterRangePercent = 0.1f,
                StartTime = startTime,
                Interval= interval,
            };
            judgmentZoneList.Add(zone);
        }
    }
    public bool GetJudgment(float time)
    {
        JudgmentZone zone = judgmentZoneList.Find(e => e.IsTimeInJudgmentZone(time));
        if (zone == null) {
            return false;
        }
        return zone.GetJudgmentResult(time);
    }
}
