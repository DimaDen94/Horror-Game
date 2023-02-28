using UnityEngine;

public class MainMenuState : IState
{
    private StateMachine _stateMachine;
    private ISceenLoader _sceneLoader;
    private IUIFactory _uiFactory;

    public MainMenuState(StateMachine stateMachine, ISceenLoader sceneLoader, IUIFactory uiFactory)
    {
        _stateMachine = stateMachine;
        _sceneLoader = sceneLoader;
        _uiFactory = uiFactory;
    }

    public void Enter()
    {
        Debug.Log("MainMenuState Enter");
        _sceneLoader.Load(SceneEnum.MainMenu, OnLoad);

    }

    private void OnLoad()
    {
        MainMenuMediator hud = _uiFactory.CreateMainMenuHud();
        hud.Construct(_stateMachine);
    }

    public void Exit()
    {
        
    }
}