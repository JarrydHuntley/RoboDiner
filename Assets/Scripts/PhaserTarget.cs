using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class will handle phaser interactions when phaser is targeting the object (and no other object becomes a target of the phaser)
/// </summary>
public class PhaserTarget : MonoBehaviour {
    public GameObject DeathObject;
    public GameObject Shockwave;
    private float m_heat = 0.0f;
    private Color m_defaultColor;
    private bool m_isSafe = false;

    void Start()
    {
        m_isSafe = false;
        m_defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    public void MarkSafe()
    {
        m_isSafe = true;
    }
    
    public void HandlePhaserEffects(PhaserScript phaser)
    {
        m_heat += Time.deltaTime * 1.1f;
        //Debug.Log(gameObject.name + " heat: " + m_heat);
        Vector3 pos = this.transform.position;
        pos.x += GetWobble();
        pos.y += GetWobble();
        this.transform.position = pos;
        if(m_isSafe)
        {
            phaser.SetIdle();
        }
        else if (m_heat > 3.01f && DeathObject != null && Shockwave != null)
        {
            phaser.SetIdle();
            Instantiate(DeathObject, this.transform.position, this.transform.rotation);
            Instantiate(Shockwave, this.transform.position, this.transform.rotation); 
            GameObject.Destroy(this.gameObject);
        }
        
    }

    float GetWobble()
    {
        return Random.Range(0.0f, 0.01f);
    }

    void Update()
    {
        if(m_heat > 0f)
        {
            m_heat -= Time.deltaTime;
            Color newColor = m_defaultColor;
            newColor.r += m_heat;
            newColor.b -= m_heat;
            newColor.g -= m_heat;
            //Debug.Log(gameObject.name + " heat: " + m_heat);
            gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
        }
        else
        {
            m_heat = 0f;
            gameObject.GetComponent<SpriteRenderer>().material.color = m_defaultColor;
        }
    }
}
