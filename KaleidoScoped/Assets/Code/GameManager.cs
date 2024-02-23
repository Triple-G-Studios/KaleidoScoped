using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public KillCounter killCounter;
    public Timer timer;

    public SceneLoader sceneLoader;

    void Update()
    {
        CheckGameConditions();
    }

    void CheckGameConditions()
    {
        if (killCounter.GetKills() >= 12)
        {
            sceneLoader.VictoryScreen();
        }

        if (timer.IsTimeUp())
        {
            sceneLoader.GameOverScreen();
        }
    }
}
