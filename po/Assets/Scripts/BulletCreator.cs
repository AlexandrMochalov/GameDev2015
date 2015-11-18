using UnityEngine;
using System.Collections;

public class BulletCreator : MonoBehaviour {
	void Start () {
        Invoke("Spawn", 1f);
    }

    void Spawn()
    {
        float t = Random.Range(0.5f, 2.5f);
        float x = Random.Range(-8, 8);
        var bullet = (GameObject)Instantiate(Resources.Load("Prefabs/Ball"));
        bullet.transform.position = new Vector3(x, 6f, 0f);
        Invoke("Spawn", t);
    }
}
