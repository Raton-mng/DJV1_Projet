using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryScript : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1; 
    }

    public void Quit()
    {
        Application.Quit();
    }
}
