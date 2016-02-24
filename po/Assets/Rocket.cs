using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {
    private Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            anim.SetBool("Right", true);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            anim.SetBool("Right", false);

        anim.SetBool("Run", Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow));

        Vector3 point1 = new Vector3(1f, 1f, 1f);
        Vector3 point2 = new Vector3(2f, 3f, 4f);
        float distance = (point1 - point2).magnitude;
    }
}
