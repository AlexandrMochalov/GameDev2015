using UnityEngine;
using System.Collections;

public class PacmanPlayer : PacmanCharacter {
    [SerializeField]
    private PacmanLevel _levelController;
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

    public void OnDetect (Collider2D other)
    {
        switch (other.tag)
        {
            case ("smallPoint"):
                other.gameObject.SetActive(false);
                _levelController.OnSmallPoint();
                break;
            case ("bigPoint"):
                other.gameObject.SetActive(false);
                _levelController.OnBigPoint();
                break;
            case ("enemy"):
                _levelController.OnCasper(other.GetComponent<Casper>());
                break;
            case "cherry":
                _levelController.OnCherry();
                other.gameObject.SetActive(false);
                break;
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
