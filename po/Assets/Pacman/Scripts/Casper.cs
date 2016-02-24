using UnityEngine;
using System.Collections.Generic;

public class Casper : PacmanCharacter {
    public GameObject LeftEye;
    public GameObject RightEye;
    public GameObject LeftPupil;
    public GameObject RightPupil;

    private int frameCount = 0;
    private Dictionary<State, Vector3> _leftEyePositions = new Dictionary<State, Vector3>();
    private Dictionary<State, Vector3> _rightEyePositions = new Dictionary<State, Vector3>();
    private Dictionary<State, Vector3> _pupilPositions = new Dictionary<State, Vector3>();

    void Start()
    {
        InitEyesPositions();
    }

    void Update() {
        frameCount++;
        if (frameCount >= 100)
        {
            frameCount = 0;
            int rnd = Random.Range(0, 1000);
            var nextState = (rnd < 250) ? State.Left :
                            (rnd < 500) ? State.Right :
                            (rnd < 750) ? State.Up :
                            State.Down;

            ApplyNewState(nextState);
        }

        CheckForContinueMoving();
        UpdateEyes();

        transform.position += _velocity;
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
}
