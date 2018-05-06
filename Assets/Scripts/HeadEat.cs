using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEat : MonoBehaviour
{
    public Transform MoveEatingObjectsHere;
    private GameObject m_eatingTarget;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(m_eatingTarget != null)
        {
            m_eatingTarget.transform.position = Vector3.Lerp(m_eatingTarget.transform.position, MoveEatingObjectsHere.transform.position, Time.deltaTime * 6f);
            m_eatingTarget.transform.localScale = Vector3.Lerp(m_eatingTarget.transform.localScale, Vector3.zero, Time.deltaTime * 3f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpaceDebris") || other.gameObject.CompareTag("Astronaut"))
        {
            m_eatingTarget = other.gameObject;
            if(m_eatingTarget.GetComponent<CapsuleCollider>())
            {
                m_eatingTarget.GetComponent<CapsuleCollider>().enabled = false;
            }
            if(m_eatingTarget.GetComponent<PhaserTarget>())
            {
                m_eatingTarget.GetComponent<PhaserTarget>().MarkSafe();
            }
            
            Destroy(other.gameObject, 1f);
            FindObjectOfType<HeadAnimateNeckSwallow>().Swallow();
        }
    }
}
