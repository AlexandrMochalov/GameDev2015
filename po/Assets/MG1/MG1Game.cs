using UnityEngine;
using System.Collections.Generic;

public class MG1Game : MonoBehaviour {
    [SerializeField]
    private GameObject _prototype;
    [SerializeField]
    private int _colCount = 5;
    [SerializeField]
    private int _rowCount = 4;


    private float size = 1.5f;
    private List<List<MG1Plate>> _game = new List<List<MG1Plate>> ();

	// Use this for initialization
	void Start ()
    {
        int index = 0;
        for (int c = 0; c < _colCount; c++)
        {
            var col = new List<MG1Plate>();
            float x = (c - _colCount * 0.5f) * size;
            for (int r = 0; r < _rowCount; r++)
            {
                var plateObj = Object.Instantiate<GameObject>(_prototype);//transform.GetChild(index);
                var plate = plateObj.AddComponent<MG1Plate>();
                float y = (r - _rowCount * 0.5f) * size;
                plateObj.SetActive(true);
                index++;
                col.Add(plate);
                plateObj.transform.localPosition = new Vector3(x, y, 0f);
                plate.row = r;
                plate.col = c;
                plate.Game = this;

                if (Random.Range(0, 100) > 50)
                {
                    plate.Change ();
                }
            }
            _game.Add(col);
        }
	}

    void Change(int row, int col)
    {
        if (row >= 0 && row < _rowCount && col >= 0 && col < _colCount)
            _game[col][row].Change();
    }

    bool CheckForVictory ()
    {
        foreach (var col in _game) {
            foreach (var plate in col) {
                if (!plate.IsOpen) return false;
            }
        }

        return true;
    }

    public void OnPlateMouseDown(int row, int col)
    {
        Change(row - 1, col);
        Change(row + 1, col);
        Change(row, col - 1);
        Change(row, col + 1);

        if (CheckForVictory())
            Debug.LogError("W I N");
    }
}
