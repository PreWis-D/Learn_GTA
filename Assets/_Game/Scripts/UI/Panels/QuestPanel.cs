using System;
using TMPro;
using UnityEngine;

public class QuestPanel : BasePanel
{
    [SerializeField] private TMP_Text _questNpcName;
    [SerializeField] private TMP_Text _description;

    internal void SetDialogText(DialogConfig dialogConfig)
    {
        _questNpcName.text = dialogConfig.NameNPC;
        _description.text = dialogConfig.Text;
    }
}