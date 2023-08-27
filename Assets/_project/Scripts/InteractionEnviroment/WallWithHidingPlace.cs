using DG.Tweening;
using UnityEngine;

public class WallWithHidingPlace : MonoBehaviour
{
    [SerializeField] private GameObject _loot;
    [SerializeField] private Transform _damper;

    private const float _movingDistanceUp = 4;

    public void OpenDamper() {
        _damper.DOMove(Vector3.up * _movingDistanceUp, 2);
        _loot.SetActive(true);
    }
}
