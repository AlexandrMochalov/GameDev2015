using UnityEngine;
using System.Collections;

public class UIEventListener : MonoBehaviour {

    public void OnToggle(bool val)
    {
        Debug.LogError(val);
    }

    public void OnSlider(float val)
    {
        Debug.LogError(val);
    }

}

