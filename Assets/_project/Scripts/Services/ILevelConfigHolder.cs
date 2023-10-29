using System.Collections.Generic;

public interface ILevelConfigHolder
{
    List<LevelConfig> Configs { get; }

    LevelConfig GetLevelConfig(LevelEnum level);
}