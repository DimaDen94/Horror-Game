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
        _matalGrate.DOLocalMoveY(YOffset, OpenDuration).OnComplete(()=> {
            if (_isClose)
            {
                GateOpened?.Invoke();
                _matalGrate.gameObject.SetActive(false);
            }
            _isClose = false;
        });
    }

    public void CloseGate()
    {
        if (!_matalGrate.gameObject.activeSelf)
            _matalGrate.gameObject.SetActive(true);

        Debug.Log("Close");
        _metalSound.Play();
        _matalGrate.DOLocalMoveY(0, OpenDuration);
    }
}
