using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennyScript : MonoBehaviour
{
    // Benny movements
    public GameObject benny;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
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
            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && benny.transform.position.z < borderSize)
            {
                benny.transform.position = new Vector3(benny.transform.position.x, 1, benny.transform.position.z + tileSize);
            }
            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && benny.transform.position.z > -borderSize)
            {
                benny.transform.position = new Vector3(benny.transform.position.x, 1, benny.transform.position.z - tileSize);
            }
            if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && benny.transform.position.x > -borderSize)
            {
                benny.transform.position = new Vector3(benny.transform.position.x - tileSize, 1, benny.transform.position.z);
            }
            if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && benny.transform.position.x < borderSize)
            {
                benny.transform.position = new Vector3(benny.transform.position.x + tileSize, 1, benny.transform.position.z);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                benny_rotation -= 90f;
                if (benny_rotation < 0) benny_rotation = 270;

                benny.transform.Rotate(0, -90, 0);
                _nextAllowedInputTime = Time.time + CooldownTime;
            }
            if (Input.GetKey(KeyCode.E))
            {
                benny_rotation += 90f;
                if (benny_rotation == 360) benny_rotation = 0;

                benny.transform.Rotate(0, 90, 0);
                _nextAllowedInputTime = Time.time + CooldownTime;
            }
            ticker = 0f;
        }

        if (!isHoldingKey) // instant move w single press
        {
            ticker = timeStepper;
        }

        
    }
}