using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnButton : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;

    private void OnMouseDown()
    {
        keyboardController.TurnButtonDown();
    }
}
