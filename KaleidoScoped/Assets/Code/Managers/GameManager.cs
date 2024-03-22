using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaleidoscoped
{
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
            if (killCounter.GetKills() >= 10)
            {
                sceneLoader.VictoryScreen();
            }

            if (timer.IsTimeUp())
            {
                sceneLoader.GameOverScreen();
            }
        }
    }
}