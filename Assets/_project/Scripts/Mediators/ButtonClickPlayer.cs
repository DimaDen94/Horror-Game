using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickPlayer : MonoBehaviour
{
    [SerializeField] private SoundEnum _sound = SoundEnum.Click;
    private IAudioService _audioService;
    private Button _button;

    public void Construct(IAudioService audioService) =>
        _audioService = audioService;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(PlayAudio);
    }

    private void PlayAudio()
    {
        _audioService.PlayAudio(_sound);
    }
}
