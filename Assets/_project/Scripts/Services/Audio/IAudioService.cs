public interface IAudioService
{
    void LoadVolumeData();
    void PlayAudio(SoundEnum name);
    void PlayBackMusic(SoundEnum soundEnum);
    void SetBackMusicVolume(float volume);
    void SetDefaultBackMusicVolume();
    void StopAudio(SoundEnum name);
    void SwitchVolume(AudioEnum _audioType, bool enable);
}