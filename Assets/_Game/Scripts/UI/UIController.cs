using UnityEngine;

public class UIController : MonoBehaviour
{
    private BasePanel[] _panels;

    public void Init(Player player)
    {
        _panels = GetComponentsInChildren<BasePanel>();

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
}