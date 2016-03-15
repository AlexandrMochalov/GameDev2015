using UnityEngine;
using System.Collections;

public class PacmanDetector : MonoBehaviour {
    public PacmanPlayer Player;

    void OnTriggerEnter2D(Collider2D other)
    {
        Player.OnDetect(other);
    }
}
