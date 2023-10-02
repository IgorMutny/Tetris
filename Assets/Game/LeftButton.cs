using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftButton : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;

    private void OnMouseDown()
    {
        keyboardController.LeftButtonDown();
    }

}
