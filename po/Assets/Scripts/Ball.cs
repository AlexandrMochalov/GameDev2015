using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {
    public void ConvertToScore() {
        GameObject score;
        if (tag == "ball")
            score = (GameObject)Instantiate(Resources.Load("Prefabs/ScoreUp"));
        else
            score = (GameObject)Instantiate(Resources.Load("Prefabs/ScoreDown"));

        score.transform.position = transform.position + new Vector3 (0f, 1f, 0f);

        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "ground") 
        {
            Destroy(gameObject);
        }
    }

}
