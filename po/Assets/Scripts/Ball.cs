using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "ground") 
        {
            Destroy(gameObject);
        }
    }
}
