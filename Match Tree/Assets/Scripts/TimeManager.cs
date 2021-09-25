using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public Text timeText;

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private string GetTimeString(float timeRemaining)
    {
        int minute = Mathf.FloorToInt(timeRemaining / 60);
        int second = Mathf.FloorToInt(timeRemaining % 60);

        return string.Format("{0} : {1}", minute.ToString(), second.ToString());
    }

    #region Singleton

    private static TimeManager _instance = null;

    public static TimeManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<TimeManager>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: TimeManager not Found");
                }
            }

            return _instance;
        }
    }

    #endregion

    public int duration;

    private float time;

    private void Start()
    {
        time = 0;
    }

    private void Update()
    {
        if (GameFlowManager.Instance.IsGameOver)
        {
            return;
        }

        if (time > duration)
        {
            GameFlowManager.Instance.GameOver();
            return;
        }

        time += Time.deltaTime;
        timeText.text = GetTimeString(TimeManager.Instance.GetRemainingTime() + 1);
    }

    public float GetRemainingTime()
    {
        return duration - time;
    }
}
