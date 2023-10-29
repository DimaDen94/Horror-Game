using System;

[Serializable]
public class HintState
{
    public HintEnum hint;
    public bool enable;

    public HintState(HintEnum hint, bool enable)
    {
        this.hint = hint;
        this.enable = enable;
    }

    public HintState() {

    }
}