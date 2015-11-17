using UnityEngine;
using System.Collections;

public class KeyListener : MonoBehaviour {
	private Animator _animator;
	private float _speed = 0.0f; 
	private bool _called = false;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		bool space = false;

		if (Input.GetMouseButton(0)) space = true;

		_speed += Time.deltaTime;	
		if (!enabled) 
		{

		}
		_animator.SetBool("Space", space);
	}

	void OnLook () {
		//GetComponent<AudioSource>().Play();
	}
}
