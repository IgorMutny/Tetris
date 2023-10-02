using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite volumeOn;
    [SerializeField] private Sprite volumeOff;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource effectSource;

    private string volumeKey = "Volume";
    private int volume = 1;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (PlayerPrefs.HasKey(volumeKey))
        {
            volume = PlayerPrefs.GetInt(volumeKey, 1);
        }

        SetVolume();
    }

    private void SetVolume()
    {
        if (volume == 1)
        {
            musicSource.volume = 0.25f;
            effectSource.volume = 0.25f;
            spriteRenderer.sprite = volumeOn;
        }
        else if (volume == 0)
        {
            musicSource.volume = 0f;
            effectSource.volume = 0f;
            spriteRenderer.sprite = volumeOff;
        };
    }

    private void OnMouseDown()
    {
        if (volume == 1)
        {
            volume = 0;
        }
        else if (volume == 0)
        {
            volume = 1;
        }

        PlayerPrefs.SetInt(volumeKey, volume);
        SetVolume();
    }
}
