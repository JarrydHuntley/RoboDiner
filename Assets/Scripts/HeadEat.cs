using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadEat : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpaceDebris") || other.gameObject.CompareTag("Astronaut"))
        {
            Destroy(other.gameObject);
            FindObjectOfType<HeadAnimateNeckSwallow>().Swallow();
        }
    }
}
