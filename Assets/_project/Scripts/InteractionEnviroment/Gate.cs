using System;
using DG.Tweening;
using UnityEngine;

public class Gate : MonoBehaviour
{
    private const int OpenDuration = 3;
    private const int YOffset = 3;
    private bool _isClose = true;

    public event Action GateOpened;

    [SerializeField] private Transform _matalGrate;
    [SerializeField] private AudioSource _metalSound;

    public void OpenGate() {
        _metalSound.Play();
        _matalGrate.DOMoveY(YOffset, OpenDuration).OnComplete(()=> {
            if(_isClose)
                GateOpened?.Invoke();
            _isClose = false;
        });
    }

    public void CloseGate()
    {
        Debug.Log("Close");
        _metalSound.Play();
        _matalGrate.DOMoveY(0, OpenDuration);
    }
}
