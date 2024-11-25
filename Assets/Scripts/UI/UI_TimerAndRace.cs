using UnityEngine;
using UnityEngine.UI;

public class UI_TimerAndRace : Singleton<UI_TimerAndRace>
{
    [SerializeField] private Text RaceNumberText;
    [Space]
    [SerializeField] private Text RaceTimeHoursText;
    [SerializeField] private Text RaceTimeMinutesText;
    [SerializeField] private Text RaceTimeSecondsText;
    [SerializeField] private Text RaceTimeMilisecondsText;

    private void Update()
    {
        if (!RaceManager.Instance.RaceStarted)
            return;

        UpdateRaceNumberText();
        UpdateRaceTimerText();
    }

    public void UpdateRaceTimerText()
    {
        int h = 0, m = 0, s = 0, ms = 0;
        h = (int)RaceManager.Instance.RaceTime / 3600;
        m = (int)RaceManager.Instance.RaceTime / 60;
        s = (int)RaceManager.Instance.RaceTime;
        ms = (int)((RaceManager.Instance.RaceTime - (int)RaceManager.Instance.RaceTime) * 100);

        if (h < 10 && h >= 0)
            RaceTimeHoursText.text = "0" + h.ToString();
        else
            RaceTimeHoursText.text = h.ToString();

        if (m < 10 && m >= 0)
            RaceTimeMinutesText.text = "0" + m.ToString();
        else
            RaceTimeMinutesText.text = m.ToString();

        if (s < 10 && s >= 0)
            RaceTimeSecondsText.text = "0" + s.ToString();
        else
            RaceTimeSecondsText.text = s.ToString();

        if (ms < 10 && ms >= 0)
            RaceTimeMilisecondsText.text = "0" + ms.ToString();
        else
            RaceTimeMilisecondsText.text = ms.ToString();
    }
    public void UpdateRaceNumberText()
    {
        RaceNumberText.text = RaceManager.Instance.RaceNumber.ToString();
    }
}
