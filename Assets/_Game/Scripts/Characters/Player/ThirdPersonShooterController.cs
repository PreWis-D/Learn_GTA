using Cinemachine;
using StarterAssets;
using UnityEngine;

public class ThirdPersonShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _aimCamera;
    [SerializeField] private float _defaultSensitivity = 1f;
    [SerializeField] private float _aimSensitivity = 0.5f;
    [SerializeField] private LayerMask _aimColliderLayerMask = new LayerMask();
    [SerializeField] private Transform _debugTransform;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _spawnBulletPosition;

    private ThirdPersonController _personController;
    private StarterAssetsInputs _inputs;

    private void Awake()
    {
        _personController = GetComponent<ThirdPersonController>();
        _inputs = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        if (_aimCamera)
        {
            Vector3 mouseWorldPosition = Vector3.zero;
            Vector2 screenCenterPoint = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out RaycastHit raycast, 999f, _aimColliderLayerMask))
            {
                _debugTransform.position = raycast.point;
                mouseWorldPosition = raycast.point;
            }

            if (_inputs.aim)
            {
            _aimCamera.gameObject.SetActive(true);
            _personController.SetSesitivity(_aimSensitivity);
            _personController.SetRotateOnMove(false);

                Vector3 worldAimTarget = mouseWorldPosition;
                worldAimTarget.y = transform.position.y;
                Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

                transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            }
            else
            {
                _aimCamera.gameObject.SetActive(false);
                _personController.SetSesitivity(_defaultSensitivity);
                _personController.SetRotateOnMove(true);
            }


            if (_inputs.shoot)
            {
                Vector3 aimDirection = (mouseWorldPosition - _spawnBulletPosition.position).normalized;
                Instantiate(_bulletPrefab, _spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
                _inputs.shoot = false;
            }
        }
    }
}