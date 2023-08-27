using UnityEngine;

public class BlackoutMediator : MonoBehaviour
{
    private const string BlackoutAnimationKey = "Blackout";
    [SerializeField] private Animator _animator;

    private void Awake() => DontDestroyOnLoad(this);

    public void Backout()
    {
        gameObject.SetActive(true);
        _animator.SetBool(BlackoutAnimationKey, true);
    }

    public void Daybreak()
    {
        gameObject.SetActive(true);
        _animator.SetBool(BlackoutAnimationKey, false);
    }

    public void DestroyBlackout()
    {
        gameObject.SetActive(false);
    }
}
