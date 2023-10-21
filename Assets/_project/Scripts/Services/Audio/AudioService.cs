using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioService : IAudioService
{
    private TotalAudio _totalSounds;
    private AudioMixer _mixer;
    private AudioMixerGroup _soundGroup;
    private AudioMixerGroup _musicGroup;
    private AudioContainer _audioContainer;

    private float _deaultMusicVolume;

    public string AudioMixerPath = "Audio/AudioMixer";
    public string TotalSoundsPath = "Audio/Total Sounds";
    public string AudioContainerPath = "Audio/AudioContainer";

    private SoundEnum _curentMusic = SoundEnum.None;

    public AudioService() {
        _totalSounds = Resources.Load<TotalAudio>(TotalSoundsPath);
        _audioContainer = Object.Instantiate(Resources.Load<AudioContainer>(AudioContainerPath));

        _mixer = Resources.Load<AudioMixer>(AudioMixerPath);
        _soundGroup = _mixer.FindMatchingGroups(AudioEnum.Sound.ToString())[0];
        _musicGroup = _mixer.FindMatchingGroups(AudioEnum.Music.ToString())[0];
        CreateSoundSources();

        LoadVolumeData();
    }



    private void CreateSoundSources()
    {
        CreateAudioList(_totalSounds.Sounds, _soundGroup);
        CreateAudioList(_totalSounds.Music, _musicGroup);
    }

    private void CreateAudioList(List<AudioData> audio, AudioMixerGroup group)
    {
        foreach (AudioData sound in audio)
        {
            sound.source = _audioContainer.gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.valume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = group;
        }
    }


    public void PlayBackMusic(SoundEnum soundEnum)
    {
        foreach (AudioData audioData in _totalSounds.Music)
        {
            if (audioData.soundName == soundEnum)
            {
                if(soundEnum != _curentMusic)
                    audioData.source.Play();

                _deaultMusicVolume = audioData.valume;
                _curentMusic = soundEnum;
            }
            else {
                audioData.source.Stop();
            }
        }
    }

    public void SetBackMusicVolume(float volume)
    {
        if (volume >= 0 && volume <= 1)
            foreach (var audioData in _totalSounds.Music)
                audioData.source.volume = volume;
    }
    public void SetDefaultBackMusicVolume()
    {
        foreach (var audioData in _totalSounds.Music)
            audioData.source.volume = _deaultMusicVolume;
    }

    public void PlayAudio(SoundEnum name)
    {
        AudioData sound = _totalSounds.Sounds.Find(sound => sound.soundName == name);
        sound.source.Play();
    }

    public void StopAudio(SoundEnum name)
    {
        try
        {
            _totalSounds.Sounds.Find(sound => sound.soundName == name).source.Stop();
        }
        catch (MissingReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }

    public void LoadVolumeData()
    {
        bool musicEnable = PlayerPrefs.GetString(AudioEnum.Music.ToString(), true.ToString()).Equals("True");
        SwitchVolume(AudioEnum.Music, musicEnable);

        bool soundEnable = PlayerPrefs.GetString(AudioEnum.Sound.ToString(), true.ToString()).Equals("True");
        SwitchVolume(AudioEnum.Sound, soundEnable);
    }

    public void SwitchVolume(AudioEnum _audioType, bool enable)
    {
        if (enable)
            _mixer.SetFloat(_audioType.ToString(), 0f);
        else
            _mixer.SetFloat(_audioType.ToString(), -80f);
    }

    public void SoundEnable(bool enable)
    {
        PlayerPrefs.SetString(AudioEnum.Sound.ToString(), enable.ToString());
        SwitchVolume(AudioEnum.Sound, enable);
    }

    public void MusicEnable(bool enable)
    {
        PlayerPrefs.SetString(AudioEnum.Music.ToString(), enable.ToString());
        SwitchVolume(AudioEnum.Music, enable);
    }


    public bool IsSoundEnable() => PlayerPrefs.GetString(AudioEnum.Sound.ToString(), true.ToString()).Equals("True");

    public bool IsMusicEnable() => PlayerPrefs.GetString(AudioEnum.Music.ToString(), true.ToString()).Equals("True");

}
