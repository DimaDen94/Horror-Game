using DG.Tweening;
using UnityEngine;

public class WallWithHidingPlace : MonoBehaviour
{
    [SerializeField] private Collider _loot;
    [SerializeField] private Transform _damper;

    private const float _movingDistanceUp = 4;

    public void OpenDamper() {
        _damper.DOLocalMoveY(_movingDistanceUp, 2);
        _loot.enabled = true;
    }

    public void CloseDamper()
    {
        _damper.DOLocalMoveY( -_movingDistanceUp, 2);
    }
}
