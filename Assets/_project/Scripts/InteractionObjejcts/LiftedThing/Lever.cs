using System;
using DG.Tweening;
using UnityEngine;

public class Lever : LiftedThing
{
    private const int MovementDuration = 1;
    [SerializeField] private GameObject _mesh;

    public void MeshUp()
    {
        if (_mesh != null)
            _mesh.transform.DOLocalMove(Vector3.zero, MovementDuration);
    }
}
