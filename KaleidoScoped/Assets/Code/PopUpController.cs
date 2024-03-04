using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kaleidoscoped {
    public class PopUpController : MonoBehaviour
    {
        public GameObject popupmenu;

        public void ClosePopup() => popupmenu.SetActive(false);
    }
}