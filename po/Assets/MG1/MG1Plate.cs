using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))] 
public class MG1Plate : MonoBehaviour {
    public MG1Game Game;
    public bool IsOpen { get; private set; }
    public int row;
    public int col;

    void OnMouseDown() {
        //Change ();
        Game.OnPlateMouseDown(row, col);
    }

    public void Change ()
    {
        IsOpen = !IsOpen;
        GetComponent<SpriteRenderer>().color = IsOpen ? Color.green : Color.white;
    }
}
