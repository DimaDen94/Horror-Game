using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    private const string AnimationAttackTrigger = "Attack";
    [SerializeField] private AudioSource _laugh;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private float _delay = 5;
    private float _timeCounter = 0;


    private void OnEnable()
    {
        if (Target == null)
            Target = GetComponent<EnemyStateMachine>().Target;

        _laugh.Play();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Enemy>().Animator;
        if (Target != null)
            _navMeshAgent.SetDestination(Target.gameObject.transform.position);

        _navMeshAgent.velocity = Vector3.zero;
        _navMeshAgent.isStopped = true;

        _timeCounter = _delay;

    }

    private void Update()
    {
        if (_timeCounter >= _delay)
        {
            _timeCounter = 0;

            if (_animator != null)
                _animator.SetTrigger(AnimationAttackTrigger);

            if (Target.GetComponent<IHitable>() != null)
                Target.GetComponent<IHitable>().Hit(transform);
        }
        _timeCounter += Time.deltaTime;
    }
}
