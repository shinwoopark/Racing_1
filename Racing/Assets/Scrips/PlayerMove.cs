using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    enum State {Moving, Drifting, Booster}
    State CurrentState;

    public PlayerEffect PlayerEffect;

    public Rigidbody SphereCollider;

    public float ForwardAccel, ReverseAccel, MaxSpeed, TurnStrength, GravityForce, GroundDrag, JumpPower;

    private float _speedInput, _turnInput;

    private bool _bgrounded;

    public LayerMask GroundCheck;
    public float GroundRayLength;
    public Transform PlayerPos, GroundRayPos;

    public Transform LeftFrontWheel, RightFrontWheel, LeftBackWheel, RightBackWheel;
    public float MaxWheelTurn;

    private int _direction = 0;
    private float _currentSpeed;
    private float _slowDown = 1;
    private bool _bdrift = false;

    private void StateUpdate()
    {
        switch (CurrentState)
        {
            case State.Moving:
                _bdrift = false;
                break;
            case State.Drifting:
                _bdrift = true;
                break;
            case State.Booster:
                _bdrift = false;
                break;
        }

        //Drift
        if (_bdrift)
        {
            if(_bgrounded)
                PlayerEffect.Play(1, true);
            else
                PlayerEffect.Play(1, false);
        }
        else if (!_bdrift)
        {
            PlayerEffect.Play(1, false);
            TurnStrength = 90;
            TurnStrength -= _speedInput / 200;
            if (TurnStrength >= 90)
                TurnStrength = 90;
        }
    }
    void Start()
    {
        CurrentState = State.Moving;

        SphereCollider.transform.parent = null;
    }

    void Update()
    {
        StateUpdate();

        InputDrift();

        RayHitUpdate();

        CallEffext();
    }

    void FixedUpdate()
    {
        transform.position = SphereCollider.transform.position;

        _currentSpeed = _speedInput * _slowDown;

        DriftUpdate();

        InputMove();

        PlayerMoveUpdate();

        WheelMoveUpdate();
    }

    private void RayHitUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(GroundRayPos.position, -transform.up, out hit, GroundRayLength, GroundCheck))
        {
            _bgrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        else
        {
            _bgrounded = false;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }

    private void PlayerMoveUpdate()
    {
        if (_bgrounded)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, _turnInput * TurnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0));

        if (_bgrounded)
        {
            SphereCollider.drag = GroundDrag;

            if (Mathf.Abs(_currentSpeed) > 0)
            {
                SphereCollider.AddForce(transform.forward * _currentSpeed);
            }
        }
        else
        {
            SphereCollider.drag = 0.1f;

            SphereCollider.AddForce(Vector3.up * -GravityForce * 100);
        }
    }

    private void WheelMoveUpdate()
    {
        LeftFrontWheel.localRotation = Quaternion.Euler(LeftFrontWheel.localRotation.eulerAngles.x, (_turnInput * MaxWheelTurn) - 270, LeftFrontWheel.localRotation.eulerAngles.z);
        RightFrontWheel.localRotation = Quaternion.Euler(RightFrontWheel.localRotation.eulerAngles.x, (_turnInput * MaxWheelTurn) - 90, RightFrontWheel.localRotation.eulerAngles.z);

        LeftFrontWheel.eulerAngles += new Vector3(0, 0, _currentSpeed * Time.deltaTime);
        RightFrontWheel.eulerAngles += new Vector3(0, 0, _currentSpeed * Time.deltaTime);
        LeftBackWheel.eulerAngles += new Vector3(0, 0, _currentSpeed * Time.deltaTime);
        RightBackWheel.eulerAngles += new Vector3(0, 0, _currentSpeed * Time.deltaTime);
    }

    private void InputMove()
    {
        _speedInput = 0;

        if (Input.GetAxis("Vertical") > 0)
        {
            _speedInput = Input.GetAxis("Vertical") * ForwardAccel * 1000;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _speedInput = Input.GetAxis("Vertical") * ReverseAccel * 1000;
        }

        _turnInput = Input.GetAxis("Horizontal");
    }

    private void InputDrift()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!_bgrounded)
                return;

            if (_turnInput == 0 || _speedInput <= 0)
            {
                CurrentState = State.Moving;
                return;
            }

            CurrentState = State.Drifting;
            

            if (_turnInput > 0)
            {
                SphereCollider.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                _direction = 1;
            }
            else if (_turnInput < 0)
            {
                SphereCollider.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                _direction = -1;
            }
        }

        if (_bdrift)
        {
            if (Input.GetKeyUp(KeyCode.Space) ||
                Input.GetKeyUp(KeyCode.W) || 
                Input.GetKeyUp(KeyCode.A) || 
                Input.GetKeyUp(KeyCode.D) || 
                Input.GetKeyUp(KeyCode.LeftArrow) || 
                Input.GetKeyUp(KeyCode.UpArrow) || 
                Input.GetKeyUp(KeyCode.RightArrow))
            {
                CurrentState = State.Moving;
                _direction = 0;
            }
        }
    }

    private void DriftUpdate()
    {
        float turnStrength = 10;
        float slowDown = 0.3f;

        if (_direction != 0)
        {
            _slowDown -= slowDown * Time.deltaTime;

            if (_slowDown < 0.3f)
            {
                _slowDown = 0.3f;
            }

            turnStrength += turnStrength * 5 + Time.deltaTime * 2;
            TurnStrength += turnStrength * Time.deltaTime;

            if (TurnStrength >= 120)
            {
                TurnStrength = 120;
            }
        }
        else
        {
            _slowDown = 1;
        }

        if(_direction > 0)
        {
            
        }
        if (_direction < 0)
        {

        }       
    }

    private void CallEffext()
    {

    }
}
