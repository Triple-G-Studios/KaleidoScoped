using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaleidoscoped
{
    public class GameManager : MonoBehaviour
    {
        public KillCounter killCounter;
        public Timer timer;
        public string winningTeam;

        public SceneLoader sceneLoader;

        void Update()
        {
            CheckGameConditions();
        }

        void CheckGameConditions()
        {
            if (killCounter.DetermineWinner() == 1)
            {
                // Blue team won
                winningTeam = "Blue team";
                sceneLoader.VictoryScreen();
            } else if (killCounter.DetermineWinner() == 2)
            {
                // Red team won
                winningTeam = "Red team";
                sceneLoader.VictoryScreen();
            }

            /*if (killCounter.GetKills() >= 12)
            {
                sceneLoader.VictoryScreen();
            }*/

            if (timer.IsTimeUp())
            {
                sceneLoader.GameOverScreen();
            }
        }
    }
}