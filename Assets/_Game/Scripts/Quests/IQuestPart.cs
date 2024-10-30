using System;

public interface IQuestPart
{
    public event Action QuestCompleted;
    public void Init();
    public void Enter();
    public void OnCompleted();
}