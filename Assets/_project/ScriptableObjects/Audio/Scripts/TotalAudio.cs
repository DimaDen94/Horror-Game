using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Audio/Sounds", order = 51)]
public class TotalAudio : ScriptableObject
{
    [SerializeField] private List<AudioData> _sounds;
    [SerializeField] private List<AudioData> _music;

    public List<AudioData> Sounds => _sounds;
    public List<AudioData> Music => _music; 

}
