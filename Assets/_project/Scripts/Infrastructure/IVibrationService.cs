public interface IVibrationService
{
    bool IsVibrationEnable();
    void SwitchEnable();
    void TryVibration();
}