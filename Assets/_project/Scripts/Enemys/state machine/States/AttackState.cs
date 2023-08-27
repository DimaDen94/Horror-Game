using UnityEngine;
using UnityEngine.AI;

public class AttackState : State
{
    [SerializeField] private AudioSource _laugh;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void OnEnable()
    {
        if (Target == null)
            Target = GetComponent<EnemyStateMachine>().Target;

        _laugh.Play();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Enemy>().Animator;
        if (Target != null)
            _navMeshAgent.SetDestination(Target.gameObject.transform.position);
  

        if (_animator != null)
            _animator.SetTrigger("Attack");

        _navMeshAgent.velocity = Vector3.zero;
        Target.Death(transform);
    }

    //private void FaceTarget(Vector3 destination, float speed)
    //{
    //    Vector3 lookPos = destination - transform.position;
    //    lookPos.y = 0;
    //    Quaternion rotation = Quaternion.LookRotation(lookPos);
    //    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed);
        
    //}

}
