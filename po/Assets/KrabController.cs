using UnityEngine;
using System.Collections;

public class KrabController : MonoBehaviour {
	private Animator _anim;
	private float _position = 0f;
	private float _step = 0.01f;
	private float _edge = 5f;


	void Start () {
		_anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		bool left = Input.GetKey(KeyCode.LeftArrow);
		bool right = !left && Input.GetKey(KeyCode.RightArrow);

		if(left)
			_position = Mathf.Max ((_position - _step), -1f);
		else if (right)
			_position = Mathf.Min ((_position + _step), 1f);

		_anim.SetBool("Run", left || right);
		transform.position = new Vector3((_position * _edge), -3.4f, 0f);
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "ball")
		{
			_anim.SetTrigger("Hit");
			coll.enabled = false;
		}
	}
}
