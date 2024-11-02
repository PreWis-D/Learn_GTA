using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] private string _interactionDescription;
    [SerializeField] private Transform _characterPoint;

    private Collider _collider;

    public string InteractionDescription => _interactionDescription;
    public Transform CharacterPoint => _characterPoint;

    public Animator Animator { get; private set; }

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        _collider = GetComponent<Collider>();
    }

    public void Execute()
    {
        _collider.enabled = false;
    }
}