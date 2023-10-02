using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGameButton : MonoBehaviour
{
    [SerializeField] private EntryPoint entryPoint;

    public Action ButtonPressed;

    private void OnMouseDown()
    {
        ButtonPressed?.Invoke();
        entryPoint.NewGameHandle();
    }
}
