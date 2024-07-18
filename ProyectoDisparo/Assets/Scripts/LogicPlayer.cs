using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicPlayer : MonoBehaviour
{    
    public float velocityMovement = 5.0f;

    private Rigidbody rb;
    private Animator anim;
    private float x, y;
    Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        movement = new Vector3(x, 0f, y);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);    
    }

    void FixedUpdate()
    {
        MovePlayer(movement);
    }

    void MovePlayer(Vector3 direction)
    {
        rb.MovePosition(transform.position + direction * velocityMovement * Time.deltaTime);
    }
}
