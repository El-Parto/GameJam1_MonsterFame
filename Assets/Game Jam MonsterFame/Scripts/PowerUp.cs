using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float timer = 3;
    private PlayerControl pCntrl;
    public Vector3 initialScale;
    public Vector3 currentScale;

    public bool countingDown = false;
    // Start is called before the first frame update
    void Start()
    {
        pCntrl = FindObjectOfType<PlayerControl>();
        initialScale = pCntrl.gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            countingDown = true;
        }
    }

    public void OnTriggerStay2D(Collider2D _collider)
    {
        
        if(_collider.gameObject.CompareTag("Player") && timer >= 0 )
        {
            currentScale = pCntrl.gameObject.transform.localScale;
            //Vector3 newScale = currentScale;
            
            _collider.gameObject.transform.localScale += (new Vector3((.01f) * Time.deltaTime, (.01f) * Time.deltaTime,(.01f) * Time.deltaTime));
            pCntrl.moveSpeed += 2.5f * Time.deltaTime;
            pCntrl.jumpHeight += 0.1f * Time.deltaTime;


        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            countingDown = false;
        }
    }

    public void Timer()
    {
        if(countingDown)
        {
            timer -= 1 * Time.deltaTime;
        }

        if(timer <= 0)
        {
            countingDown = false;
            Destroy(gameObject);
        }
            
    }

    private void OnDestroy()
    {
        timer = 3;
    }
}
