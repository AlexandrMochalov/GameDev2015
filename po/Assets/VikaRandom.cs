using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class VikaRandom : MonoBehaviour {
    List<int> q = new List<int>() { 1, 2, 3, 4, 5 };
    List<int> vv = new List<int>();
    List<int> vs = new List<int>();
    // Use this for initialization
    void Start () {
        var text = GetComponent<Text>();
        bool isvv = true;
        while (q.Count > 0) {
            int index = Random.Range(0, q.Count - 1);
            
            isvv = !isvv;
            if (isvv) vv.Add(q[index]);
            else vs.Add(q[index]);

            q.RemoveAt(index);
        }
        text.text += "\n ВB: \n";
        foreach (int v in vv)
            text.text += " " + v;

        text.text += "\n ВС: \n";
        foreach (int v in vs)
            text.text += " " + v;

    }

    // Update is called once per frame
    void Update () {
	
	}
}
