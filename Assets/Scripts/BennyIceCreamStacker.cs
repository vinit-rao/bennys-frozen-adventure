using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennyIceCreamStacker : MonoBehaviour
{
    // Ice cream attach when stacked
    public float stackHeight; // check for coordinate of the highest scoop
    void Start()
    {
        stackHeight = transform.position.y; // Initialize stack height to the position of the stacker itself
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag("Scoop"))
        {
            // Attach the collided scoop to the stacker
            other.transform.SetParent(transform);
            stackHeight = other.transform.position.y; // Update the stack height to the position of the newly attached scoop
        }
    }
}
