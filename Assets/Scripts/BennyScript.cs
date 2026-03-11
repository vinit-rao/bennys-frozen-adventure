using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennyScript : MonoBehaviour
{
    // Benny movements
    public GameObject benny;
    public float benny_x;
    public float benny_z;
    public float tileSize = 1;
    public float timeStepper = .2f; // benny move ticker for holding down arrow keys
    public float ticker = 0f;
    public bool isHoldingKey = false;

    public float benny_rotation = 0f;
    public float CooldownTime;
    private float _nextAllowedInputTime;
    public int borderSize = 4;

    void Start()
    {
        benny = gameObject;

        benny_x = benny.transform.position.x;
        benny_z = benny.transform.position.z;


    }

    void Update()
    {
        ticker += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        if (isHoldingKey && ticker >= timeStepper) // hold down arrows to move continously

        {
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && benny_z < borderSize)
            {
                benny.transform.position = new Vector3(benny_x, 1, benny_z + tileSize);
                benny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && benny_z > -borderSize)
            {
                benny.transform.position = new Vector3(benny_x, 1, benny_z - tileSize);
                benny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }

            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && benny_x > -borderSize)
            {
                benny.transform.position = new Vector3(benny_x - tileSize, 1, benny_z);
                benny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && benny_x < borderSize)
            {
                benny.transform.position = new Vector3(benny_x + tileSize, 1, benny_z);
                benny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if (Input.GetKey(KeyCode.Q))
            {
                benny_rotation -= 90f;
                benny.transform.Rotate(0f, benny_rotation, 0f);
                _nextAllowedInputTime = Time.time + CooldownTime;
                benny_rotation = 0f;
            }
            if (Input.GetKey(KeyCode.E))
            {
                benny_rotation += 90f;
                benny.transform.Rotate(0f, benny_rotation, 0f);
                _nextAllowedInputTime = Time.time + CooldownTime;
                benny_rotation = 0f;
            }
            ticker = 0f;
        }

        if (!isHoldingKey) // instant move w single press
        {
            ticker = timeStepper;
        }

        
    }
}