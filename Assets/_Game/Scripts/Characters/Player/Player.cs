using StarterAssets;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InteractableDetector _detector;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private StarterAssetsInputs _starterAssetsInputs;

    public ThirdPersonController ThirdPersonController {  get; private set; }

    private void Awake()
    {
        ThirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        _starterAssetsInputs.InteractableButtonClicked += TryUseInteractable;

        _playerAnimator.Init(this);
        ThirdPersonController.Init(this);
        ThirdPersonController.SetActiveControllState(ActiveControllState.None);
    }

    private void Start()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.Camera);
    }

    private void TryUseInteractable()
    {
        if (_detector.Interactable == null) return;

        switch (_detector.Interactable)
        {
            case QuestNPC questNPC:
                ThirdPersonController.SetActiveControllState(ActiveControllState.None);
                break;
        }

        _detector.Interactable.Execute();
    }

    private void Update()
    {
        _detector.Update();
    }

    internal void OnRespawnFinished()
    {
        ThirdPersonController.SetActiveControllState(ActiveControllState.All);
        _detector.ChangeActiveState(true);
    }

    private void OnDestroy()
    {
        _starterAssetsInputs.InteractableButtonClicked -= TryUseInteractable;
    }
}