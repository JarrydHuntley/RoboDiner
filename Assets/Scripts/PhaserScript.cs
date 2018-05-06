using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]
public class PhaserScript : MonoBehaviour
{
    public bool MasterInstance = false;
    public float MinIdleTime = 2;
    public float MaxIdleTime = 6;
    public float MinWarmingTime = 0.5f;
    public float MaxWarmingTime = 2;
    public float MinFiringTime = 2;
    public float MaxFiringTime = 4;
    public float MinFiringScale = 0f;
    public float MaxFiringScale = 0.1f;
    public float WobbleSize = 0.01f;
    public LayerMask PhaserInteractionLayerMask;
    public AudioSource PhaserWarmupAudio;
    public AudioSource PhaserFireAudio;

    private PhaserTarget m_currentTarget;
    static private int m_targetIndex = 0;
    static private PhaserTarget[] m_potentialTargets = new PhaserTarget[1];
    static private PhaseState m_state;
    static private PhaseState m_lastState;
    static private float m_currentStateTime;
    static private float m_lineWidth;
    private bool m_wobbleOn = false;
    static private bool m_UseLastTargetLocation = false;
    private Vector3 m_lastTargetLocation;

    void Start()
    {
        m_UseLastTargetLocation = false;
        m_lineWidth = 0f;
        ChangeState(PhaseState.Idle);
    }

    void ChangeState(PhaseState state)
    {
        m_state = state;
        m_currentStateTime = GetTimeoutForState();
        //Debug.Log("Phaser: Changing state to: " + m_state.ToString());
        if (state == PhaseState.WarmingUp)
        {
            m_currentTarget = null;
        }
        this.GetComponentInParent<FancyDinerBehavior>().LaserStateChange(m_state);
    }

    void SelectTarget()
    {
        m_potentialTargets = FindObjectsOfType<PhaserTarget>();
        m_currentTarget = null;
        if (m_potentialTargets.Length > 0)
        {
            for (int x = 0; x < m_potentialTargets.Length; x++)
            {
                if (m_potentialTargets[x].tag == "Astronaut" || m_potentialTargets[x].tag == "Aestroid")
                {
                    //Debug.Log("Phaser: Target Found!");
                    m_targetIndex = x;
                    m_currentTarget = m_potentialTargets[x];
                    m_UseLastTargetLocation = false;
                }
            }
        }
        
        // If we can't find a target, then we return our state to idle (nothing to fire at)
        if (m_currentTarget == null)
        {
            //Debug.Log("Phaser: No Target Found!");
            ChangeState(PhaseState.Idle);
        }
    }

    void HandleStateChanges()
    {
        m_currentStateTime -= Time.deltaTime;
        if(m_currentStateTime < 0)
        {
            if(m_state == PhaseState.Idle)
            {
                PhaserFireAudio.Stop();
                PhaserWarmupAudio.Play();
                ChangeState(PhaseState.WarmingUp);
            }
            else if(m_state == PhaseState.WarmingUp)
            {
                PhaserWarmupAudio.Stop();
                PhaserFireAudio.Play();
                ChangeState(PhaseState.Firing);
                SelectTarget();
            }
            else
            {
                PhaserFireAudio.Stop();
                ChangeState(PhaseState.Idle);
            }
        }
    }

    float GetTimeoutForState()
    {
        if (m_state == PhaseState.WarmingUp)
        {
            return Random.Range(MinWarmingTime, MaxWarmingTime);
        }
        else if (m_state == PhaseState.Firing)
        {
            return Random.Range(MinFiringTime, MaxFiringTime);
        }
        else // if (m_state == PhaseState.Idle)
        {
            return Random.Range(MinIdleTime, MaxIdleTime);
        }
    }

    float GetWobble()
    {
        m_wobbleOn = !m_wobbleOn;
        if (m_wobbleOn)
            return WobbleSize;
        return 0f;
    }

    void HandleSizeChanges(float wobble)
    {
        if (m_state == PhaseState.Idle || m_state == PhaseState.PoweringDown)
        {
            Vector3 scale = this.transform.localScale;
            scale.x = Mathf.Lerp(scale.x, MinFiringScale, Time.deltaTime * 2f);
            scale.y = scale.x;
            this.transform.localScale = scale;
        }
        else if (m_state == PhaseState.WarmingUp)
        {
            Vector3 scale = this.transform.localScale;
            scale.x = Mathf.Lerp(scale.x, MaxFiringScale * 2f, Time.deltaTime);
            scale.y = scale.x;
            this.transform.localScale = scale;
        }
        else
        {
            Vector3 scale = this.transform.localScale;
            scale.x = Mathf.Lerp(scale.x, MaxFiringScale * 2f, 1f) + wobble;
            scale.y = scale.x;
            this.transform.localScale = scale;
        }
    }

