using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public const int TypeI = 0;
    public const int TypeJ = 1;
    public const int TypeL = 2;
    public const int TypeO = 3;
    public const int TypeZ = 4;
    public const int TypeT = 5;
    public const int TypeS = 6;

    public const int ColorI = 7;
    public const int ColorJ = 1;
    public const int ColorL = 2;
    public const int ColorO = 3;
    public const int ColorZ = 4;
    public const int ColorT = 5;
    public const int ColorS = 6;

    [SerializeField] private Sprite empty;
    [SerializeField] private Sprite filled;

    private SpriteRenderer spriteRenderer;

    public void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = empty;
        spriteRenderer.color = Color.white;
    }

    public void SetState(int state)
    {
        Sprite newSprite = empty;
        Color newColor = Color.white;

        if (state > 0)
            newSprite = filled;

        switch (state)
        {
            case ColorJ: newColor = new Color(0f, 0.3f, 1f); break;
            case ColorL: newColor = new Color(1f, 0.4f, 0f); break;
            case ColorO: newColor = new Color(1f, 1f, 0f); break;
            case ColorZ: newColor = new Color(1f, 0.15f, 0.15f); break;
            case ColorT: newColor = Color.magenta; break;
            case ColorS: newColor = Color.green; break;
            case ColorI: newColor = Color.cyan; break;
        }

        spriteRenderer.sprite = newSprite;
        spriteRenderer.color = newColor;
    }
}

