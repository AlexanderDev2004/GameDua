using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidbody;
    public float MovementSpeed{ get; set;}
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");
        float verticalAxis = Input.GetAxisRaw("Vertical");
        
        if (horizontalAxis != 0 || verticalAxis != 0)
        {
            Walk(horizontalAxis, verticalAxis);
        }
    }

    void Walk(float xVector,float zVector)
    {
        rigidbody.velocity = new Vector3(xVector, 0, zVector);
    }
}
