using System;
using DG.Tweening;
using UnityEngine;

public class Bottle : LiftedThing
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Mesh _redMesh; 
    [SerializeField] private ParticleSystem _particleSystem; 
    
    private bool _isRed = false;

    public void SetRedSkin() {
        _meshFilter.mesh = _redMesh;
        _isRed = true;
    }

    public bool IsRed() =>
        _isRed;

    public void Blow()
    {
        Instantiate(_particleSystem,transform.position,Quaternion.identity);
        Destroy(this);
    }

    public void Throw(Albino albino)
    {
        albino.GetComponent<Collider>().enabled = false;
        transform.parent = albino.transform;
        transform.DOJump(albino.transform.position, 1, 1, 1).OnComplete(
            () => {
                Blow();
                albino.Die();
            });
    }
}
