using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceObjectRotation : MonoBehaviour
{
    private Vector3 m_rotationAmount = Vector3.zero;
    private Vector3 m_targetRotationAmount = Vector3.zero;
    private float m_speed = 0.1f;

    // Use this for initialization
	void Start () {
       m_rotationAmount = Vector3.zero;
       m_targetRotationAmount = Vector3.zero;
    }

    public void SetRotation(float rotationAmount, float speed)
    {
        m_targetRotationAmount.z = rotationAmount;
        m_speed = speed;
    }
	
	// Update is called once per frame
	void Update () {
        if(m_rotationAmount.z != m_targetRotationAmount.z)
        {
            m_rotationAmount = Vector3.Slerp(m_rotationAmount, m_targetRotationAmount, m_speed * Time.deltaTime);
            m_rotationAmount = m_targetRotationAmount;
        }
        transform.Rotate(0f, 0f, m_rotationAmount.z * Time.deltaTime * m_speed);
    }
}
