using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardController : MonoBehaviour
{
    public Action<int> Move;
    public Action Turn;
    public Action Drop;
    public Action Pause;

    private KeyCode left;
    private KeyCode right;
    private KeyCode turn;
    private KeyCode drop;
    private KeyCode pause;

    private float dropDelay = 0.1f;

    private bool gameIsOn = false;
    private bool dropButtonDragged = false;

    public void Init()
    {
        left = KeyCode.LeftArrow;
        right = KeyCode.RightArrow;
        turn = KeyCode.UpArrow;
        drop = KeyCode.DownArrow;
        pause = KeyCode.Space;

        GetComponent<FigureController>().GameOver += () => gameIsOn = false;
    }

    public void NewGameHandle()
    {
        gameIsOn = true;
    }

    private void Update()
    {
        if (gameIsOn == true)
        {
            if (Input.GetKeyDown(left) == true) Move?.Invoke(-1);
            if (Input.GetKeyDown(right) == true) Move?.Invoke(1);
            if (Input.GetKeyDown(turn) == true) Turn?.Invoke();
            if (Input.GetKeyDown(drop) == true) HandleDrop();

            if (Input.GetKeyDown(pause) == true) Pause?.Invoke();
        }
    }

    private void HandleDrop()
    {
        if (Input.GetKey(drop) == true && gameIsOn == true)
        {
            Drop?.Invoke();
            Invoke(nameof(HandleDrop), dropDelay);
        }
    }

    public void PauseButtonDown()
    {
        if (gameIsOn == true)
        {
            Pause?.Invoke();
        }
    }

    public void LeftButtonDown()
    {
        if (gameIsOn == true)
        {
            Move?.Invoke(-1);
        }
    }

    public void RightButtonDown()
    {
        if (gameIsOn == true)
        {
            Move?.Invoke(1);
        }
    }

    public void TurnButtonDown()
    {
        if (gameIsOn == true)
        {
            Turn?.Invoke();
        }
    }

    public void DropButtonDrag()
    {
        if (gameIsOn == true && dropButtonDragged == true)
        {
            Drop?.Invoke();
            Invoke(nameof(DropButtonDrag), dropDelay);
        }
    }

    public void DropButtonDown()
    {
        if (gameIsOn == true)
        {
            dropButtonDragged = true;
            DropButtonDrag();
        }
    }

    public void DropButtonUp()
    {
        if (gameIsOn == true)
        {
            dropButtonDragged = false;
        }
    }
}
