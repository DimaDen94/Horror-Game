    using TMPro;
using UnityEngine;
using Zenject;
[RequireComponent(typeof(TextMeshProUGUI))]
public class TextMeshProTranslator : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField] private TranslatableKey key;
	private ILocalizationService _localizationService;
    private TextMeshProUGUI _text;

    [Inject]
	public void Construct(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
        _text = GetComponent<TextMeshProUGUI>();
        UpdateText();

    }

    public void UpdateText() => _text.text = _localizationService.GetTranslateByKey(key);
}
