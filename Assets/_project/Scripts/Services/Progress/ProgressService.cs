using System;

public class ProgressService : IProgressService
{
    private UserProgress _userProgress;
    private IJsonConvertor _jsonConvertor;
    private IPlayerPrefsService _playerPrefsService;

    public event Action LevelIncremented;

    public ProgressService(IJsonConvertor jsonConvertor, IPlayerPrefsService playerPrefsService)
    {
        _jsonConvertor = jsonConvertor;
        _playerPrefsService = playerPrefsService;
        LoadProgress();
    }

    public LevelEnum LevelCompletedCount => _userProgress.CurrentLevel;

    public event Action PurchaseDataChanged;

    public void LoadProgress()
    {
        UserProgress defaultProgress = GenerateStartProgress();
        string defaultSerializeProgress = _jsonConvertor.SerializeObject(defaultProgress);
        string progressString = _playerPrefsService.GetProgress(defaultSerializeProgress);
        _userProgress = _jsonConvertor.DeserializeObject<UserProgress>(progressString);
    }

    private UserProgress GenerateStartProgress() => new UserProgress() { CurrentLevel = LevelEnum.Level1 };

    public void SetNewCurrentLevel(LevelEnum level)
    {
        _userProgress.CurrentLevel = level;
        SaveProgress();
    }

    public LevelEnum GetCurrentLevel() => _userProgress.CurrentLevel;

    public void ResetProgress()
    {
        _userProgress.CurrentLevel = LevelEnum.Level1;
        SaveProgress();
    }

    private void SaveProgress()
    {
        string progressString = _jsonConvertor.SerializeObject(_userProgress);
        _playerPrefsService.SaveProgress(progressString);
    }
}