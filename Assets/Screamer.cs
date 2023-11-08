using DG.Tweening;
using UnityEngine;

public class Screamer : MonoBehaviour
{
    private const float JumpDurotation = 0.5f;
    private const float ScaleDurotation = 0.5f;
    [SerializeField] private Animator _animator;
    private Vector3 _offset = new Vector3(0,1.6f,0);

    public void JumpTo(Transform target)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOMove(target.position + _offset, JumpDurotation));
        sequence.Join(transform.DOScale(Vector3.one * 3, ScaleDurotation));
        sequence.AppendCallback(()=> { Destroy(gameObject); });
    }
}
