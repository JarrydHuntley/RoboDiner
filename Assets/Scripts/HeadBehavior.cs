using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBehavior : MonoBehaviour
{

    private Rigidbody rb;
    private float _timer;

    [SerializeField]
    private float minMoveTime = 1.0f;
    [SerializeField]
    private float maxMoveTime = 10.0f;
    [SerializeField]
    private float movementForce = 10.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveHead();
    }

    private void MoveHead()
    {
        _timer -= Time.deltaTime;

        if (Mathf.Max(0.0f, _timer) == 0.0f)
        {
            Debug.Log("Move head");
            Vector3 headPosition = rb.transform.position;

            //mineX = centerX + radius * cos(angle)
            //mineY = centerY + radius * sin(angle)

            int randAngle = Random.Range(0, 360);
            float newX = headPosition.x + movementForce * Mathf.Cos(randAngle);
            float newY = headPosition.y + movementForce * Mathf.Sin(randAngle);

            Vector3 movement = new Vector3(newX, newY, 0.0f);
            Debug.Log(movement);
            rb.AddForce(movement,ForceMode.Impulse);

            _timer = Random.Range(minMoveTime, maxMoveTime);
        }
    }
}
