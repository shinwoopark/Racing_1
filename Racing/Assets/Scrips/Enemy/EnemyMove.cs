using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class EnemyMove : MonoBehaviour
{
    public GameObject[] WayPoints_gb;

    public Transform[] WayPoints_tr;

    public GameObject[] Safe_gb;

    public Transform[] Safe_tr;

    public Rigidbody SphereCollider;

    public float SideSpeed, GravityForce, GroundDrag;

    public LayerMask GroundCheck;
    public float GroundRayLength;
    public Transform GroundRayPos, CarHitRayPos;

    public float Speed, RotationSpeed;

    private int _bwayPointNumber = 0;

    private bool _bgrounded;

    void Start()
    {
        SphereCollider.transform.parent = null;
    }

    void Update()
    {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        
        transform.position = SphereCollider.transform.position;
    }

    private void FixedUpdate()
    {
        MoveUpdate();

        FindWayPoint();

        RayHitUpdate();
    }

    private void MoveUpdate()
    {
        if (GameInstance.instance.bRacing)
        {
            FindWayPoint();

            Speed = 10000;
        }

        if (_bgrounded)
        {
            SphereCollider.drag = GroundDrag;

            if (Mathf.Abs(Speed) > 0)
            {
                SphereCollider.AddForce(transform.forward * Speed);
            }
        }
        else
        {
            SphereCollider.drag = 0.1f;

            SphereCollider.AddForce(Vector3.up * -GravityForce * 100);
        }
    }


    private void RayHitUpdate()
    {
        RaycastHit Groundhit;

        Debug.DrawRay(GroundRayPos.position, -transform.up * GroundRayLength, Color.blue);

        if (Physics.Raycast(GroundRayPos.position, -transform.up, out Groundhit, GroundRayLength, GroundCheck))
        {
            _bgrounded = true;
        }
        else
        {
            _bgrounded = false;
        }
    }

    private void FindWayPoint()
    {
        Vector3 dir = WayPoints_tr[_bwayPointNumber].position - transform.position;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * RotationSpeed);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "WayPoint")
        {
            _bwayPointNumber++;

            Destroy(collision.gameObject);
        }
    }
}
