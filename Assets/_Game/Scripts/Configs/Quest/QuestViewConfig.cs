using UnityEngine;

[CreateAssetMenu(fileName = "QuestViewConfig", menuName = "Configs/QuestViewConfig")]
public class QuestViewConfig : ScriptableObject
{
    [SerializeField] private Sprite _presentSprite;   
    [SerializeField] private int _targetCount;   
    [SerializeField] private string _description;

    public Sprite PresentSprite => _presentSprite;
    public int TargetCount => _targetCount;
    public string Description => _description;
}