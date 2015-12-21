using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour {
    //private SerializeField rocket
    private AsyncOperation _loading = null;

    void Start() {
        Invoke("Inv", 1f);
    }

    void Inv()
    {
        _loading = MySceneManager.OnCrossSceneLoaded ();
    }

	// Update is called once per frame
	void Update () {
        if (_loading == null) return;
        if (_loading.isDone)
        {
            _loading = null;
            MySceneManager.OnLoadingDone();
        }
    }
}
