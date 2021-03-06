using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float timer = 3;
    private PlayerControl pCntrl;
    public Vector3 initialScale;
    public Vector3 currentScale;
    [SerializeField] private Text popUpText; 

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

    /// <summary>
    /// upon entering trigger zone, begin power up sequence by activating a bool
    /// </summary>
    /// <param name="other">The sample of collision collected from the incoming object.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            countingDown = true;
        }
        popUpText.gameObject.SetActive(true);
        popUpText.text = $"Powering Up... {timer.ToString("F0")}";
    }

    /// <summary>
    /// as long as you stay within the zone you "power up" more until the timer expires on the power up.
    /// </summary>
    /// <param name="_collider">Collider being detected by this Gameobject</param>
    public void OnTriggerStay2D(Collider2D _collider)
    {
        
        if(_collider.gameObject.CompareTag("Player") && timer >= 0 )
        {
            currentScale = pCntrl.gameObject.transform.localScale;
           
            
            _collider.gameObject.transform.localScale += (new Vector3((.01f) * Time.deltaTime, (.01f) * Time.deltaTime,(.01f) * Time.deltaTime));
            pCntrl.moveSpeed += 2.5f * Time.deltaTime;
            pCntrl.jumpHeight += 0.1f * Time.deltaTime;
            popUpText.text = $"Powering Up... {timer.ToString("F0")}";
        }
        
    }
/// <summary>
/// upon exiting, counting down is set to false.
/// </summary>
/// <param name="other">The collider that this gameObject's collider touches.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            countingDown = false;
        }

        popUpText.gameObject.SetActive(false);
    }


/// <summary>
/// Timer decreases in value if countingDown is True. This lets the powerup know when to "expire" and dissapear
/// </summary>
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

/// <summary>
/// upon being destroyed, value resets as a precaution.
/// </summary>
    private void OnDestroy()
    {
        timer = 3;
    }
}
