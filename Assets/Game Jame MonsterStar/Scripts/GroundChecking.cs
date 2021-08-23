using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecking : MonoBehaviour
{
    private PlayerControl pControl;
    
    public GameObject ground;

    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider)
        {
            pControl.isGrounded = true;
        }
    }

    public void OnCollisionExit2D(Collision2D _other)
    {
        if(_other.collider)
        {
            pControl.isGrounded = false;
        }
    }
    
}
