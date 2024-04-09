public interface IAnalyticService
{
    void LevelStart(LevelEnum level);
    void MemoryUnlock(LevelEnum currentLevel);
    void HintUnlock(LevelEnum levelEnum, HintEnum hintType);
    void MashroomInPot(int count, string name);
    void BottleInPot();
    void RotateTutorialCompleted();
    void PickUpKeyTutorialCompleted();
    void MoveTutorialCopmleted();
    void LeverLevelKeyLifted();
    void LeverLevelChestOpened();
    void LeverLevelLeverLifted();
    void LeverLevelLeverActivate();
    void LeverLevelMechanismFixed();
    void FirstClownLevelStartGateOpen();
    void FirstClownLevelSecondGateOpen();
    void LabyrinthLevelFirstKeyLifted();
    void LabyrinthLevelFirstChestOpened();
    void LabyrinthLevelRedKeyLifted();
    void LabyrinthLevelBlueKeyLifted();
    void LabyrinthLevelRedChestOpened();
    void LabyrinthLevelExitKeyLifted();
    void StairLevelCollectedWeight(int weightCount, string name);
}