using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    // Outlets
    public Text counterText;
    
    // Tracking
    int kills;

    // Start is called before the first frame update
    void Start()
    {
        kills = 0;
        displayKills();
    }

    private void displayKills()
    {
        if(counterText == null)
        {
            counterText = GameObject.FindWithTag("KillCounterText").GetComponent<Text>();
        } else {
            counterText.text = kills.ToString();
        }
    }

    public void incrementKills()
    {
        kills++;
        displayKills();
    }
}
