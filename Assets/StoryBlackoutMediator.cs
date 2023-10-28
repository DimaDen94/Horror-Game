using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryBlackoutMediator : BlackoutMediator
{
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;

    public void SetSprite(Sprite sprite) => _image.sprite = sprite;

    public void SetText(string text) => _text.text = text;
}
