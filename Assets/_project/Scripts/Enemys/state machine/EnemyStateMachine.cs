using UnityEngine;
public class EnemyStateMachine : MonoBehaviour
{
    private Transform _target;

    private State _currentState;

    [SerializeField] private State _firstState;

    public State CurrentState => _currentState;

    public Transform Target { get => _target; set => _target = value; }


    public void SetTarget(Transform hero)
    {
        _target = hero;
    }

    private void Awake()
    {
        _currentState = _firstState;
    }


    private void Update()
    {
        if (_currentState == null)
            return;
        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
        {
            _currentState.Exit();
        }
        _currentState = nextState;
        if (_currentState != null)
        {
            _currentState.Enter(_target);
        }
    }
}
