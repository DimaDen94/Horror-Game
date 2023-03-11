using UnityEngine;
using UnityEngine.UI;

public class MainMenuMediator : MonoBehaviour
{
    [SerializeField] private Button _continue;
    [SerializeField] private Button _newGame;
    [SerializeField] private Button _setting;
    [SerializeField] private Button _adOff;

    private StateMachine _stateMachine;

    public void Construct(StateMachine stateMachine) {
        _stateMachine = stateMachine;
        _continue.onClick.AddListener(StartGame);
        _newGame.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        _stateMachine.Enter<LoadLevelState, string>(LevelEnum.Level1.ToString());
    }
}
