using System;
using System.Collections.Generic;


[Serializable]
public class LevelConfig
{
    public LevelEnum level;
    public TranslatableKey TextHintKey;
    public List<HintEnum> hints;
}
