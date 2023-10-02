using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Action ButtonPressed;

    private void OnMouseDown()
    {
        ButtonPressed?.Invoke();
        Application.Quit();
    }
}
