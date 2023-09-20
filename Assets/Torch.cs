using UnityEngine;

public class Torch : WeightThing
{
    [SerializeField] private GameObject _fire;
    private bool _isBurning = false;

    public bool IsBurning => _isBurning;

    public void Light() {
        _isBurning = true;
        _fire.SetActive(true);
        EnablePhysics();
        GetComponent<Collider>().enabled = true;
    }

    
}