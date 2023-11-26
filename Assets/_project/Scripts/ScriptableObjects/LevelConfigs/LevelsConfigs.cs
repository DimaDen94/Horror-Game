using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configuration/Levels", order = 51)]
public class LevelsConfigs : ScriptableObject {

    [SerializeField] private List<LevelConfig> _levelConfigs;

    public List<LevelConfig> LevelConfigs => _levelConfigs;
}
