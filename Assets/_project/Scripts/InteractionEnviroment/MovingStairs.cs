using System.Linq;
using DG.Tweening;
using UnityEngine;

public class MovingStairs : MonoBehaviour
{
    [SerializeField] private float _yCurrectOffetValue;
    [SerializeField] private Collider _collider;
    [SerializeField] private AudioSource _chainsAudio;

    private WeightPlatform[] _platforms;

    public float MoveDuration = 1;

    public void Construct(WeightPlatform[] platforms)
    {
        _platforms = platforms;

        foreach (var platorm in _platforms)
            platorm.WeightAdded += TryMove;

        
    }

    private bool AllPlatformFull() => _platforms.ToList().FindAll(platform => !platform.IsFull()).Count == 0;

    public void TryMove(string name)
    {
        float platformCount = _platforms.Length;
        float slotFullCount = _platforms.ToList().FindAll(platform => platform.IsFull()).Count;
        float currentOffset = _yCurrectOffetValue * slotFullCount / platformCount;

        if(transform.position.y != currentOffset) {
            transform.DOMoveY(currentOffset, MoveDuration);

            if (AllPlatformFull())
                _collider.enabled = false;

            _chainsAudio.Play();
        }

        
    }

    private void OnDestroy()
    {
        foreach (var platorm in _platforms)
            platorm.WeightAdded -= TryMove;
    }
}
