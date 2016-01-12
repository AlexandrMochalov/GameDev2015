using UnityEngine;
using System.Collections;

[RequireComponent (typeof(BoxCollider2D))]
public class CellMiniGame01 : MonoBehaviour {
    public MiniGame01 Game;
    public bool Open = false;
    public int row;
    public int col;

    void OnMouseDown() {
        Game.OnCellPress(row, col);
    }

    public void Chage() {
        Open = !Open;
        GetComponent<SpriteRenderer>().color = Open ? Color.green : Color.white;
    }

    public void ShowWin() {
        GetComponent<SpriteRenderer>().color = Color.red;
        iTween.ScaleTo(gameObject
            , iTween.Hash("scale", Vector3.zero, "time", Random.Range(1f, 3f), "easeType", "easeInOutBack"));
    }
}
