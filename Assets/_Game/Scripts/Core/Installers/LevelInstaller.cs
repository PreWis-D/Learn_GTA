using Reflex.Core;
using System;
using UnityEngine;

public class LevelInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private UIController _uiControllerPrefab;
    [SerializeField] private Player _playerPrefab;

    private ContainerBuilder _containerBuilder;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        _containerBuilder = containerBuilder;

        BindUIController();
        BindPlayer();
    }

    private void BindUIController()
    {
        _containerBuilder.AddSingleton(
            Instantiate(
            _uiControllerPrefab
            , Vector3.zero
            , Quaternion.identity
            , null));
    }

    private void BindPlayer()
    {
        _containerBuilder.AddSingleton(
            Instantiate(_playerPrefab, null));
    }
}