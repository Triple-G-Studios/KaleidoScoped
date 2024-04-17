using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;


namespace Kaleidoscoped
{
    public class CharacterSelection : NetworkBehaviour
    {
        // public Transform floatingInfo;

        [SyncVar]
        public int characterNumber = 0;

        /*public TextMesh textMeshName;
        [SyncVar(hook = nameof(HookSetName))]*/
        public string playerName = "";
        public int teamId = 1;
        public string weapon = "rifle";
        public Color paintColor = Color.blue;
        public int playerKills = 0;

        /*void HookSetName(string _old, string _new)
        {
            //Debug.Log("HookSetName");
            AssignName();
        }*/

        [SyncVar(hook = nameof(HookSetColor))]
        public Color characterColor;
        private Material cachedMaterial;
        public MeshRenderer[] characterRenderers;

        void HookSetColor(Color _old, Color _new)
        {
            //Debug.Log("HookSetColor");
            AssignColors();
        }

        public void AssignColors()
        {
            foreach (MeshRenderer meshRenderer in characterRenderers)
            {
                cachedMaterial = meshRenderer.material;
                cachedMaterial.color = characterColor;
            }
        }

        void OnDestroy()
        {
            if (cachedMaterial) { Destroy(cachedMaterial); }
        }

        /*public void AssignName()
        {
            textMeshName.text = playerName;
        }*/

        // To change server controlled sync vars, clients end Commands, and the hooks will fire
        // Although not used in this example, we could change some character aspects without replacing current prefab.
        //[Command]
        //public void CmdSetupCharacter(string _playerName, Color _characterColour)
        //{
        //    playerName = _playerName;
        //    characterColour = _characterColour;
        //}
    }
}
