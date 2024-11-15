using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] private float force=1;
    [SerializeField] private float maxSpeed=1;
    [SerializeField]private float lerpRate=1;

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
        
        if (Mathf.Abs(horizontalAxis) > 0.1f || Mathf.Abs(verticalAxis) > 0.1f)
        {
            inputPresent = true;
        }else
        {
            inputPresent = false;
        }
        Debug.Log("input present: "+inputPresent);

    }

    void FixedUpdate() {
        if (inputPresent)
        {
            Walk(horizontalAxis, verticalAxis);
        }else
        {
            Decelerate();
        }
    }

    void Walk(float xAxis,float zAxis)
    {
        // traditional velocity imposer
        // rigidbody.velocity = new Vector3(xVector, 0, zVector);

        Debug.Log("xAxis: " + xAxis + " zAxis: " + zAxis);
        // direction of vector 
        Vector3 forceDir = new Vector3(xAxis, 0, zAxis);

        // add force to rigidbody
        rb.AddForce(forceDir * force, ForceMode.Force);

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
