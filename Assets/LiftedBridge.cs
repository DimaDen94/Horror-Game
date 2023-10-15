using System;
using DG.Tweening;
using UnityEngine;

public class LiftedBridge : MonoBehaviour
{
    private const int LiftDuration = 3;
    private const float MoveHeight = 1.9f;

    public event Action BridgeCompleted;
   
    [SerializeField] private Transform _bridge;
    [SerializeField] private AudioSource _metalSound;
    [SerializeField] private BoxCollider _lockCollider;

    public void LiftUp()
    {
        if (_bridge.localPosition.y != 0)
            return;

        _bridge.DOLocalMoveY(MoveHeight, LiftDuration).OnComplete(OnBridgeCompleted);
        _metalSound.Play();
        Destroy(_lockCollider);
    }

    private void OnBridgeCompleted()
    {
        BridgeCompleted?.Invoke();
    }
}
