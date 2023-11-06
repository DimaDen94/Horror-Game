using System;

[Serializable]
public class LevelMemoryState
{
    public LevelEnum level;
    public bool enable;

    public LevelMemoryState()
    {
    }

    public LevelMemoryState(LevelEnum level)
    {
        this.level = level;
        enable = false;
    }
}