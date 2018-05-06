using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaporizedAstronaut : MonoBehaviour {
    public Sprite frameA;
    public Sprite frameB;
    private float m_DeathTimer = 0.25f;

	// Use this for initialization
	void Start () {
        GetComponent<SpriteRenderer>().sprite = frameA;
    }
	
	// Update is called once per frame
	void Update () {
        m_DeathTimer -= Time.deltaTime;
        if(m_DeathTimer < 0.7f && GetComponent<SpriteRenderer>().sprite == frameA)
        {
            GetComponent<SpriteRenderer>().sprite = frameB;
        }
        if(m_DeathTimer <0)
        {
            GameObject.Destroy(this.gameObject);
        }
	}
}
