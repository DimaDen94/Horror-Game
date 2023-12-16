using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryMenu : MonoBehaviour
{
    private const string HideAnimationKey = "Hide";

    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _memoryText;
    [SerializeField] private Image _photo;
    [SerializeField] private Animator _animator;
    [SerializeField] private TextMeshProUGUI _progressCount;

    private IAudioService _audioService;
    private StateMachine _stateMachine;

    public void Construct(IAudioService audioService, StateMachine stateMachine)
    {
        _audioService = audioService;
        _stateMachine = stateMachine;
    }

    private void Start()
    {
        _button.GetComponent<ButtonClickPlayer>().Construct(_audioService);
        _button.onClick.AddListener(CloseMenu);
    }


    public void SetText(string text) => _memoryText.SetText(text);

    public void SetSprite(Sprite sprite) => _photo.sprite = sprite;

    public void SetMemoryProgress(int progress, int maxCount) => _progressCount.SetText(progress + "/" + maxCount);

    public void Hide() => _animator.Play(HideAnimationKey);

    public void DestroyMenu() => Destroy(gameObject);

    private void CloseMenu() => _stateMachine.Enter<GameLoopState>();

}
