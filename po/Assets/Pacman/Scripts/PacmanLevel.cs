using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PacmanLevel : MonoBehaviour {
    [SerializeField]
    private GameObject _wall;
    [SerializeField]
    private GameObject _smallPoint;
    [SerializeField]
    private GameObject _bigPoint;
    [SerializeField]
    private GameObject _cherry;
    [SerializeField]
    private GameObject _enemy;
    [SerializeField]
    private GameObject _pacman;
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private int _pointScoreStep = 0;
    [SerializeField]
    private int _bigPointScoreStep = 0;
    [SerializeField]
    private int _casperScoreStep = 0;
    [SerializeField]
    private int _cherryScoreStep = 0;

    private int _score = 0;
    private int w = 21;
    private int h = 16;
    private int pointsCount = 0;
    
    //0 - wall, 1 - small point, 2 - big point, 3 - cherry, 4 - enemy, 5 - pacman
    private int[,] _map = new int[,] {
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        {0, 2, 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1, 1, 4, 1, 1, 1, 1, 2, 0 },
        {0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 3, 1, 0, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
        {0, 4, 0, 1, 1, 3, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 2, 1, 0, 4, 0 },
        {0, 1, 0, 1, 2, 1, 1, 1, 0, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 0, 1, 3, 1, 1, 0, 1, 0 },
        {0, 1, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0, 1, 0 },
        {0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0 },
        {0, 2, 1, 1, 1, 1, 1, 1, 1, 1, 5, 1, 1, 1, 1, 1, 1, 1, 1, 2, 0 },
        {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    private PacmanPlayer _player;
    private List<GameObject> _objects = new List<GameObject>();
    private List<Casper> _caspers = new List<Casper>();

    // Use this for initialization
	void Start () {
        _objects.Add(_wall);
        _objects.Add(_smallPoint);
        _objects.Add(_bigPoint);
        _objects.Add(_cherry);
        _objects.Add(_enemy);
        _objects.Add(_pacman);

        BuildMap();


	}

    void BuildMap()
    {
        var offset = new Vector3 (w-1, h-1, 0) * 0.32f;
        for (int c = 0; c < w; c++)
        {
            for (int r = 0; r < h; r++)
            {
                var objType = _map[r, c];
                var obj = (GameObject)Object.Instantiate(_objects[objType]);

                if (objType == 1 || objType == 2)
                    pointsCount++;
                else if (objType == 4)
                {
                    _caspers.Add(obj.GetComponent<Casper>());
                }
                else if (objType == 5)
                    _player = obj.GetComponent<PacmanPlayer>();

                if (obj != null)
                {
                    var pos = new Vector3(0.64f * (float)c, 0.64f * (float)r, 0f);
                    obj.SetActive(true);
                    obj.transform.SetParent(transform);
                    obj.transform.position = pos - offset;
                }
            }
        }
    }

    public void OnSmallPoint()
    {
        _score += _pointScoreStep;
        RemovePointAndCheckForWin();
        UpdateScreenScore();
    }

    public void OnBigPoint()
    {
        _score += _bigPointScoreStep;
        RemovePointAndCheckForWin();
        UpdateScreenScore();
        foreach (var casper in _caspers)
            casper.SetCanDie();
    }

    public void OnCasper(Casper casper)
    {
        if (casper.CanDie)
        {
            casper.gameObject.SetActive(false);
            _score += _casperScoreStep;
            UpdateScreenScore();
        }
        else
        {
            _player.Die();
            Debug.LogError("Lose!");
        }
    }

    public void OnCherry()
    {
        _score += _cherryScoreStep;
        UpdateScreenScore();
    }

    void RemovePointAndCheckForWin()
    {
        pointsCount--;
        if (pointsCount == 0)
            Debug.LogError("Win");
    }


    void UpdateScreenScore()
    {
        _scoreText.text = "Score: " + _score;
    }
}


