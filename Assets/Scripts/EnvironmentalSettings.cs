using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSettings : MonoBehaviour
{

    public float GravityX = 0f;
    public float GravityY = 0f;
    public float GravityZ = 0f;
    private Vector3 m_Gravity;
    private Camera m_Camera;

    // Use this for initialization
    void Start()
    {
        m_Camera = FindObjectOfType<Camera>();
    }

    void SetupOrthoGraphicCameraWidth()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float camHalfHeight = m_Camera.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;
        m_Camera.orthographicSize = camWidth;
    }

    // Update is called once per frame
    void Update()
    {
        m_Gravity.x = GravityX;
        m_Gravity.y = GravityY;
        m_Gravity.z = GravityZ;
        Physics.gravity = m_Gravity;
    }
}
