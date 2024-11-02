using Reflex.Attributes;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;

    private Player _player;
    private CamerasController _camerasController;
    private UIController _uiController;

    [Inject]
    private void Construct(CamerasController camerasController, 
         UIController uIController, Player player)
    {
        _camerasController = camerasController;
        _uiController = uIController;
        _player = player;
    }

    private void Start()
    {
        _uiController.Init(_player);
        _player.transform.SetPositionAndRotation(
            _playerSpawnPoint.transform.position,
            _playerSpawnPoint.transform.rotation);
    }
}