using Reflex.Attributes;
using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private BasePanel[] _panels;

    public void Init()
    {
        var viewPanel = GetPanel(PanelType.View) as ViewPanel;
        viewPanel.Init();

        ShowPanel(PanelType.View);
    }

    public void ShowDialogPanel(DialogConfig dialogConfig)
    {
        ShowPanel(PanelType.Quest);
        var questPanel = GetPanel(PanelType.Quest) as QuestPanel;
        questPanel.SetDialogText(dialogConfig);
    }

    public void ShowViewPanel()
    {
        ShowPanel(PanelType.View);
    }

    private void AllPanelsHide()
    {
        for (int i = 0; i < _panels.Length; i++)
            _panels[i].Hide();
    }

    private void ShowPanel(PanelType panelType)
    {
        AllPanelsHide();
        GetPanel(panelType).Show();
    }

    private BasePanel GetPanel(PanelType panelType)
    {
        for (int i = 0; i < _panels.Length; i++)
        {
            if (_panels[i].PanelType == panelType)
                return _panels[i];
        }
        return null;
    }

    public void ShowQuestView(QuestViewConfig questViewConfig)
    {
        var viewPanel = GetPanel(PanelType.View) as ViewPanel;
        viewPanel.SetPresent(questViewConfig);
        viewPanel.SetDescription(questViewConfig);
        viewPanel.ShowPresentView();
        viewPanel.ShowDescriptionView();
    }

    public void ShowCountText(int currentValue, int targetValue)
    {
        var viewPanel = GetPanel(PanelType.View) as ViewPanel;
        viewPanel.SetCountText(currentValue, targetValue);
    }

    public void HidePresent()
    {
        var viewPanel = GetPanel(PanelType.View) as ViewPanel;
        viewPanel.HidePresentView();
    }
}