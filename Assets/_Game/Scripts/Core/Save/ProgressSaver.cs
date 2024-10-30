using UnityEngine;

public class ProgressSaver
{
    private const string _currentQuestSave = "CurrentQuestSave";

    public int CurrentQuestIndex { get; private set; }

    public ProgressSaver()
    {
        CurrentQuestIndex = PlayerPrefs.GetInt(_currentQuestSave);
    }

    public void OnProgressReset()
    {
        CurrentQuestIndex = 0;
        PlayerPrefs.SetInt(_currentQuestSave, CurrentQuestIndex);
    }

    public void OnQuestFinished()
    {
        CurrentQuestIndex++;
        PlayerPrefs.SetInt(_currentQuestSave, CurrentQuestIndex);
    }
}