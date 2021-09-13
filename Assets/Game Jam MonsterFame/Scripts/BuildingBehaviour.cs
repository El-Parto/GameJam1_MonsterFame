using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor.Experimental.GraphView;

using UnityEngine;

public class BuildingBehaviour : MonoBehaviour
{

    private GameData gameData;
    private PlayerControl pCntrol;
    private Vector3 initialScale = Vector3.one;

    [SerializeField] private Collider2D cityColl;

    [SerializeField] private Rigidbody2D cRB;

    [SerializeField] private LayerMask playerMsk;

    private Vector2 playerPos;

    [SerializeField] private Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        pCntrol = FindObjectOfType<PlayerControl>();
        cityColl = GetComponent<Collider2D>();
        cRB = GetComponentInChildren<Rigidbody2D>();
        playerMsk = LayerMask.GetMask("PlayerMSK");
    }

    // Update is called once per frame
    void Update()
    {
        onTouch();
    }

    /// <summary>
    /// When an object collides with the top of the building
    /// it will "crush" the object by shrinking it by half.
    /// Once the object reaches a quarter of its size, it destroys itself.
    /// </summary>
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Player"))
        {
            float currentScale = gameObject.transform.localScale.y;
            
           gameObject.transform.localScale = new Vector3(initialScale.x, currentScale * 0.5f, initialScale.z);
            
            //gameObject.transform.localScale.Scale(new Vector3(1,currentScale* 0.25f *Time.deltaTime, 1));

            
            if(gameObject.transform.localScale.y <= 0.25f)
                Destroy(gameObject);
            
            //instantiate a boom here
                
        }
    }

    /// <summary>
    /// Runs a check to see whether or not the building is touching the player using a raycast. 
    /// </summary>
    public bool Touched()
    {
        RaycastHit2D buildinghit = Physics2D.Raycast(gameObject.transform.position, Vector2.left, cityColl.bounds.extents.x + 5, playerMsk);

        Color rayColor;
        if(buildinghit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(cityColl.bounds.center, Vector2.left * (cityColl.bounds.extents.x + 5), rayColor);
        
        return buildinghit.collider != null;
    }
/// <summary>
/// performs a function to add force to the building to send it flying upon contact with the player
/// using the "Touched" bool function.
/// </summary>
    private void onTouch()
    {
        if(Touched())
        {
            gameObject.transform.Translate(Vector3.right *Time.deltaTime);
        }
    }
    
    
    public void OnDestroy()
    {
        gameData.score++;
    }
}
