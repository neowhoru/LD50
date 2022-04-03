using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public void DisableSpriteAndDelayDestroyObject()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(gameObject, 3);
    }
}
