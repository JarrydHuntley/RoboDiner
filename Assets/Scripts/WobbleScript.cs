using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleScript : MonoBehaviour
{



    private SpriteRenderer sr;
    [SerializeField]
    private Sprite[] sprites;

    [SerializeField]
    private float frameSpeed = 0.5f;
    private float _frameTimer = 0.0f;


    private int currentFrame;

    // Use this for initialization
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        _frameTimer += Random.Range(0.1f , 1.8f);
    }

    // Update is called once per frame
    void Update()
    {
        _frameTimer -= Time.deltaTime;

        //counting frames
        if (Mathf.Max(0.0f, _frameTimer) == 0.0f)
        {
            _frameTimer = frameSpeed;
            currentFrame = (currentFrame + 1) % sprites.Length;
            //Debug.Log(sprites[currentFrame]);
            sr.sprite = sprites[currentFrame];
        }
    }
}
