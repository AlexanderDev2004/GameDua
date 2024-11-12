using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public float speed;
    public float GroundDist;

    public LayerMask teraainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castpos = transform.position;
        castpos.y += 1;
        if(Physics.Raycast(castpos, -transform.up, out hit, Mathf.Infinity, teraainLayer)) 
        {
            if (hit.collider != null) 
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + GroundDist;
                transform.position = movePos;
            }
        }

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("vertical");
        Vector3 moveDir = new Vector3 (x, 0, y);
        rb.velocity = moveDir * speed;


        if (x != 0 &&  x < 0) {
            sr.flipX = true;
        } else if (x != 0 && x > 0) {
            sr.flipX = false;
        }
    }
}
