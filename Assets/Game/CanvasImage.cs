using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasImage : MonoBehaviour
{
    private RectTransform rectTransform;
    private Image image;
    private float canvasWidth;
    private float canvasHeight;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();

        canvasWidth = rectTransform.rect.width;
        canvasHeight = rectTransform.rect.height;

        SetScale();
    }

    private void Update()
    {
        if (canvasWidth != rectTransform.rect.width || canvasHeight != rectTransform.rect.height)
        {
            canvasWidth = rectTransform.rect.width;
            canvasHeight = rectTransform.rect.height;
            SetScale();
        }
    }

    private void SetScale()
    {
        int imageWidth = image.sprite.texture.width;
        int imageHeight = image.sprite.texture.height;
        
        float widthScale = canvasWidth / imageWidth;
        float heightScale = canvasHeight / imageHeight;

        float scale = Mathf.Min(widthScale, heightScale);

        GetComponent<CanvasScaler>().scaleFactor = scale;
    }
}
