using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobbleScript : MonoBehaviour
{

    private SpriteRenderer sr;
    private Sprite[] sprites;

    private bool useTimingOffset = false;
    private float frameSpeed = 0.5f;
   // private float frameToggleSpeed = 0.5f;

    private float _frameTimer = 0.0f;
   // private float _frameToggle = 0.0f;

    private int currentFrame;
   // private bool secondFrame;

    //private int frameCount;

    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>();
        //frameCount = sprites.Length / 2;
    }

    // Update is called once per frame
    void Update()
    {
        _frameTimer -= Time.deltaTime;
        //_frameToggle -= Time.deltaTime;   

        //counting frames
        if (Mathf.Max(0.0f, _frameTimer) == 0.0f)
        {
            _frameTimer = frameSpeed;
            currentFrame = (currentFrame + 1) % sprites.Length;
        }

        //toggling frames
        //if (Mathf.Max(0.0f, _frameToggle) == 0.0f)
        //{
        //    _frameToggle = frameToggleSpeed;
        //}
    }
}
