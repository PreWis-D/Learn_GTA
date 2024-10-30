using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private BaseState _firstState;
    [SerializeField] private BaseState _alarmState;
    [SerializeField] private BaseState _seekState;
    [SerializeField] private BaseState _moveState;

    private BaseNPC _npc;
    protected BaseState _currentState;

    public BaseState CurrentState => _currentState;

    #region Core
    public void Init(BaseNPC npc)
    {
        _npc = npc;
        Reset(_firstState);
    }

    public void Activate()
    {
        if (_currentState == null)
            OnDied();
        else
            _currentState.enabled = true;
    }

    public void Deactivate()
    {
        if (_currentState == null)
            OnDied();
        else
            _currentState.enabled = false;
    }
    #endregion

    private void Update()
    {
        if (_currentState == null)
            return;

        var nextState = _currentState.GetNextState();
        if (nextState != null)
            Transit(nextState);
    }

    protected void Reset(BaseState startState)
    {
        _currentState = startState;
        
        if (_currentState != null)
            _currentState.Enter(_npc);
    }

    protected void Transit(BaseState nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_npc);
    }

    public void Alarm()
    {
        Transit(_alarmState);
    }

    public void Seek()
    {
        Transit(_seekState);
    }

    public void MoveToPoint()
    {
        Transit(_moveState);
    }

    public void OnDied()
    {
        gameObject.SetActive(false);
    }
}