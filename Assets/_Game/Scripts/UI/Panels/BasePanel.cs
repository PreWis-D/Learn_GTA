using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasePanel : MonoBehaviour
{
    [SerializeField] private PanelType _panelType;
    [SerializeField] private Transform _panel;

    public PanelType PanelType => _panelType;

    public void Show()
    { 
        _panel.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _panel.gameObject.SetActive(false);
    }
}

public enum PanelType
{
    None,
    Quest,
    View
}