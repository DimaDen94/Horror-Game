using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelConfig
{
    public LevelEnum level;
    public TranslatableKey TextHintKey;
    public TranslatableKey TextMemoryKey;
    //public Vector3 MemoryItemPosition;
    public List<HintEnum> hints;
}
