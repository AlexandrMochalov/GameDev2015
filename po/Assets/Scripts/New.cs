using UnityEngine;
using System.Collections;
using System;

public class New : MonoBehaviour {
    public UnityEngine.Events.UnityEvent s;
    
    private int _ground = 0;
    private int _wall = 0;

    void OnTriggerEnter2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "groud":
                _ground += 1;
                break;
            case "wall":
                _wall += 1;
                break;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        switch (coll.gameObject.tag)
        {
            case "groud":
                _ground -= 1;
                break;
            case "wall":
                _wall -= 1;
                break;
        }
    }
    
    //check if we can jum
    bool CanJump() {
        return _ground > 0;
    }

    //check if we can walk
    bool CanWalkToRight() {
        return _wall <= 0;
    }
    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && CanJump())
        { 
            //Do Jump
        }

        if (Input.GetKey(KeyCode.RightArrow) && CanWalkToRight())
        {
            //Do go right
        }
    }
}
