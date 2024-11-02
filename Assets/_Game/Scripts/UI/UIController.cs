using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private ViewPanel _viewPanel;
    [SerializeField] private QuestPanel _questPanel;

    private BasePanel[] _panels;

    public void Init(Player player)
    {
        _panels = GetComponentsInChildren<BasePanel>();

        _viewPanel.Init(player);

        ShowPanel(PanelType.View);
    }

    public void ShowDialogPanel(DialogConfig dialogConfig)
    {
        ShowPanel(PanelType.Quest);
        _questPanel.SetDialogText(dialogConfig);
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
        _viewPanel.SetPresent(questViewConfig);
        _viewPanel.SetDescription(questViewConfig);
        _viewPanel.ShowPresentView();
        _viewPanel.ShowDescriptionView();
    }

    public void ShowCountText(int currentValue, int targetValue)
    {
        _viewPanel.SetCountText(currentValue, targetValue);
    }

    public void HidePresent()
    {
        _viewPanel.HidePresentView();
    }
}