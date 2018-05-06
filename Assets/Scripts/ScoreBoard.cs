using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreBoard : MonoBehaviour {
    public int AcceptableLosses = 8;
    public int Wins = 0;

    public void RecordLoss()
    {
        AcceptableLosses -= 1;
        if(AcceptableLosses <= 0)
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    public void RecordWin()
    {
        Wins += 1;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<TextMesh>().text = "Acceptable Patron Losses: " + AcceptableLosses.ToString() + "    Diners: " + Wins.ToString();
    }
}
