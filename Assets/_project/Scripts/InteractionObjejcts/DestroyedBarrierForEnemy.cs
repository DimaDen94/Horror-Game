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
            LockBoard notDestroyedBoard = _boards.Find(board => !board.ISDestroy);
            if (notDestroyedBoard == null)
                BarrierDestroyed?.Invoke();
            else
                notDestroyedBoard.Hit();

        }
    }
}
