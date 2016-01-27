using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    private Rigidbody2D _rigitBody;
    
    void Start()
    {
        _rigitBody = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("ground"))
        {
            _rigitBody.velocity = Vector2.zero;
            _rigitBody.AddForce(new Vector2(0f, 400f));
        }
        else
        {
            gameObject.SetActive(false);            
        }
            
    }
    
}
