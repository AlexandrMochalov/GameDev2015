using UnityEngine;
using System.Collections;

public class MySecondsScript : MonoBehaviour {
	[HideInInspector]
	public string MyGameName = "Game";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		GetComponent<SpriteRenderer>().color = Color.red;
		GetComponent<Transform>().position = new Vector3(0f,0f,0f);
		transform.position = new Vector3(0f,0f,0f);
	}
}
