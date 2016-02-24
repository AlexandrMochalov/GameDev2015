using UnityEngine;
using System.Collections;

public class PacmanPlayer : PacmanCharacter {
    private Animator _anim;

    void Start () {
        _anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (_state == State.Die) return;

        if (Input.GetKeyDown(KeyCode.UpArrow) && ApplyNewState (State.Up)) 
            _anim.SetTrigger("Up");
        else if (Input.GetKeyDown(KeyCode.DownArrow) && ApplyNewState (State.Down)) 
            _anim.SetTrigger("Down");
        else if (Input.GetKeyDown(KeyCode.RightArrow) && ApplyNewState (State.Right)) 
            _anim.SetTrigger("Right");
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && ApplyNewState (State.Left)) 
            _anim.SetTrigger("Left");

        CheckForContinueMoving();

        transform.position += _velocity;
    }

    void OnTriggerEnter2D (Collider2D other)
    {
    }

    void OnTriggerExit2D(Collider2D other)
    {
    }
}
