using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviour : MonoBehaviour
{

    private GameData gameData;
    private PlayerControl pCntrol;
    private Vector3 initialScale = Vector3.one;
    // Start is called before the first frame update
    void Start()
    {
        gameData = FindObjectOfType<GameData>();
        pCntrol = FindObjectOfType<PlayerControl>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(pCntrol.buildingHit)
        {
            float currentScale = gameObject.transform.localScale.y;
            
            gameObject.transform.localScale = new Vector3(initialScale.x, currentScale * 0.5f, initialScale.z);
            
            //gameObject.transform.localScale.Scale(new Vector3(1,currentScale* 0.25f *Time.deltaTime, 1));

            if(gameObject.transform.localScale.y <= 0.25f)
                Destroy(gameObject);

        }*/
    }

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

    public void OnDestroy()
    {
        gameData.score++;
    }
}
