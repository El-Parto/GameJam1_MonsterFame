using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    

    [SerializeField]private Rigidbody2D pRB;
    [SerializeField] private LayerMask lPlatMask; // for ground checking

    public bool canMove = true;
    public float moving = 1;
    public float moveSpeed = 20;
    public float jumpHeight =10;
    public float jumpSquatTimer;

    [SerializeField] private Collider2D chilColl;
    [SerializeField] private Collider2D pColl; // player collider
    public bool isGrounded = false;

    public float distToGround;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pRB= GetComponent<Rigidbody2D>();
        chilColl = GetComponentInChildren<Collider2D>();
        pColl = GetComponent<Collider2D>();
        lPlatMask = LayerMask.GetMask("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        pRB.velocity = new Vector2(moving, pRB.velocity.y);
        Movement();
        
        
    }

    public void Movement()
    {
        float currentGravity = pRB.gravityScale;
        // horizontal
        if (canMove)
        {
            float maxSpeed = Mathf.Lerp(0, moveSpeed, 1);

            moving = Input.GetAxisRaw("Horizontal") * (maxSpeed * Time.fixedDeltaTime);


        }
        //vertical
        if(canMove)
        {
            if(Input.GetButtonDown("Jump") && IsGrounded())
                pRB.AddForce(Vector2.up *jumpHeight, ForceMode2D.Impulse);
            
        }

        if(!IsGrounded())
        {
            pRB.velocity = new Vector2(moving * 0.5f, pRB.velocity.y);
        }


    }

    private bool IsGrounded()
    {
        float heightLenience = .01f;
        //using the thing below with the raycast, if you're too exact, just like using == instead of >= or <= it could cause problems
        RaycastHit2D raycastHit = Physics2D.Raycast(pColl.bounds.center, Vector2.down, pColl.bounds.extents.y + heightLenience, lPlatMask);
        //pColl.Raycast(Vector2.down, RaycastHit2D);
        Color rayColor;
        if(raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(pColl.bounds.center, Vector2.down * (pColl.bounds.extents));

        // this sets the bool for IsGrounded. If the raycast hits a collider that isn't null (or if the ray cast isn't touching a collider at the moment.
        return raycastHit.collider != null;
    }

    

}
