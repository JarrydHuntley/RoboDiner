using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadAnimateNeckSwallow : MonoBehaviour
{

    [SerializeField]
    private GameObject[] necks;
    private int currentNeck;
    [SerializeField]
    private float animateTime = 0.2f;
    private float _timer;

    private float neckScale = .5f;


    // Use this for initialization
    void Start()
    {
        currentNeck = -1;
        _timer = 0.0f;
    }


    // Update is called once per frame
    void Update()
    {
        AnimateSwallow();
        if (Input.GetButton("Fire1"))
        {
            Swallow();
        }
    }

    public void Swallow()
    {
        currentNeck = 0;
        _timer = animateTime;
    }

    public void AnimateSwallow()
    {
        _timer -= Time.deltaTime;

        if (Mathf.Max(0.0f, _timer) == 0.0f && currentNeck >= 0)
        {
            if (currentNeck == necks.Length)
            {
                necks[currentNeck - 1].transform.localScale = new Vector3(1, 1, 1);
                currentNeck = -1;
            }
            else
            {
                necks[currentNeck].transform.localScale += new Vector3(neckScale, neckScale, neckScale);

                for (int i = 0; i < necks.Length; i++)
                {
                    if (i == currentNeck)
                    {
                        continue;
                    }
                    else
                    {
                        necks[i].transform.localScale = new Vector3(1, 1, 1);
                    }
                }

                _timer = animateTime;
                currentNeck++;
            }
        }
    }
}
