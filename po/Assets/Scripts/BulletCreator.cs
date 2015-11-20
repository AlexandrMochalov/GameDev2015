using UnityEngine;
using System.Collections;

public class BulletCreator : MonoBehaviour {
	void Start () {
        Invoke("Spawn", 1f);
    }

    void Spawn()
    {
        GameObject bullet;
        float t = Random.Range(0.5f, 2.5f);
        float x = Random.Range(-5, 5);

        if (Random.value > 0.5f)
            bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
        else
            bullet = (GameObject)Instantiate(Resources.Load("Prefabs/BowlingBall"));

        bullet.transform.position = new Vector3(x, 6f, 0f);
        Invoke("Spawn", t);
    }
}
