using Reflex.Core;
using UnityEngine;

public class SystemInstaller : MonoBehaviour, IInstaller
{
    [SerializeField] private CamerasController _camerasController;

    private ContainerBuilder _containerBuilder;

    public void InstallBindings(ContainerBuilder containerBuilder)
    {
        _containerBuilder = containerBuilder;

        BindCamerasSystem();
    }

    private void BindCamerasSystem()
    {
        _containerBuilder.AddSingleton(
            Instantiate(
            _camerasController
            , Vector3.zero
            , Quaternion.identity
            , null));
    }
}