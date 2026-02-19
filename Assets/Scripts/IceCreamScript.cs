using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamScript : MonoBehaviour
{
    private Rigidbody rb;
    // when collide with the same object, stack on top of each other
    void Start()
    {
        // infinite drag
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = Mathf.Infinity;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnCollisionEnter(Collision other)
{
    // Already attached? Do nothing.
    if (transform.parent != null) return;

    Transform t = other.transform;

    // Walk up the hierarchy to see if we hit Benny or something attached to him
    while (t != null)
    {
        if (t.CompareTag("Player")) // <-- change to Benny's tag
        {
            // Parent to Benny
            transform.SetParent(t);

            // Stop physics
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
                rb.isKinematic = true;
            }

            return;
        }

        t = t.parent;
    }
}
}