    void HandlePhaserSizeChanges(float wobble)
    {
        if (m_state == PhaseState.Idle)
        {
            m_lineWidth = Mathf.Lerp(m_lineWidth, MinFiringScale, Time.deltaTime * 2f);
            GetComponent<LineRenderer>().startWidth = (m_lineWidth / 2f);
            GetComponent<LineRenderer>().endWidth = m_lineWidth;

        }
        else if (m_state == PhaseState.WarmingUp)
        {
            m_lineWidth = Mathf.Lerp(m_lineWidth, MinFiringScale, Time.deltaTime * 2f);
            GetComponent<LineRenderer>().startWidth = (m_lineWidth / 2f);
            GetComponent<LineRenderer>().endWidth = m_lineWidth;
        }
        else if (m_state == PhaseState.PoweringDown)
        {
            m_lineWidth = Mathf.Lerp(m_lineWidth, MinFiringScale, Time.deltaTime * 4f);
            GetComponent<LineRenderer>().startWidth = (m_lineWidth / 2f);
            GetComponent<LineRenderer>().endWidth = m_lineWidth;

        }
        else
        {
            m_lineWidth = Mathf.Lerp(m_lineWidth, MaxFiringScale, Time.deltaTime);
            GetComponent<LineRenderer>().startWidth = (m_lineWidth / 2f) + wobble;
            GetComponent<LineRenderer>().endWidth = m_lineWidth + wobble;
        }
    }

    void SetLineRendererPosition()
    {

        Vector3 targetLocation;
        if (!m_UseLastTargetLocation)
        {
            targetLocation = m_currentTarget.transform.position;
            m_lastTargetLocation = targetLocation;
        }
        else
        {
            targetLocation = m_lastTargetLocation;
        }
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        this.GetComponent<LineRenderer>().SetPosition(1, targetLocation);
    }

    void HandlePhaserTargetChanges()
    {
        if(m_state == PhaseState.Firing || m_state == PhaseState.Idle)
        {
            if(m_potentialTargets == null)
            {
                return;
            }
            else if (m_currentTarget == null && m_potentialTargets.Length - 1 >= m_targetIndex && m_potentialTargets[m_targetIndex] != null)
            {
                m_currentTarget = m_potentialTargets[m_targetIndex];
            }
            else if (m_potentialTargets[m_targetIndex] == null)
            {
                m_currentStateTime = 0f;
                return;
            }
            else if (m_currentTarget != m_potentialTargets[m_targetIndex])
            {
                SetLineRendererPosition();
            }
            else if (m_currentTarget == m_potentialTargets[m_targetIndex])
            {
                SetLineRendererPosition();
            }
            
        }
    }

    void SwitchTargetIfInRaycast()
    {
        if (m_state == PhaseState.WarmingUp || m_currentTarget == null)
            return;

        RaycastHit hit;
        //Debug.DrawRay(transform.position, m_currentTarget.transform.position - transform.position, Color.yellow);
        Physics.Raycast(transform.position, (m_currentTarget.transform.position - transform.position), out hit, PhaserInteractionLayerMask);
        if (hit.transform.gameObject != null)
        {
            
            PhaserTarget phaserTarget = hit.transform.gameObject.GetComponent<PhaserTarget>();
            if (phaserTarget != null && m_currentTarget != phaserTarget)
            {
                m_currentTarget = phaserTarget;
            }
        }
    }

    void HandlePhaserEffectsForTarget()
    {
        if(m_currentTarget != null && m_state == PhaseState.Firing)
        {
            m_currentTarget.HandlePhaserEffects(this);
        }
    }

    public void SetIdle()
    {
        ChangeState(PhaseState.PoweringDown);
        PhaserFireAudio.Stop();
        m_currentStateTime = Random.Range(3f, 6f);
        m_UseLastTargetLocation = true;
    }

	// Update is called once per frame
	void Update ()
    {
        float wobble = GetWobble();

        if (m_lastState != m_state && m_state == PhaseState.WarmingUp && m_lastState == PhaseState.Idle)
        {
            m_currentTarget = null;
            m_UseLastTargetLocation = false;
            PhaserFireAudio.Stop();
            PhaserWarmupAudio.Stop();
        }
        m_lastState = m_state;
        
        if(MasterInstance)
        {
            HandleStateChanges();
        }
        HandlePhaserTargetChanges();
        HandleSizeChanges(wobble);
        HandlePhaserSizeChanges(wobble);
        SwitchTargetIfInRaycast();
        HandlePhaserEffectsForTarget();
    }
}
