using UnityEngine;

[CreateAssetMenu(fileName = "DialogConfig", menuName = "Configs/DialogConfig")]
public class DialogConfig : ScriptableObject
{
    [SerializeField] private string _nameNPC;
    [SerializeField][TextArea(3, 10)] private string _text;

    public string NameNPC => _nameNPC;
    public string Text => _text;
}