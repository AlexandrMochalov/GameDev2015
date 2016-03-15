using UnityEngine;

public class PacmanCollider : MonoBehaviour {
    public int WallCount { private set; get; }

    public PacmanCharacter _player;

    void Start () {
        WallCount = 0;
        GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "wall")
        {
            WallCount += 1;
            _player.OnWallCollision(this);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "wall")
            WallCount = Mathf.Max( WallCount-1, 0);
    }
}
