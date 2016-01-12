using UnityEngine;
using System.Collections.Generic;

public class MiniGame01 : MonoBehaviour {
    private List<List<CellMiniGame01>> _game = new List<List<CellMiniGame01>>();

    void Start() {
        for (int row = 0; row < 3; row++) {
            var rowList = new List<CellMiniGame01>();
            _game.Add(rowList);
            for (int col = 0; col < 3; col++) {
                var cellObject = GameObject.Find("Cell" + row + col);
                var cell = cellObject.GetComponent<CellMiniGame01>();
                cell.Game = this;
                cell.row = row;
                cell.col = col;
                cell.Chage();

                rowList.Add(cell);
            }
        }

        for (int i = 0; i < 3; i++) {
            //Invoke("RndClick", i * 0.5f);
            //RndClick ();
        }
    }

    void RndClick() {
        var r = Random.Range(0, 3);
        var c = Random.Range(0, 3);
        _game[r][c].gameObject.SendMessage("OnMouseDown");
    }

    void ChangeCell(int row, int col) {
        if (row >= 0 && row < 3 && col >= 0 && col < 3)
            _game[row][col].Chage();
    }

    bool CheckVictory() { 
        for (int row = 0; row< 3; row++) {
            for (int col = 0; col< 3; col++) {
                if (!_game[row][col].Open)
                    return false;
            }
        }
        return true;
    }

    public void OnCellPress(int row, int col)
    {
        ChangeCell(row + 1, col);
        ChangeCell(row - 1, col);
        ChangeCell(row, col + 1);
        ChangeCell(row, col - 1);

        if (CheckVictory())
            for (int r = 0; r < 3; r++)
                for (int c = 0; c < 3; c++)
                    _game[r][c].ShowWin();
    }
}
