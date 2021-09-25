using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private static int highScore;    
    public Text highScoreText;
    public Text currentScoreText;

    private void Update()
    {
        highScoreText.text = ScoreManager.Instance.HighScore.ToString();
        currentScoreText.text = ScoreManager.Instance.CurrentScore.ToString();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    #region Singleton

    private static ScoreManager _instance = null;

    public static ScoreManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ScoreManager>();

                if (_instance == null)
                {
                    Debug.LogError("Fatal Error: ScoreManager not Found");
                }
            }

            return _instance;
        }
    }

    #endregion

    public int tileRatio;
    public int comboRatio;

    public int HighScore { get { return highScore; } }
    public int CurrentScore { get { return currentScore; } }

    private int currentScore;

    private void Start()
    {
        ResetCurrentScore();
    }

    public void ResetCurrentScore()
    {
        currentScore = 0;
    }

    public void IncrementCurrentScore(int tileCount, int comboCount)
    {
        currentScore += (tileCount * tileRatio) * (comboCount * comboRatio);
        SoundManager.Instance.PlayScore(comboCount > 1);
    }

    public void SetHighScore()
    {
        if(currentScore > highScore){
            highScore = currentScore;
        }
    }
}
