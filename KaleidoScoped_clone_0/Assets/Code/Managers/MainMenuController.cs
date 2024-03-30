using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaleidoscoped
{
    public class MainMenuController : MonoBehaviour
    {
        public void loadGame() => SceneManager.LoadScene("LobbyLanding");
    }
}
