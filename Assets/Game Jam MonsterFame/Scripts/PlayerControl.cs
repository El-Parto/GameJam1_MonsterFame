using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    
    // for checking the current position of the player in the building Behaviour script
    public Vector2 playerOffset;
    
    [SerializeField] private GameObject cameraShakeGO;
    [SerializeField] private bool shaking;

    [SerializeField]private Rigidbody2D pRB;
    [SerializeField] private LayerMask lPlatMask; // for ground checking
    [SerializeField] private LayerMask buildMask; // for checking if we hit a building

    
    
    public RaycastHit2D buildingHit;

    public bool canMove = true;
    public float moving = 1;
    public float moveSpeed = 275.3f;
    public float jumpHeight =29.4f;
    public float jumpSquatTimer;
    [SerializeField] private bool isTackling;
    [SerializeField] private bool canTaunt;
    public bool isTaunting;
    [SerializeField]private bool canScale; 
    [SerializeField] private bool isMoving;
    
    [SerializeField] private Collider2D chilColl;
    [SerializeField] private Collider2D pColl; // player collider
        //public bool isGrounded = false;

    //public float distToGround;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pRB= GetComponent<Rigidbody2D>();
        chilColl = GetComponentInChildren<Collider2D>();
        pColl = GetComponent<Collider2D>();
        lPlatMask = LayerMask.GetMask("Platform");
        buildMask = LayerMask.GetMask("Building");

    }

    // Update is called once per frame
    void Update()
    {
        pRB.velocity = new Vector2(moving, pRB.velocity.y);
        Movement();
        Taunt();
        playerOffset = gameObject.transform.localPosition;

        if(moving >= 0.1f || !IsGrounded() || moving <= -0.1f)
            isMoving = true;
        else
            isMoving = false;
        
        
        
    }
/// <summary>
/// Makes the player move based on input (Horizontal and Jump)
/// also checks whether or not it's possible to do these actions by checking a bool
/// </summary>
    public void Movement()
    {
        float currentGravity = pRB.gravityScale;
        // horizontal
        if(canMove)
        {
            float maxSpeed = Mathf.Lerp(0, moveSpeed, 1);

            moving = Input.GetAxisRaw("Horizontal") * (maxSpeed * Time.fixedDeltaTime);

        }

        //vertical
        if(canMove)
        {
            if(Input.GetButtonDown("Jump") && IsGrounded())
            {
                pRB.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
                isMoving = true;
            }


        }
        else
            isMoving = false;

        if(!IsGrounded())
        {
            pRB.velocity = new Vector2(moving * 0.5f, pRB.velocity.y);
        }


    }

    /// <summary>
    /// the raycast that checks whether the player is on the ground.
    /// The ground is determined by colliding with a layermasked collider
    /// </summary>
    /// <returns> the collider that the Raycast makes contact with.</returns>
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
        Debug.DrawRay(pColl.bounds.center, Vector2.down * (pColl.bounds.extents), rayColor);

        // this sets the bool for IsGrounded. If the raycast hits a collider that isn't null (or if the ray cast isn't touching a collider at the moment.
        return raycastHit.collider != null;
    }

    public bool BodySlammed()
    {
        float heightLenience = .01f;
        //using the thing below with the raycast, if you're too exact, just like using == instead of >= or <= it could cause problems
        buildingHit = Physics2D.Raycast(pColl.bounds.center, Vector2.down, pColl.bounds.extents.y + heightLenience, buildMask);
        //pColl.Raycast(Vector2.down, RaycastHit2D);
        Color rayColor;
        if(buildingHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(pColl.bounds.center, Vector2.down * (pColl.bounds.extents), rayColor);

        // this sets the bool for IsGrounded. If the raycast hits a collider that isn't null (or if the ray cast isn't touching a collider at the moment.
        return buildingHit.collider != null;
    }
    
    


    /// <summary>
    /// The user performs a taunt that allows them to increase their score
    /// by pressing the down key for cerain amount of time.
    /// </summary>
    public void Taunt()
    {
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            
        }

    }

    public bool IsScaled()
    {
        if(cameraShakeGO.transform.localPosition.x >= 10)
            return true;

        return false;
    }   
    /*
    public IEnumerator CameraShake()
    {
        
        float yPos;
        bool scaled = false;
        cameraShakeGO.transform.Translate(new Vector3(0,(2f)*Time.deltaTime,0));

        //yield return WaitUntil(() => cameraShakeGO.transform.localScale.x >= 10);
    }*/
}
