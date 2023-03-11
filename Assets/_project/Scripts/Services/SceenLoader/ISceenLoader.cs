using System;

public interface ISceenLoader
{
    void Load(SceneEnum scene, Action onLoad = null);
    void Load(string name, Action onLoad = null);
    void Reload(Action onLoad = null);
}