using UnityEngine;
using UnityEngine.UI;

public class MainMenuMediator : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _adOff;

    private StateMachine _stateMachine;
    private IProgressService _progressService;

    public void Construct(StateMachine stateMachine, IAudioService audioService, IProgressService progressService) {
        _stateMachine = stateMachine;
        _progressService = progressService;
        _continue.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _newGame.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _setting.GetComponent<ButtonClickPlayer>().Construct(audioService);
        _adOff.GetComponent<ButtonClickPlayer>().Construct(audioService);

        _continue.onClick.AddListener(Continue);
        _newGame.onClick.AddListener(NewGame);
    }

    private void NewGame()
    {
        _progressService.ResetProgress();
        _stateMachine.Enter<LoadLevelState, string>(_progressService.GetCurrentLevel().ToString());
    }
    private void Continue()
    {
        _stateMachine.Enter<LoadLevelState, string>(_progressService.GetCurrentLevel().ToString());
    }
}
