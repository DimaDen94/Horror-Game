using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceenLoader : MonoBehaviour, ISceenLoader
{
    private string _lastScene = string.Empty;

    private void Awake() =>
        DontDestroyOnLoad(this);

    public void Load(SceneEnum scene, Action onLoad = null)
    {
        if (!_lastScene.Equals(scene.ToString()))
            Load(scene.ToString(), onLoad);
        else
            onLoad?.Invoke();

        _lastScene = scene.ToString();
    }

    public void Load(string name, Action onLoad = null)
    {
        _lastScene = name;
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
