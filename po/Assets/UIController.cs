using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void DeselectAll()
    {
        var eventSystem = GetComponentInChildren<EventSystem>();
        eventSystem.SetSelectedGameObject(null);
    }

    public void OnList(int value)
    {
        Debug.LogError(value);
    }
}
