using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _speedRotateWheels;
    [SerializeField] private CarModel[] _carModels;

    private CarModel _currentCarModel;
    private MovePath _movePath;
    private bool _isMove;
    private int _currentPoint = 0;

    public event Action<Car> LastPointReached;

    public void Init()
    {
        int random = Random.Range(0, _carModels.Length);
        _currentCarModel = _carModels[random];

        for (int i = 0; i < _carModels.Length; i++)
            _carModels[i].gameObject.SetActive(false);

        _currentCarModel.gameObject.SetActive(true);
        _currentCarModel.Init(_speedRotateWheels);

    }

    public void SetMovePoints(MovePath movePath)
    {
        _movePath = movePath;
    }

    public void Activate()
    {
        _isMove = true;
        _currentCarModel.Activate();
    }

    public void Deactivate()
    {
        _isMove = false;
        _currentCarModel.Deactivate();
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
                    Deactivate();
                    _currentPoint = 0;
                    LastPointReached?.Invoke(this);
                }
            }
            else
            {
                _agent.SetDestination(_movePath.MovePoints[_currentPoint].transform.position);
            }
        }
    }
}