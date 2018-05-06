using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour {
    public int AcceptableLosses = 1;
    public int Wins = 0;

    public void RecordLoss()
    {
        AcceptableLosses -= 1;
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
        GetComponent<TextMesh>().text = "Acceptable Patron Losses: " + AcceptableLosses.ToString();// + " Diners: " + Wins.ToString();
    }
}
