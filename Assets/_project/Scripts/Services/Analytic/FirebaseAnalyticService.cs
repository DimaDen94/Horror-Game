using System;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseAnalyticService : IAnalyticService
{
    private const string HintUnlockEventKey = "HintUnlock";
    private const string HintTypeParameter = "HintType";

    private FirebaseApp app;
    private DependencyStatus dependencyStatus;



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
            HintUnlockEventKey,
            new Parameter(FirebaseAnalytics.ParameterLevel, (int)currentLevel),
            new Parameter(FirebaseAnalytics.ParameterLevelName, currentLevel.ToString()),
            new Parameter(HintTypeParameter, hintType.ToString())
        );
    }
}