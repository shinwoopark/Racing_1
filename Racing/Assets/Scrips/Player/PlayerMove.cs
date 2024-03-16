using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    enum State {Moving, Drifting} // X
    State CurrentState;

    public PlayerEffect PlayerEffect;

    public Rigidbody SphereCollider;

    public float ForwardAccel, ReverseAccel, MaxSpeed, TurnStrength, GravityForce, GroundDrag, JumpPower;

    private float _speedInput, _turnInput;

    private bool _bGrounded;

    public LayerMask GroundCheck;
    public float GroundRayLength;
    public Transform PlayerPos, GroundRayPos;

    public Transform LeftFrontWheel, RightFrontWheel, LeftBackWheel, RightBackWheel;
    public float MaxWheelTurn;

    private int _direction = 0;
    private float _currentSpeed;
    private float _slowDown = 1;

    private bool _bdrift = false;
    private bool _bbooster = false;

    private float _booster;
    private float _boosterTime;

    private bool _bpause = false;

    private void StateUpdate()
    {
        switch (CurrentState)
        {
            case State.Moving:
                _bdrift = false;
                break;
            case State.Drifting:
                _bdrift = true;
                _bbooster = false;
                break;
        }

        //Drift
        if (_bdrift)
        {
            if(_bGrounded)
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

        _booster = 1;
    }

    void Update()
    {
        TimePause();

        if (_bpause)
            return;

        StateUpdate();

        InputDrift();

        RayHitUpdate();

        BoosterCheck();

        CallEffext();    
    }

    void FixedUpdate()
    {
        if (_bpause)
            return;

        Position();

        CurrentSpeed();

        DriftUpdate();

        InputMove();

        PlayerMoveUpdate();

        WheelMoveUpdate();
    }

    private void TimePause()
    {
        if (Time.timeScale == 0)
        {
            _bpause = true;
        }
        else
        {
            _bpause = false;
        }
    }

    private void Position()
    {
        transform.position = SphereCollider.transform.position; 
    }

    private void CurrentSpeed()
    {
        if (_bbooster)
        {
            _currentSpeed = _booster;
        }           
        else
        {
            _currentSpeed = _speedInput * _slowDown;
        }       
    }

    private void BoosterCheck()
    {
        if (_bbooster)
        {
            _boosterTime -= Time.deltaTime;

            if(CurrentState == State.Drifting)
            {
                _bbooster = false;
            }
                
            if (_boosterTime <= 0)
            {            
                _bbooster = false;
            }   
        }

        if (!_bbooster)
        {
            _booster = 0;
            _boosterTime = 0;
        }
            
    }

    private void RayHitUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(GroundRayPos.position, -transform.up, out hit, GroundRayLength, GroundCheck))
        {
            _bGrounded = true;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
        else
        {
            _bGrounded = false;

            transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        }
    }

    private void PlayerMoveUpdate() //2
    {
        if (_bGrounded)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, _turnInput * TurnStrength * Time.deltaTime * Input.GetAxis("Vertical"), 0));

        if (_bGrounded)
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

    private void InputMove() //1
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
            if (!_bGrounded)
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

            //if (_slowDown < 0.3f)
            //{
            //    _slowDown = 0.3f;
            //}

            if (_slowDown < 0)
            {
                _slowDown = 0;
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

    public void Booster(int BoosterNumber)
    {
        _bbooster = true;
        switch (BoosterNumber)
        {
            case 1:
                _booster = 12000;
                _boosterTime = 1.5f;
                break;
            case 2:             
                _booster = 20000;
                _boosterTime = 3;
                break;
        }
    }

    private void CallEffext()
    {

    }
}
