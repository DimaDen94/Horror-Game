using UnityEngine;
using UnityEngine.AI;

public class MoveState : State
{
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private AudioSource _laugh;
    private Animator _animator;

    private float _delay = 10;
    private float _time = 0;

    private void OnEnable()
    {
        _animator = GetComponent<Enemy>().Animator;
        _animator.SetTrigger("Walk");

        if (Target == null)
            Target = GetComponent<EnemyStateMachine>().Target;

        _time = _delay;
    }

    private void Update()
    {
        if (Target != null)
        {
            _navMeshAgent.SetDestination(Target.gameObject.transform.position);

            Debug.DrawLine(transform.position, Target.transform.position, Color.red);
        }

        if (_time > _delay)
        {
            _laugh.Play();
            _time = 0;
        }
        else
            _time += Time.deltaTime;
    }

}
