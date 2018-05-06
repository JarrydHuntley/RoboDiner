using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAstronauts : MonoBehaviour
{
    public GameObject AstroNautPrefab;
    public GameObject TargetObject;
    public float MinTimeToSpawn = 3f;
    public float MaxTimeToSpawn = 6f;
    private float m_Timer = 3f;

    // Use this for initialization
    void Start()
    {
        m_Timer = Random.Range(MinTimeToSpawn, MaxTimeToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        m_Timer -= Time.deltaTime;
        if (m_Timer < 0f)
        {
            m_Timer = Random.Range(MinTimeToSpawn, MaxTimeToSpawn);
            GameObject newObject = (GameObject)Instantiate(AstroNautPrefab, this.transform.position, this.transform.rotation);
            newObject.GetComponent<Rigidbody>().AddForce((TargetObject.transform.position - this.transform.position) * 6f);
        }
    }
}
