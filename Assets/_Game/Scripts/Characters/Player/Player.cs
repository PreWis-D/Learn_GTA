using DG.Tweening;
using StarterAssets;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InteractableDetector _detector;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private StarterAssetsInputs _starterAssetsInputs;

    public ThirdPersonController ThirdPersonController { get; private set; }
    public WeaponBase CurrentWeapon {  get; private set; }

    private void Awake()
    {
        ThirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        _playerAnimator.Init(this);
        ThirdPersonController.Init(this);
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);
    }

    private void Update()
    {
        _detector.Update();

        if (CurrentWeapon != null)
            _playerAnimator.SetAim(_starterAssetsInputs.aim);
    }
}