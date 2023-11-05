using System;
using TMPro;
using UnityEngine;

public class ToastView : MonoBehaviour
{
    public event Action ToastDestroyed;
    public TextMeshProUGUI _text;

    private void DestroyToast()
    {
        ToastDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public void SetText(string text) => _text.text = text;
}