using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceenLoader : MonoBehaviour, ISceenLoader
{
    private SceneEnum _lastScene = SceneEnum.None;

    private void Awake() =>
        DontDestroyOnLoad(this);

    public void Load(SceneEnum scene, Action onLoad = null)
    {
        if (_lastScene != scene)
            Load(scene.ToString(), onLoad);
        else
            onLoad?.Invoke();
    }

    public void Load(string name, Action onLoad = null)
    {
        StartCoroutine(LoadSceene(name, onLoad));
    }

    public void Reload(Action onLoad = null)
    {
        StartCoroutine(ReloadSceene(onLoad));
    }

    private IEnumerator ReloadSceene(Action onLoad)
    {
        AsyncOperation waitForNextSceen = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        if (!waitForNextSceen.isDone)
            yield return null;
        onLoad?.Invoke();
    }

    private IEnumerator LoadSceene(string name, Action onLoad)
    {
        AsyncOperation waitForNextSceen = SceneManager.LoadSceneAsync(name);
        while (!waitForNextSceen.isDone)
            yield return null;
        onLoad?.Invoke();
    }
}
