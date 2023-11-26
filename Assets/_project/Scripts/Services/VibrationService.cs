using UnityEngine;

public class VibrationService : IVibrationService
{
    private const string VibrationKey = "Vibration";

    public VibrationService()
    {
        PlayerPrefs.GetString(AudioEnum.Music.ToString(), true.ToString()).Equals("True");
    }

    public bool IsVibrationEnable()
    {
        return PlayerPrefs.GetString(VibrationKey, true.ToString()).Equals("True");
    }

    public void SwitchEnable()
    {
        PlayerPrefs.SetString(VibrationKey, (!PlayerPrefs.GetString(VibrationKey, true.ToString()).Equals("True")).ToString());
    }

    public void TryVibration() {
        if(IsVibrationEnable())
            Handheld.Vibrate();
    }
}