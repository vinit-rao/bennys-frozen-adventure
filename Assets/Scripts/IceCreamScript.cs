using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamScript : MonoBehaviour
{
    private Rigidbody rb;
    public BennyIceCreamStacker stackHeight;
    // when collide with the same object, stack on top of each other
    void Start()
    {
        // infinite drag
        rb = GetComponent<Rigidbody>();
        rb.angularDrag = Mathf.Infinity;
        stackHeight = BennyIceCreamStacker.FindObjectOfType<BennyIceCreamStacker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

private void OnCollisionEnter(Collision other)
{
    if (other.transform.CompareTag("Scoop"))
    {
        // when a scoop collides with another scoop, set transform to Benny (tag "Player")
        other.transform.SetParent(stackHeight.transform);
        stackHeight.stackHeight = other.transform.position.y; // Update the stack height to the position of the newly attached scoop
    }
}
}
