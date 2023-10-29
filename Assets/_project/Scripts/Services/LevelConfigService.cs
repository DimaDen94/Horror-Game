using System.Collections.Generic;

public class LevelConfigService : ILevelConfigHolder
{
    private const string LevelConfigsPath = "Configs/Levels";

    private IAssetProvider _assetProvider;
    private LevelsConfigs _levels;

    public LevelConfigService(IAssetProvider assetProvider)
    {
        _assetProvider = assetProvider;
        _levels = _assetProvider.LoadScriptableObject<LevelsConfigs>(LevelConfigsPath);
    }

    public LevelConfig GetLevelConfig(LevelEnum level) =>
        _levels.LevelConfigs.Find(conf => conf.level == level);

    public List<LevelConfig> Configs => _levels.LevelConfigs;
}