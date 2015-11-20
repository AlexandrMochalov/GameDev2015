using UnityEngine;
using System.Collections;

public class KrabController : MonoBehaviour {
    [SerializeField]
    private GameObject _bar;

	private Animator _anim;
	private float _position = 0f;
	private float _step = 0.01f;
	private float _edge = 5f;
    private int _score = 0;


	void Start () {
		_anim = GetComponent<Animator>();
        _bar.transform.localScale = new Vector3(0.5f, 1, 1);
        _bar.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0f, 1f);
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
        string tag = coll.gameObject.tag;
        if (tag == "ball" || tag == "ironBall")
        {
            _anim.SetTrigger("Hit");
		    coll.enabled = false;
            coll.GetComponent<Ball>().ConvertToScore();
            UpdateBar(tag == "ball");
        }
    }

    void UpdateBar(bool up) {
        _score += (up ? 10 : -10);
        _score = Mathf.Clamp(_score, -50, 50);
        float scale = Mathf.Clamp (((float)_score + 50f) / 100f, 0f, 1f);
        float red = 1f - scale;
        float green = scale;

        _bar.transform.localScale = new Vector3(scale, 1, 1);
        _bar.GetComponent<SpriteRenderer>().color = new Color(red, green, 0f, 1f);
    }
}
