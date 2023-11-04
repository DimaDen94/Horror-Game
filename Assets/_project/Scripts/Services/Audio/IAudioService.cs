public interface IAudioService
{
    bool IsMusicEnable();
    bool IsSoundEnable();
    void LoadVolumeData();
    void MusicEnable(bool enable);
    void PlayAudio(SoundEnum name);
    void PlayBackMusic(SoundEnum soundEnum);
    void SetBackMusicVolume(float volume);
    void SetDefaultBackMusicVolume();
    void SoundEnable(bool enable);
    void StopAudio(SoundEnum name);
    void SwitchVolume(AudioEnum _audioType, bool enable);
    void PlaySpeech(LevelEnum currentLevel);
}