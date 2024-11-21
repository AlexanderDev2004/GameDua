using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] Animator animator;
    [SerializeField] private float forceOfWalking = 100;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float lerpRate = 5.6f;

    float horizontalAxis;
    float verticalAxis;
    bool inputPresent;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        inputPresent = Mathf.Abs(horizontalAxis) > 0.1f || Mathf.Abs(verticalAxis) > 0.1f;
        // Debug.Log("input present: "+inputPresent);

        if (inputPresent) Flip();
    }

    void Flip()
    {
        if (horizontalAxis > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalAxis < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void FixedUpdate()
    {
        if (inputPresent)
        {
            Walk(horizontalAxis, verticalAxis);
        }
        else
        {
            Decelerate();
        }
    }

    void Walk(float xAxis, float zAxis)
    {
        // traditional velocity imposer
        // rigidbody.velocity = new Vector3(xVector, 0, zVector);

        Debug.Log("xAxis: " + xAxis + " zAxis: " + zAxis);
        // direction of vector 
        Vector3 forceDir = new Vector3(xAxis, 0, zAxis);

        // add force to rigidbody
        rb.AddForce(forceDir * forceOfWalking, ForceMode.Force);

        // speed cap
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = rb.velocity.normalized * maxSpeed;
    }

    void Decelerate()
    {
        // isolate y-velocity
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        // interpolate
        horizontalVelocity = Vector3.Lerp(horizontalVelocity, Vector3.zero, lerpRate * Time.fixedDeltaTime);
        // apply new velocity
        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
}
