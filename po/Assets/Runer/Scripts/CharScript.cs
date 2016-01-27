using UnityEngine;
using System.Collections;

public class CharScript : MonoBehaviour {
	public float JumpForce;
	public float Speed;

	public GameObject Explosion;

	private Rigidbody2D rb;
	private bool isGrounded = false;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
	}

	void Update () {
		if (Input.anyKeyDown && isGrounded)
		{
			rb.AddForce(Vector2.up * JumpForce);
			isGrounded = false;
		}

		if (transform.position.y < 0)
			Deth();
	}

	void Deth ()
	{
		Explosion.transform.position = transform.position;
		Explosion.SetActive(true);

		rb.velocity = Vector2.zero;
		gameObject.SetActive(false);

	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (collision.gameObject.tag == "ground")
		{
			isGrounded = true;
			rb.velocity = Vector2.right * Speed;
		} 
		else if (collision.gameObject.tag == "enemy")
		{
			Deth ();
		} else {
			rb.velocity = Vector2.zero;
			gameObject.SetActive(false);
			Debug.LogError("WIN " + 15);
		}
	}
}
