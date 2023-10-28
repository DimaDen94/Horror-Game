using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent (typeof(Text))]
public class TextTranslator : MonoBehaviour
{
	[SerializeField] private TranslatableKey key;
    private ILocalizationService _localizationService;
    private Text _text;

    [Inject]
	public void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
        _text = GetComponent<Text>();
        UpdateText();
    }

    public void UpdateText() => _text.text = _localizationService.GetTranslateByKey(key);
}
