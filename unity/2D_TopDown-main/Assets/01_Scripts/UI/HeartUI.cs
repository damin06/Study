using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    private Image _heartImage;

    private void Awake()
    {
        _heartImage = GetComponent<Image>();
    }

    public void SetSprite(Sprite sprite)
    {
        _heartImage.sprite = sprite;
    }
}
