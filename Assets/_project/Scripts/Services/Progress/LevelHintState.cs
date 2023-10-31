using System;
using System.Collections.Generic;

[Serializable]
public class LevelHintState
{
    public LevelEnum level;
    public List<HintState> hintStates;

    public LevelHintState(LevelEnum level, List<HintEnum> hints)
    {
        this.level = level;
        hintStates = new List<HintState>();
        foreach (var item in hints)
        {
            if (item == HintEnum.HintText && level == LevelEnum.Level10)
                hintStates.Add(new HintState( item, true));
            else
                hintStates.Add(new HintState(item, false));
        }
    }
    public LevelHintState() { }
}
