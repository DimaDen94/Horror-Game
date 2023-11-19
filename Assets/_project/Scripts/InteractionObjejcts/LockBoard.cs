using UnityEngine;

public class LockBoard : InteractionObject
{
    private const int HitForce = 100;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private AudioSource _crashAudio;
    [SerializeField] private bool _destroy = false;

    public bool ISDestroy => _destroy;

    public override bool TryUse(HeroSlot slot)
    {
        if (slot.Thing is Hammer)
        {
            Hit();
            return true;
        }

        return false;
    }

    public void Hit()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _rigidbody.AddForce(Vector3.right * HitForce);
        _destroy = true;
        _crashAudio?.Play();
    }
}
