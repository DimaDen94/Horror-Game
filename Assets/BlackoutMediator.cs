using UnityEngine;

public class BlackoutMediator : MonoBehaviour
{
    private const string BlackoutAnimationKey = "Blackout";
    [SerializeField] private Animator _animator;

    private void Awake() => DontDestroyOnLoad(this);

    public void Backout() => _animator.SetBool(BlackoutAnimationKey, true);

    public void Daybreak() => _animator.SetBool(BlackoutAnimationKey, false);

    public void DestroyBlackout() => Destroy(gameObject);
}
