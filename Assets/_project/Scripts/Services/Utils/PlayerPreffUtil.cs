using UnityEngine;

public class PlayerPrefsService : IPlayerPrefsService
{
    private const string ProgressKey = "PROGRESS";

    public string GetProgress(string defaultProgress) =>
        PlayerPrefs.GetString(ProgressKey, defaultProgress);

    public void SaveProgress(string progress) =>
        PlayerPrefs.SetString(ProgressKey, progress);
}
