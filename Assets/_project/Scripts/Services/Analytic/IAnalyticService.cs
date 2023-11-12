public interface IAnalyticService
{
    void LevelStart(LevelEnum level);
    void MemoryUnlock(LevelEnum currentLevel);
    void HintUnlock(LevelEnum levelEnum, HintEnum hintType);
}