using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject pauseText;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private TextMeshPro hiScoreText;
    [SerializeField] private TextMeshPro scoreText;
    [SerializeField] private TextMeshPro linesText;
    [SerializeField] private TextMeshPro levelText;
    [SerializeField] private GameObject newGameButton;
    [SerializeField] private GameObject exitButton;

    private bool gameIsPaused = false;
    private float gameOverTextDelay = 4f;

    public void Init()
    {
        GetComponent<KeyboardController>().Pause += PauseHandle;
        GetComponent<FigureController>().GameOver += GameOverHandle;
        GetComponent<Statistics>().StatsChanged += SetTexts;

        pauseText.SetActive(false);
        gameOverText.SetActive(false);
    }

    public void NewGameHandle()
    {
        newGameButton.SetActive(false);
        exitButton.SetActive(false);
    }

    private void GameOverHandle()
    {
        gameOverText.SetActive(true);
        Invoke(nameof(GoBackToMainMenu), gameOverTextDelay);
    }

    private void GoBackToMainMenu()
    {
        gameOverText.SetActive(false);
        newGameButton.SetActive(true);
        exitButton.SetActive(true);
    }

    private void PauseHandle()
    {
        gameIsPaused = !gameIsPaused;
        pauseText.SetActive(gameIsPaused);
    }

    private void SetTexts(int hiScore, int score, int lines, int level)
    {
        hiScoreText.text = hiScore.ToString();
        scoreText.text = score.ToString();
        linesText.text = lines.ToString();
        levelText.text = level.ToString();
    }
}
