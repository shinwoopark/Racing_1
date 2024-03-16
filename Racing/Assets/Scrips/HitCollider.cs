using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    public Rigidbody PlayerMove_rd;

    public Rigidbody SphereCollider;

    public float Knockback;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 direction = (other.transform.position - transform.position).normalized;

        SphereCollider.AddForce(new Vector3(-direction.x, -direction.y, -direction.z) * Knockback, ForceMode.Impulse);
    }   
}
