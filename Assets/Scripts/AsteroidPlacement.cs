using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPlacement : MonoBehaviour {

	// Use this for initialization
	void Start () {
        float rotation = Random.Range(0f, 360f);
        transform.Rotate(0f, 0f, rotation);
        int orderingLayer = (int)Random.Range(0f, 5f);
        GetComponent<SpriteRenderer>().sortingOrder = orderingLayer;
        float scale = Random.Range(0.6f, 1.4f);
        Vector3 newScale = this.transform.localScale;
        newScale.x = scale;
        newScale.y = scale;
        this.transform.localScale = newScale;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
