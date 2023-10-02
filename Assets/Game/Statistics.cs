using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{
    private string hiScoreKey = "HiScore";

    private int hiScore = 0;
    private int score = 0;
    private int filledLines = 0;
    private int level = 1;

    private int[] scoreForFilledLines = new int[] { 0, 100, 300, 700, 1200 };

    public Action<int> LevelUp;
    public Action<int, int, int, int> StatsChanged;

    private void Start()
    {
        GetComponent<FixedCells>().LinesFilled += LinesFilledHandle;

        if (PlayerPrefs.HasKey(hiScoreKey))
        {
            hiScore = PlayerPrefs.GetInt(hiScoreKey, 0);
        }

        StatsChanged?.Invoke(hiScore, score, filledLines, level);
    }

    public void NewGameHandle()
    {
        filledLines = 0;
        level = 1;
        score = 0;
        StatsChanged?.Invoke(hiScore, score, filledLines, level);
    }

    private void LinesFilledHandle(int lines)
    {
        filledLines += lines;
        score += scoreForFilledLines[lines];

        if (score > hiScore)
        {
            hiScore = score;
            SaveHiScore();
        }

        if (filledLines / 10 > level - 1)
        {
            level = 1 + filledLines / 10;
            LevelUp?.Invoke(level);
        }

        StatsChanged?.Invoke(hiScore, score, filledLines, level);
    }

    private void SaveHiScore()
    {
        PlayerPrefs.SetInt(hiScoreKey, hiScore);
    }
}
