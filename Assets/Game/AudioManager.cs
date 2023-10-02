using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private FigureController figure;
    [SerializeField] private FixedCells fixedCells;
    [SerializeField] private Statistics statistics;
    [SerializeField] private NewGameButton newGameButton;
    [SerializeField] private ExitButton exitButton;

    [SerializeField] private AudioClip figureFixed;
    [SerializeField] private AudioClip linesFilled;
    [SerializeField] private AudioClip levelUp;
    [SerializeField] private AudioClip gameOver;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        figure.FigureFixed += FigureFixed;
        figure.GameOver += GameOver;
        fixedCells.LinesFilled += LinesFilled;
        statistics.LevelUp += LevelUp;
        newGameButton.ButtonPressed += FigureFixed;
        exitButton.ButtonPressed += FigureFixed;
    }

    private void FigureFixed()
    {
        audioSource.clip = figureFixed;
        audioSource.Play();
    }

    private void LinesFilled(int lines)
    {
        audioSource.clip = linesFilled;
        audioSource.Play();
    }

    private void LevelUp(int level)
    {
        audioSource.clip = levelUp;
        audioSource.Play();
    }

    private void GameOver()
    {
        audioSource.clip = gameOver;
        audioSource.Play();
    }
}
