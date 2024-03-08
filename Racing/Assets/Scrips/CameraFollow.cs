using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera_Action : MonoBehaviour
{
    public GameObject Target;

    private float _offsetX = 0.0f;
    private float _offsetY = 0.0f;
    private float _offsetZ = 0f;

    public float CameraSpeed = 10.0f;
    public float TrunSpeed = 10.0f;

    private Vector3 _targetPos;

    void FixedUpdate()
    {
        _targetPos = new Vector3(
            Target.transform.position.x + _offsetX,
            Target.transform.position.y + _offsetY,
            Target.transform.position.z + _offsetZ);

        transform.position = Vector3.Lerp(transform.position, _targetPos, Time.deltaTime * CameraSpeed);

        transform.rotation = Quaternion.Lerp(transform.rotation, Target.transform.rotation, Time.deltaTime * TrunSpeed);
    }
}
