public class FirstLevelBootstrapper : LevelBootstrapper
{
    private TutorialHUD _tutorialHud;

    private new void Start() {
        base.Start();
        _tutorialHud = _uiFactory.CreateTutorialHud();
        _tutorialHud.Construct(_hero,_hero.Mover,_hero.Slot,_inputService, _toastService,_localizationService, _hud, _analyticService);
    }
}
