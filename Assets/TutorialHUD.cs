using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialHUD : MonoBehaviour
{
    private const float HandXOffset = 100f;
    private const float HandYOffset = 100f;
    private const float HandAnimationDuration = 1f;

    [SerializeField] private GameObject _rightArea;
    [SerializeField] private Transform _rightHand;

    [SerializeField] private GameObject _leftArea;
    [SerializeField] private Transform _leftHand;

    [SerializeField] private TextMeshProUGUI _hint;

    private HeroMover _heroMover;
    private Hero _hero;
    private HeroSlot _slot;
    private Hud _hud;

    private IInputService _inputService;
    private IToastService _toastService;
    private ILocalizationService _localizationService;
    private IAnalyticService _analyticService;

    public void Construct(Hero hero,HeroMover heroMover, HeroSlot slot, IInputService inputService, IToastService toastService, ILocalizationService localizationService, Hud hud, IAnalyticService analyticService) {
        _heroMover = heroMover;
        _hero = hero;
        _slot = slot;
        _hud = hud;
        _inputService = inputService;
        _toastService = toastService;
        _localizationService = localizationService;
        _analyticService = analyticService;
    }

    private void Start() => StartRotateTutorial();

    private void StartRotateTutorial() {
        _rightArea.SetActive(true);
        _hud.HideJoystick();
        _heroMover.LockMovement();
        _rightHand.DOMoveX(_rightHand.transform.position.x + HandXOffset, HandAnimationDuration).SetLoops(-1, LoopType.Yoyo);
        _hero.OnInteractionObjectExist += OnRotateTutorialCompleted;

        _hint.SetText(_localizationService.GetTranslateByKey(TranslatableKey.RotateTutorial));
    }

    private void OnRotateTutorialCompleted()
    {
        StartPickUpKeyTutorial();
        _hero.OnInteractionObjectExist -= OnRotateTutorialCompleted;
        _analyticService.RotateTutorialCompleted();
    }

    private void StartPickUpKeyTutorial()
    {
        _rightHand.DOKill();
        _heroMover.LockRotated();
        _rightArea.SetActive(false);
        _slot.OnLootExist += OnStartPickUpKeyTutorialCompleted;

        _hint.SetText(_localizationService.GetTranslateByKey(TranslatableKey.GetKeyTutorial));
    }

    private void OnStartPickUpKeyTutorialCompleted()
    {
        _leftArea.SetActive(true);
        _slot.OnLootExist -= OnStartPickUpKeyTutorialCompleted;
        MoveTutorial();
        _analyticService.PickUpKeyTutorialCompleted();
    }

    private void MoveTutorial()
    {
        _heroMover.UnlockMovement();
        _heroMover.UnlockRotated();

        _leftHand.DOLocalMoveY(_rightHand.transform.position.y + HandYOffset, HandAnimationDuration).SetLoops(-1, LoopType.Yoyo);
        _hud.ShowJoystick();
        _hint.SetText(_localizationService.GetTranslateByKey(TranslatableKey.MoveTutorial));
    }

    private void Update()
    {
        if (_inputService.MoveAxis.sqrMagnitude > Constants.Epsilon) {
            _leftHand.DOKill();
            _leftArea.SetActive(false);
            _toastService.ShowToast(TranslatableKey.GoToExitTutorial);
            _analyticService.MoveTutorialCopmleted();
            Destroy(gameObject);
        }
    }
}
