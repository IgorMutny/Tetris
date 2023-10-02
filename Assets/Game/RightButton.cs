using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightButton : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;

    private void OnMouseDown()
    {
        keyboardController.RightButtonDown();
    }
}
