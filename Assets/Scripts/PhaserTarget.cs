using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///  This class will handle phaser interactions when phaser is targeting the object (and no other object becomes a target of the phaser)
/// </summary>
public class PhaserTarget : MonoBehaviour {
    private float m_heat = 0.0f;
    private Color m_defaultColor;

    void Start()
    {
        m_defaultColor = gameObject.GetComponent<SpriteRenderer>().color;
    }
    
    public void HandlePhaserEffects(PhaserScript phaser)
    {
        m_heat += Time.deltaTime * 1.1f;
        Debug.Log(gameObject.name + " heat: " + m_heat);
        if (m_heat > 2.49f)
        {
            GameObject.Destroy(this.gameObject);
            phaser.SetIdle();
        }
        
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
            Debug.Log(gameObject.name + " heat: " + m_heat);
            gameObject.GetComponent<SpriteRenderer>().material.color = newColor;
        }
        else
        {
            m_heat = 0f;
            gameObject.GetComponent<SpriteRenderer>().material.color = m_defaultColor;
        }
    }
}
