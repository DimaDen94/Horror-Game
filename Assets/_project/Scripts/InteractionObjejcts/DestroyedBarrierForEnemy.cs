using System;
using System.Collections.Generic;
using UnityEngine;

public class DestroyedBarrierForEnemy : MonoBehaviour, IHitable
{
    [SerializeField] private List<LockBoard> _boards;

    public bool IsDestroy => _boards.Count == 0;

    public event Action BarrierDestroyed;

    public void Hit(Transform striking)
    {
        if (_boards.Count > 0)
        {
            _boards[0].Hit();
            _boards.Remove(_boards[0]);
            if (_boards.Count == 0)
                BarrierDestroyed?.Invoke();
        }
    }
}
