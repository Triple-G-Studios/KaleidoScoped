using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Kaleidoscoped
{
    public class MenuController : MonoBehaviour
    {
        public void loadGame() => SceneManager.LoadScene("JoeyScene");
    }
}
