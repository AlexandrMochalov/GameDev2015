using UnityEngine;
using System.Collections;

public class LayerAnim : MonoBehaviour {
    private Animator _anim;
    private float _speed = 0f;
    private float _step = 0.00125f;
    private bool _boom = false;
	// Use this for initialization
	void Start () {
        _anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (_boom) return;

        if (Input.GetKey(KeyCode.Space))
            _speed += _step;
        else
            _speed = Mathf.Max(_speed - _step, 0f);

        if (_speed >= 1f)
        {
            _anim.SetBool("Boom", true);
            _boom = true;
            Debug.Log("BOOM");
        }
        else
        {
            _anim.SetFloat("Speed", _speed);
           // Debug.Log(_speed);
        }
    }
}
