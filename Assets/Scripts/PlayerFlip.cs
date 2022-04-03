using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerFlip : MonoBehaviour
{
    Vector2 v2LocalPosStart;

    public bool canMove = true;
    void Start ()
    {
        v2LocalPosStart = transform.localPosition;
    }
	
    void Update ()
    {
        if (canMove)
        {
            Quaternion rotation = transform.rotation;
            if (Input.GetAxisRaw("Horizontal") > 0f)
            {
                transform.rotation = new Quaternion(rotation.x, 0, rotation.z , rotation.w);
            }
            else if( Input.GetAxisRaw("Horizontal") < 0.0f)
            {
                transform.rotation = new Quaternion(rotation.x, -180f, rotation.z , rotation.w);
            }    
        }
        
    }
}
