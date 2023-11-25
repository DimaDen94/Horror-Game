using DG.Tweening;
using UnityEngine;

public class Bottle : LiftedThing
{
    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Mesh _redMesh; 
    [SerializeField] private ParticleSystem _particleSystem; 
    [SerializeField] private AudioSource _audioSourceBlow; 
    [SerializeField] private AudioSource _audioSourceThrow; 
    
    private bool _isRed = false;

    public void SetRedSkin() {
        _meshFilter.mesh = _redMesh;
        _isRed = true;
    }

    public bool IsRed() =>
        _isRed;

    public void Blow()
    {
        _audioSourceBlow.Play();
        Instantiate(_particleSystem,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void Throw(Nightmare dragon)
    {
        _audioSourceThrow.Play();
        dragon.GetComponent<Collider>().enabled = false;
        transform.parent = dragon.transform;
        transform.DOJump(dragon.transform.position, 1, 1, 1).OnComplete(
            () => {
                Blow();
                dragon.Die();
            });
    }
}
