using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    [SerializeField] private KeyboardController keyboardController;

    [SerializeField] private Sprite pauseOn;
    [SerializeField] private Sprite pauseOff;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        keyboardController.Pause += SwitchSprite;
    }

    private void OnMouseDown()
    {
        keyboardController.PauseButtonDown();
    }

    private void SwitchSprite()
    {
        if (spriteRenderer.sprite == pauseOff)
        {
            spriteRenderer.sprite = pauseOn;
        }
        else if(spriteRenderer.sprite == pauseOn)
        {
            spriteRenderer.sprite = pauseOff;
        }
    }
}
