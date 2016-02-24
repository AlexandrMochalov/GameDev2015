using UnityEngine;

public class PacmanCollider : MonoBehaviour {
    public int WallCount { private set; get; }

    void Start () {
        WallCount = 0;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "wall") WallCount += 1;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "wall") WallCount -= 1;
    }
}
