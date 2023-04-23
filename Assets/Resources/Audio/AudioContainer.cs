using UnityEngine;

public class AudioContainer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

}
