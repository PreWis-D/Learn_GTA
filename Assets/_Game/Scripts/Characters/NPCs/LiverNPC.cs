using System;
using UnityEditorInternal.VR;
using UnityEngine;

public class LiverNPC : BaseNPC
{
    private LiverType _type;
    private MovePath _movePath;
    private bool _isMove;
    private int _currentPoint = 0;

    public LiverType Type => _type;

    public event Action<LiverNPC> LastPointReached;

    public void Init(LiverType type)
    {
        _type = type;
    }

    public void SetMovePoints(MovePath movePath)
    {
        _movePath = movePath;
    }

    public void Activate()
    {
        switch (Type)
        {
            case LiverType.Moving:
                Agent.speed = 2;
                _isMove = true;
                _animator.Move(_isMove);
                break;
            case LiverType.Talking:
                _animator.LiverTalk();
                break;
        }
    }

    public void Deactivate()
    {
        _isMove = false;
        _animator.Move(_isMove);
    }

    private void Update()
    {
        if (_isMove)
        {
            if (Vector3.Distance(transform.position, _movePath.MovePoints[_currentPoint].transform.position) < 0.5f)
            {
                _currentPoint++;

                if (_currentPoint >= _movePath.MovePoints.Length)
                {
                    _currentPoint = 0;
                    Agent.speed = 0;
                    Deactivate();
                    LastPointReached?.Invoke(this);
                }
            }
            else
            {
                Agent.SetDestination(_movePath.MovePoints[_currentPoint].transform.position);
            }
        }
    }
}

public enum LiverType
{
    None,
    Talking,
    Moving
}