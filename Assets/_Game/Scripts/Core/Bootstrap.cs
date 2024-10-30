using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void OnEnable()
    {
        StartGame();
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);        
    }
}