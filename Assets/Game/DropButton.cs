using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropButton : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;

    private void OnMouseDown()
    {
        keyboardController.DropButtonDown();
    }

    private void OnMouseUp()
    {
        keyboardController.DropButtonUp();
    }
}
