using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LanguageItemView : MonoBehaviour
{
    [SerializeField] private Button _btn;
    [SerializeField] private Image _image;
    private SystemLanguage _language;

    public UnityAction<SystemLanguage> LanguageClick;

    public void Construct(SystemLanguage language, Sprite icon)
    {
        _language = language;
        _image.sprite = icon;
        
        _btn.onClick.AddListener(OnLanguageClick);
    }

    private void OnLanguageClick() => LanguageClick?.Invoke(_language);

}
