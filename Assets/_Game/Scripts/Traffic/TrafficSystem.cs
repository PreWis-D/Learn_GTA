using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;

public class TrafficSystem : MonoBehaviour
{
    [Header("NPCs")]
    [SerializeField] private Transform _npcsContainer;
    [SerializeField] private LiverNPC _npcPrefab;
    [SerializeField] private int _maxNpcsCount;
    [SerializeField] private float _cooldownSpawnNpc;

    [Space(10)]
    [Header("NPCs points")]
    [SerializeField] private MovePath[] _npcsMovePaths;
    [SerializeField] private Transform[] _idleNpcPoints;
    [SerializeField] private Transform[] _talkNpcPoints;

    [Space(10)]
    [Header("Cars")]
    [SerializeField] private Transform _carsContainer;
    [SerializeField] private Car _carPrefab;
    [SerializeField] private int _maxCarsCCount;
    [SerializeField] private float _cooldownSpawnCar;

    [Space(10)]
    [Header("Cars points")]
    [SerializeField] private MovePath[] _carsMovePaths;
    [SerializeField] private Transform[] _idleCarPoints;

    private List<Car> _cars = new();
    private List<LiverNPC> _movingNpcs = new();

    private bool _isActivate;

    private void Start()
    {
        _isActivate = true;

        if (_npcPrefab)
        {
            CreateNotMovingNPCs(LiverType.None, _idleNpcPoints);
            CreateNotMovingNPCs(LiverType.Talking, _talkNpcPoints);
            WorkSpawnMovingNpcs().Forget();
        }

        if (_carPrefab)
        {
            CreateNotMovingCars(_idleCarPoints);
            WorkSpawnMovingCars().Forget();
        }
    }

    #region NPCs
    private void CreateNotMovingNPCs(LiverType liverType, Transform[] spawnPoints)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var npc = Instantiate(_npcPrefab
                , spawnPoints[i].transform.position
                , spawnPoints[i].transform.rotation
                , _npcsContainer.transform);

            npc.Init(liverType);
            npc.Activate();
        }
    }

    private async UniTask WorkSpawnMovingNpcs()
    {
        while (_isActivate)
        {
            if (_movingNpcs.Count < _maxNpcsCount)
                SpawnMovingNPC();

            await UniTask.Delay((int)(_cooldownSpawnNpc * 1000));
        }
    }

    private void SpawnMovingNPC()
    {
        int random = Random.Range(0, _npcsMovePaths.Length);
        var npc = TryGetNpc();

        if (npc)
            RestartNpc(npc, _npcsMovePaths[random]);
        else
            CreateMovingNPC(LiverType.Moving, random);
    }

    private void RestartNpc(LiverNPC npc, MovePath movePath)
    {
        npc.gameObject.SetActive(true);
        npc.SetMovePoints(movePath);
        npc.transform.SetPositionAndRotation(
            movePath.MovePoints[0].transform.position,
            movePath.MovePoints[0].transform.rotation);
        npc.Activate();
    }

    private LiverNPC CreateMovingNPC(LiverType liverType, int movePathIndex)
    {
        var npc = Instantiate(_npcPrefab
            , _npcsMovePaths[movePathIndex].MovePoints[0].transform.position
            , _npcsMovePaths[movePathIndex].MovePoints[0].transform.rotation
            , _npcsContainer.transform);
        npc.Init(liverType);
        npc.SetMovePoints(_npcsMovePaths[movePathIndex]);
        npc.Activate();
        npc.LastPointReached += OnNPCLastPositionReached;
        _movingNpcs.Add(npc);
        return npc;
    }

    private LiverNPC TryGetNpc()
    {
        if (_movingNpcs.Count > 0)
        {
            foreach (var npc in _movingNpcs)
            {
                if (npc.gameObject.activeSelf == false)
                    return npc;
            }
        }

        return null;
    }

    private void OnNPCLastPositionReached(LiverNPC liverNPC)
    {
        liverNPC.gameObject.SetActive(false);
    }
    #endregion

    #region Cars
    private void CreateNotMovingCars(Transform[] spawnPoints)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            var car = Instantiate(_carPrefab
                , spawnPoints[i].transform.position
                , spawnPoints[i].transform.rotation
                , _npcsContainer.transform);

            car.Init();
        }
    }

    private async UniTask WorkSpawnMovingCars()
    {
        while (_isActivate)
        {
            if (_cars.Count < _maxCarsCCount)
                SpawnCar();

            await UniTask.Delay((int)(_cooldownSpawnCar * 1000));
        }
    }

    private void SpawnCar()
    {
        int random = Random.Range(0, _carsMovePaths.Length);
        var car = TryGetCar();

        if (car)
            RestartCar(car, _carsMovePaths[random]);
        else
            CreateMovingCar(random);
    }

    private void RestartCar(Car car, MovePath movePath)
    {
        car.gameObject.SetActive(true);
        car.SetMovePoints(movePath);
        car.transform.SetPositionAndRotation(
            movePath.MovePoints[0].transform.position,
            movePath.MovePoints[0].transform.rotation);
        car.Init();
        car.Activate();
    }

    private Car CreateMovingCar(int movePathIndex)
    {
        var car = Instantiate(_carPrefab
            , _carsMovePaths[movePathIndex].MovePoints[0].transform.position
            , _carsMovePaths[movePathIndex].MovePoints[0].transform.rotation
            , _carsContainer.transform);
        car.Init();
        car.SetMovePoints(_carsMovePaths[movePathIndex]);
        car.Activate();
        car.LastPointReached += OnCarLastPositionReached;
        _cars.Add(car);
        return car;
    }

    private Car TryGetCar()
    {
        if (_cars.Count > 0)
        {
            foreach (var car in _cars)
            {
                if (car.gameObject.activeSelf == false)
                    return car;
            }
        }

        return null;
    }

    private void OnCarLastPositionReached(Car car)
    {
        car.gameObject.SetActive(false);
    }
    #endregion

    private void OnDestroy()
    {
        foreach (var npc in _movingNpcs)
            npc.LastPointReached -= OnNPCLastPositionReached;

        foreach (var car in _cars)
            car.LastPointReached -= OnCarLastPositionReached;
    }
}