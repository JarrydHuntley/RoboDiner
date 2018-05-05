﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach to object (probably an invisible one) that we want to follow user input,
/// whether mouse or touch behavior.
/// </summary>
public class InputTracking : MonoBehaviour {

    public float Speed = 1f;
    private float m_InputZOffset = 10f;

    private Camera m_Camera;

	// Use this for initialization
	void Start () {
        m_Camera = FindObjectOfType<Camera>();
    }

    private bool HandleMouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            FollowInputPosition(Input.mousePosition);
            return true;
        }
        return false;
    }

    private bool HandleTouchInput()
    {
        if(Input.touchCount == 1)
        {
            FollowInputPosition(Input.GetTouch(0).position);
            return true;
        }
        return false;
    }

    private void FollowInputPosition(Vector3 targetPosition)
    {
        targetPosition.z = m_InputZOffset;
        //GetComponent<Rigidbody2D>().
        targetPosition = m_Camera.ScreenToWorldPoint(targetPosition);
        if (Vector3.Distance(transform.position, targetPosition) > 1.0f)
        {
            this.transform.LookAt(targetPosition);
            GetComponent<Rigidbody2D>().AddRelativeForce(Vector3.forward * Speed, ForceMode2D.Force);
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        bool mouseInputRx = HandleMouseInput();
        bool touchInputRx = HandleTouchInput();
        if(!mouseInputRx && !touchInputRx)
        {
            this.transform.position = new Vector3(-100, -100, 0f);
        }
    }
}