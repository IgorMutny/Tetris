using System;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action Step;

    private float delayTime;
    private float currentDelay = 0;

    private bool gameIsOn = false;
    private bool gameIsPaused = false;

    private void Start()
    {
        GetComponent<FigureController>().GameOver += () => gameIsOn = false;
        GetComponent<Statistics>().LevelUp += SetSpeed;
        GetComponent<KeyboardController>().Pause += () => gameIsPaused = !gameIsPaused;
    }

    public void NewGameHandle()
    {
        gameIsOn = true;
        gameIsPaused = false;
        SetSpeed(1);
    }

    private void Update()
    {
        if (gameIsOn == true && gameIsPaused == false)
        {
            currentDelay -= Time.deltaTime;

            if (currentDelay <= 0)
            {
                currentDelay = delayTime;
                Step?.Invoke();
            }
        }
    }

    private void SetSpeed(int level)
    {
        delayTime = Mathf.Pow(0.8f, level - 1);
        currentDelay = delayTime;
    }
}
