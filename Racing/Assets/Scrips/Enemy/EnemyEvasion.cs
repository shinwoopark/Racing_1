using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEvasion : MonoBehaviour
{
    public Rigidbody SphereCollider;

    public Transform CarHitRayPos;

    public LayerMask HardleCheck;

    public float SideSpeed;

    void Update()
    {
        RaycastHit Hardlehit;

        Debug.DrawRay(CarHitRayPos.position, transform.forward * 10, Color.red);

        if (Physics.Raycast(CarHitRayPos.position, transform.forward, out Hardlehit, 10, HardleCheck))
        {
            int dir = Random.Range(0, 2);

            if (dir == 0)
            {
                SideSpeed *= -1;
            }

            SphereCollider.AddForce(Vector3.right * SideSpeed, ForceMode.Impulse);
        }
    }
}
