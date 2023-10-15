public interface IPlayerPrefsService
{
    string GetProgress(string defaultProgress);
    void SaveProgress(string progress);
}