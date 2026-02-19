using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamStacker : MonoBehaviour
{
    // Ice cream attach when stacked
    void Start()
    {
        
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
        }
    }
}
