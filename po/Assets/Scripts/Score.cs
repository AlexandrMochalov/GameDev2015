﻿using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
    void OnAnimationEnd() {
        Destroy(gameObject);
    }
}
