public class AnalyticService : IAnalyticService
{
    private IAnalyticService _firebaseAnalyticService;

    public AnalyticService(FirebaseAnalyticService firebaseAnalyticService)
    {
        _firebaseAnalyticService = firebaseAnalyticService;
    }

    public void HintUnlock(LevelEnum levelEnum, HintEnum hintType)
    {
        
    }

    public void LevelStart(LevelEnum level)
    {
       
    }

    public void MemoryUnlock(LevelEnum currentLevel)
    {
        
    }

    public void BottleInPot()
    {
        
    }

    public void MashroomInPot(int count)
    {
        
    }
}