using System;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseAnalyticService : IAnalyticService
{
    
    private DependencyStatus dependencyStatus;
    private FirebaseApp app;

    public void Init(Action result)
    {
#if UNITY_ANDROID
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
            result?.Invoke();
        });
#endif
    }

    public void LevelStart(LevelEnum level)
    {
        FirebaseAnalytics.LogEvent(
            FirebaseAnalytics.EventLevelStart,
            new Parameter(FirebaseAnalytics.ParameterLevel, (int)level),
            new Parameter(FirebaseAnalytics.ParameterLevelName, level.ToString())
        ) ;
    }

    public void MemoryUnlock(LevelEnum currentLevel)
    {
        FirebaseAnalytics.LogEvent(
            FirebaseAnalytics.EventUnlockAchievement,
            new Parameter(FirebaseAnalytics.ParameterLevel, (int)currentLevel),
            new Parameter(FirebaseAnalytics.ParameterLevelName, currentLevel.ToString())
        );
    }

    public void HintUnlock(LevelEnum currentLevel, HintEnum hintType)
    {
        FirebaseAnalytics.LogEvent(
            EventKeys.HintUnlock,
            new Parameter(FirebaseAnalytics.ParameterLevel, (int)currentLevel),
            new Parameter(FirebaseAnalytics.ParameterLevelName, currentLevel.ToString()),
            new Parameter(EventKeys.HintType, hintType.ToString())
        );
    }

    public void MashroomInPot(int count, string name) => FirebaseAnalytics.LogEvent(EventKeys.DragonLevelMashroomInPot,
        new Parameter(EventKeys.MashromsCount, count.ToString()),
        new Parameter(EventKeys.MashromsName, name)
        );

    public void BottleInPot() => FirebaseAnalytics.LogEvent(EventKeys.DragonLevelBottleInPot);

    public void RotateTutorialCompleted() => FirebaseAnalytics.LogEvent(EventKeys.RotateTutorialCompleted);

    public void PickUpKeyTutorialCompleted() => FirebaseAnalytics.LogEvent(EventKeys.PickUpKeyTutorialCompleted);

    public void MoveTutorialCopmleted() => FirebaseAnalytics.LogEvent(EventKeys.MoveTutorialCopmleted);

    public void LeverLevelKeyLifted() => FirebaseAnalytics.LogEvent(EventKeys.LeverLevelKeyLifted);

    public void LeverLevelChestOpened() => FirebaseAnalytics.LogEvent(EventKeys.LeverLevelChestOpened);

    public void LeverLevelLeverLifted() => FirebaseAnalytics.LogEvent(EventKeys.LeverLevelLeverLifted);

    public void LeverLevelLeverActivate() => FirebaseAnalytics.LogEvent(EventKeys.LeverLevelLeverActivate);

    public void LeverLevelMechanismFixed() => FirebaseAnalytics.LogEvent(EventKeys.LeverLevelMechanismFixed);

    public void FirstClownLevelStartGateOpen() => FirebaseAnalytics.LogEvent(EventKeys.FirstClownLevelStartGateOpen);

    public void FirstClownLevelSecondGateOpen() => FirebaseAnalytics.LogEvent(EventKeys.FirstClownLevelSecondGateOpen);

    public void LabyrinthLevelFirstKeyLifted() => FirebaseAnalytics.LogEvent(EventKeys.LabyrinthLevelFirstKeyLifted);

    public void LabyrinthLevelFirstChestOpened() => FirebaseAnalytics.LogEvent(EventKeys.LabyrinthLevelFirstChestOpened);

    public void LabyrinthLevelRedKeyLifted() => FirebaseAnalytics.LogEvent(EventKeys.LabyrinthLevelRedKeyLifted);

    public void LabyrinthLevelBlueKeyLifted() => FirebaseAnalytics.LogEvent(EventKeys.LabyrinthLevelBlueKeyLifted);

    public void LabyrinthLevelRedChestOpened() => FirebaseAnalytics.LogEvent(EventKeys.LabyrinthLevelRedChestOpened);

    public void LabyrinthLevelExitKeyLifted() => FirebaseAnalytics.LogEvent(EventKeys.LabyrinthLevelExitKeyLifted);

    public void StairLevelCollectedWeight(int weightCount, string name)
    {
        FirebaseAnalytics.LogEvent(EventKeys.StairLevelCollectedWeight,
        new Parameter(EventKeys.WeightCount, weightCount.ToString()),
        new Parameter(EventKeys.WeightName, name)
        );
    }
}

public class EventKeys {

    public const string HintUnlock = "HintUnlock";

    public const string RotateTutorialCompleted = "RotateTutorialCompleted";
    public const string PickUpKeyTutorialCompleted = "PickUpKeyTutorialCompleted";
    public const string MoveTutorialCopmleted = "MoveTutorialCopmleted";

    public const string LeverLevelKeyLifted = "LeverLevelKeyLifted";
    public const string LeverLevelChestOpened = "LeverLevelChestOpened";
    public const string LeverLevelLeverLifted = "LeverLevelLeverLifted";
    public const string LeverLevelLeverActivate = "LeverLevelLeverActivate";
    public const string LeverLevelMechanismFixed = "LeverLevelMechanismFixed";

    public const string FirstClownLevelSecondGateOpen = "FirstClownLevelSecondGateOpen";
    public const string FirstClownLevelStartGateOpen = "FirstClownLevelStartGateOpen";

    public const string DragonLevelMashroomInPot = "MashroomInPot";
    public const string DragonLevelBottleInPot = "BottleInPot";

    public const string LabyrinthLevelFirstKeyLifted = "LabyrinthLevelFirstKeyLifted";
    public const string LabyrinthLevelFirstChestOpened = "LabyrinthLevelFirstChestOpened";
    public const string LabyrinthLevelRedKeyLifted = "LabyrinthLevelRedKeyLifted";
    public const string LabyrinthLevelBlueKeyLifted = "LabyrinthLevelBlueKeyLifted";
    public const string LabyrinthLevelRedChestOpened = "FLabyrinthLevelRedChestOpened";
    public const string LabyrinthLevelExitKeyLifted = "LabyrinthLevelExitKeyLifted";

    public const string StairLevelCollectedWeight = "StairLevelCollectedWeight";

    //Parameters
    public const string HintType = "HintType";
    public const string WeightCount = "WeightCount";
    public const string WeightName = "WeightName";
    public const string MashromsCount = "Mashroms";
    public const string MashromsName = "MashromsName";
}