using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FancyDinerBehavior : MonoBehaviour {
    public GameObject TargetA;
    public GameObject TargetB;

    private GameObject m_currentTarget;

	// Use this for initialization
	void Start () {
        m_currentTarget = TargetA;

    }

    public void LaserStateChange(PhaseState state)
    {
        if(state == PhaseState.Idle || state == PhaseState.PoweringDown)
        {
            m_currentTarget = TargetA;
        }
        else if (state == PhaseState.WarmingUp)
        {
            m_currentTarget = TargetB;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.position = Vector3.Slerp(this.transform.position, m_currentTarget.transform.position, Time.deltaTime);
	}
}
