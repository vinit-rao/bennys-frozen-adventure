using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennyScript : MonoBehaviour
{
    // Benny movements
    public GameObject benny;
    public float bennny_x;
    public float bennny_y; // fixed
    public float benny_z;
    [SerializeField] float tileSize = 2;
    public float timeStepper = .2f; // benny move ticker for holding down arrow keys
    public float ticker = 0f;
    public bool isHoldingKey = false;

    public float benny_rotation = 0f;
    public float CooldownTime;
    private float _nextAllowedInputTime;

    void Start()
    {
        benny = GameObject.FindWithTag("Player");
        bennny_y = 1f; // fixed y
        benny.transform.position = new Vector3(0, bennny_y, 0); // spawn benny at x-z origin y = 1
        bennny_x = benny.transform.position.x;
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
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                benny.transform.position = new Vector3(bennny_x, bennny_y, benny_z + tileSize);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                benny.transform.position = new Vector3(bennny_x, bennny_y, benny_z - tileSize);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                benny.transform.position = new Vector3(bennny_x - tileSize, bennny_y, benny_z);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                benny.transform.position = new Vector3(bennny_x + tileSize, bennny_y, benny_z);
                bennny_x = benny.transform.position.x;
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