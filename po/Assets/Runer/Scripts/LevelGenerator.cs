using UnityEngine;
using System.Collections.Generic;

public class LevelGenerator : MonoBehaviour {
	public int LevelLength;
	public GameObject LandProto;
	public GameObject EnemyProto;

	public GameObject Char;
	public GameObject Exit;

	private List<bool> _map = new List<bool>();
	private float _size;

	void Start () {
		_size = LandProto.GetComponent<BoxCollider2D>().size.x;
		InitMap ();
	}
	
	// Update is called once per frame
	void Update () {
		var pos = Camera.main.transform.position;
		Camera.main.transform.position = new Vector3 (Char.transform.position.x, pos.y, pos.z);
	}

	void InitMap () {
		bool isPrevEmpty = false;
		bool isPrevEnemy = false;

		Exit.transform.position = new Vector3(LevelLength*_size, 0f, 0f);

		for (int i=0; i<LevelLength; i++) {
			var isLand = (i==0) || isPrevEmpty || Random.Range(0, 1000) > 500;
			_map.Add(isLand);
			if (isLand) {
				var land = (GameObject)GameObject.Instantiate(LandProto, new Vector3(i * _size, 0f, 0f), Quaternion.identity);
				land.SetActive(true);

				if (i > 0 && !isPrevEmpty && !isPrevEnemy && Random.Range(0, 1000) > 500) {
					var y = Random.Range(1.5f, 2.5f);
					var enemy = (GameObject)GameObject.Instantiate(EnemyProto, new Vector3(i * _size, y, 0f), Quaternion.identity);
					enemy.SetActive(true);
					isPrevEnemy = true;
				} else {
					isPrevEnemy = false;
				}

				isPrevEmpty = false;
			} else {
				isPrevEmpty = true;
			}
		}
	}

}
