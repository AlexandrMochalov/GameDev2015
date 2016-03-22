using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeGame : MonoBehaviour {
    private enum PointState {
        None,
        Snake,
        Food
    }

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    private struct Point
    {
        public int x;
        public int y;

        public Point (int px, int py)
        {
            x = px;
            y = py;
        }

        public Point(Point point)
        {
            x = point.x;
            y = point.y;
        }
    }

    [SerializeField]
    private GameObject _proto;
    [SerializeField]
    private float _pointSize = 0.16f;
    
    const int _gameSize = 50;
    private GameObject _food = null;
    private Direction _currDirection = Direction.Up;
    private Direction _newDirection = Direction.Up;


    private PointState[,] _game = new PointState[_gameSize, _gameSize];
    private GameObject[,] _objects = new GameObject[_gameSize, _gameSize];
    private List<Point> _snake = new List<Point>();

    Vector3 PointToPosition (Point point)
    {
        var half = (_gameSize - 1) * 0.5f;
        return new Vector3(point.x - half, point.y - half, 0f) * _pointSize;
    }

    GameObject CreateObjectToPoint(Point point)
    {
        GameObject obj = GameObject.Instantiate(_proto);
        obj.transform.position = PointToPosition(point);
        obj.SetActive(true);

        _objects[point.x, point.y] = obj;

        return obj;
    }

    PointState GetState(Point point)
    {
        return _game[point.x, point.y];
    }

    void Start()
    {
        for (int c = 0; c < _gameSize; c++)
        {
            for (int r = 0; r < _gameSize; r++)
            {
                _game[c, r] = PointState.None;
            }
        }

        _game[25, 0] = PointState.Snake;
        _game[25, 1] = PointState.Snake;

        _snake.Add(new Point(25, 0));
        _snake.Add(new Point(25, 1));

        CreateObjectToPoint(new Point(25, 0));
        CreateObjectToPoint(new Point(25, 1));

        SetRandomFood();
        UpdateSnake();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && _currDirection != Direction.Up)
            _newDirection = Direction.Down;
        else if (Input.GetKeyDown(KeyCode.UpArrow) && _currDirection != Direction.Down)
            _newDirection = Direction.Up;
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _currDirection != Direction.Left)
            _newDirection = Direction.Right;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _currDirection != Direction.Right)
            _newDirection = Direction.Left;
    }

    Point CalcNext(Point current)
    {
        Point next = new Point(current);

        switch (_currDirection)
        {
            case Direction.Up:
                next.y++;
                break;
            case Direction.Down:
                next.y--;
                break;
            case Direction.Left:
                next.x--;
                break;
            case Direction.Right:
                next.x++;
                break;
        }

        return next;
    }

    

    void UpdateSnake()
    {
        //bool runNext = true;
        _currDirection = _newDirection;
        int headIndex = _snake.Count - 1;
        Point head = _snake[headIndex];
        Point tail = _snake[0];
        Point next = CalcNext(head);

        //if out of range
        if (next.x >= _gameSize || next.x < 0 || next.y >= _gameSize || next.y < 0)
        {
            Debug.LogError("Lose");
            return;
        }

        //Debug.LogError(GetState(next));

        switch (GetState(next))
        {
            case PointState.None:
                _game[tail.x, tail.y] = PointState.None;
                _game[next.x, next.y] = PointState.Snake;

                var obj = _objects[tail.x, tail.y];
                _objects[tail.x, tail.y] = null;
                _objects[next.x, next.y] = obj;
                obj.transform.position = PointToPosition(next);

                for (int i = 0; i < headIndex; i++)
                    _snake[i] = _snake[i + 1];

                _snake[headIndex] = next;
                break;

            case PointState.Food:
                _game[next.x, next.y] = PointState.Snake;
                _snake.Add(next);
                SetRandomFood();
                break;

            case PointState.Snake:
                Debug.LogError("Lose");
                return;
                //break;
        }

        Invoke("UpdateSnake", 0.125f);
    }

    void SetRandomFood()
    {
        var list = new List<Point>();
        for (int c = 0; c < _gameSize; c++)
        {
            for (int r = 0; r < _gameSize; r++)
            {
                if (_game[c, r] == PointState.None)
                    list.Add(new Point(c, r));
            }
        }

        int index = Random.Range(0, list.Count);
        var point = list[index];
        _food = CreateObjectToPoint(point);
        _game[point.x, point.y] = PointState.Food;
    }
}
