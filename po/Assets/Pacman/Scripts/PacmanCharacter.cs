using UnityEngine;
using System.Collections;

public class PacmanCharacter : MonoBehaviour {
    protected enum State { None, Up, Right, Left, Down, Die }

    public float Speed = 0.25f;
    public PacmanCollider RightCollider;
    public PacmanCollider LeftCollider;
    public PacmanCollider TopCollider;
    public PacmanCollider BottomCollider;

    protected State _state = State.None;
    protected Vector3 _velocity = Vector3.zero;

    protected void CheckForContinueMoving()
    {
        if ((_state == State.Up && TopCollider.WallCount > 0) ||
            (_state == State.Down && BottomCollider.WallCount > 0) ||
            (_state == State.Right && RightCollider.WallCount > 0) ||
            (_state == State.Left && LeftCollider.WallCount > 0))
        {
            _velocity = Vector3.zero;
            _state = State.None;
        }
    }

    protected bool ApplyNewState(State state)
    {
        bool result = false;
        if (_state != state)
        {
            if (state == State.Right && RightCollider.WallCount == 0)
            {
                result = true;
                _velocity = new Vector3(Speed, 0f, 0f);
            }
            else if (state == State.Left && LeftCollider.WallCount == 0)
            {
                result = true;
                _velocity = new Vector3(-Speed, 0f, 0f);
            }
            else if (state == State.Up && TopCollider.WallCount == 0)
            {
                result = true;
                _velocity = new Vector3(0f, Speed, 0f);
            }
            else if (state == State.Down && BottomCollider.WallCount == 0)
            {
                result = true;
                _velocity = new Vector3(0f, -Speed, 0f);
            }
        }

        if (result) _state = state;
        return result;
    }
}
