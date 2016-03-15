using UnityEngine;
using System.Collections.Generic;

public class Casper : PacmanCharacter {
    [SerializeField]
    private float _dethTime = 0f;

    public GameObject LeftEye;
    public GameObject RightEye;
    public GameObject LeftPupil;
    public GameObject RightPupil;
    public bool CanDie { private set; get; }

    private bool _mustChangeDirection = true;

    private int _frameCount = 0;
    private float _dethTimeCountdown = 0f;
    private Dictionary<State, Vector3> _leftEyePositions = new Dictionary<State, Vector3>();
    private Dictionary<State, Vector3> _rightEyePositions = new Dictionary<State, Vector3>();
    private Dictionary<State, Vector3> _pupilPositions = new Dictionary<State, Vector3>();
    private Color _normalColor;
    
    public void SetCanDie()
    {
        CanDie = true;
        _dethTimeCountdown = _dethTime;
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    void Start()
    {
        CanDie = false;
        InitEyesPositions();
        var r = Random.Range(0, 300);
        _normalColor = (r < 100) ? Color.red :
                       (r < 200) ? Color.green :
                       Color.yellow;

        GetComponent<SpriteRenderer>().color = _normalColor;
    }

    void Update() {
        _frameCount++;
        if (_frameCount >= 100 || _mustChangeDirection)
        {
            _mustChangeDirection = false;
            _frameCount = 0;
            int rnd = Random.Range(0, 1000);
            var nextState = (rnd < 250) ? State.Left :
                            (rnd < 500) ? State.Right :
                            (rnd < 750) ? State.Up :
                            State.Down;

            if (nextState == State.Left && LeftCollider.WallCount > 0)
                nextState = State.Right;
            if (nextState == State.Right && RightCollider.WallCount > 0)
                nextState = State.Up;
            if (nextState == State.Up && TopCollider.WallCount > 0)
                nextState = State.Down;
            if (nextState == State.Down && BottomCollider.WallCount > 0)
                nextState = State.Left;

            ApplyNewState(nextState);
        }

        CheckForContinueMoving();
        UpdateEyes();

        transform.position += _velocity;

        if (CanDie)
        {
            _dethTimeCountdown -= Time.deltaTime;
            if (_dethTimeCountdown <= 0f)
            {
                CanDie = false;
                _dethTimeCountdown = 0f;
                GetComponent<SpriteRenderer>().color = _normalColor;
            }
        }
    }

    void UpdateEyes()
    {
        LeftEye.transform.localPosition = _leftEyePositions[_state];
        RightEye.transform.localPosition = _rightEyePositions[_state];
        LeftPupil.transform.localPosition = _pupilPositions[_state];
        RightPupil.transform.localPosition = _pupilPositions[_state];
    }

    void InitEyesPositions()
    {
        _leftEyePositions[State.None]   = new Vector3(-0.5f, 0.2f, 0);
        _leftEyePositions[State.Die]    = new Vector3(-0.5f, 0.2f, 0);
        _leftEyePositions[State.Right]  = new Vector3(-0.3f, 0.2f, 0);
        _leftEyePositions[State.Left]   = new Vector3(-0.7f, 0.2f, 0);
        _leftEyePositions[State.Up]     = new Vector3(-0.5f, 0.2f, 0);
        _leftEyePositions[State.Down]   = new Vector3(-0.5f, 0.2f, 0);

        _rightEyePositions[State.None]  = new Vector3(0.5f, 0.2f, 0);
        _rightEyePositions[State.Die]   = new Vector3(0.5f, 0.2f, 0);
        _rightEyePositions[State.Right] = new Vector3(0.7f, 0.2f, 0);
        _rightEyePositions[State.Left]  = new Vector3(0.3f, 0.2f, 0);
        _rightEyePositions[State.Up]    = new Vector3(0.5f, 0.2f, 0);
        _rightEyePositions[State.Down]  = new Vector3(0.5f, 0.2f, 0);

        _pupilPositions[State.None]     = new Vector3(0f, 0f, 0);
        _pupilPositions[State.Die]      = new Vector3(0f, 0f, 0);
        _pupilPositions[State.Right]    = new Vector3(0.07f, 0f, 0);
        _pupilPositions[State.Left]     = new Vector3(-0.07f, 0f, 0);
        _pupilPositions[State.Up]       = new Vector3(0f, 0.07f, 0);
        _pupilPositions[State.Down]     = new Vector3(0f, -0.07f, 0);
    }

    public override void OnWallCollision(PacmanCollider collider)
    {
        if ((collider == LeftCollider && _state == State.Left) ||
            (collider == RightCollider && _state == State.Right) ||
            (collider == TopCollider && _state == State.Up) ||
            (collider == BottomCollider&& _state == State.Down))
        {
            _mustChangeDirection = true;
        }
    }
}
