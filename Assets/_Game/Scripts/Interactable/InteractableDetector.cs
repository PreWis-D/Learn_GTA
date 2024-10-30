using System;
using UnityEngine;

[Serializable]
public class InteractableDetector
{
    [SerializeField] private Transform _viewPos;
    [SerializeField] private float _searchDist = 5f;
    [SerializeField] private LayerMask _layerMask;

    public bool IsActive { get; private set; } = true;
    public IInteractable Interactable { get; private set; }

    public void ChangeActiveState(bool isActive)
    {
        IsActive = isActive;

        if (IsActive == false)
            Interactable = null;
    }

    public void Update()
    {
        if (IsActive == false) return;

        var ray = new Ray(_viewPos.position, _viewPos.forward);
        Debug.DrawRay(_viewPos.position, _viewPos.forward, Color.red);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _searchDist, _layerMask))
        {
            var interactable = hit.transform.gameObject.GetComponentInParent<IInteractable>();

            if (interactable != null)
                Interactable = interactable;
            else
                Interactable = null;
        }
        else
        {
            Interactable = null;
        }
    }
}
