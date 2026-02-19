using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennyScript : MonoBehaviour
{
    // set up a tile based movement system for the player character, using the arrow keys to move up, down, left, and right. The player should only be able to move one tile at a time, and should not be able to move through walls or other obstacles.
    // Start is called before the first frame update
    public GameObject benny;
    public float benny_x;
    public float benny_z;
    [SerializeField] float tileSize = 2;
    public float timeStepper = .2f;
    public float ticker = 0f;
    public bool isHoldingKey = false;

    public float benny_rotation = 1;
    public float CooldownTime;
    private float _nextAllowedInputTime;
    private bool alreadyTurned;

    //vectors

    void Start()
    {
        benny = GameObject.FindWithTag("Player");
        benny_x = benny.transform.position.x;
        benny_z = benny.transform.position.z;
    }

    void rotateBenny(int rotation)
    {
        benny_rotation += rotation;
        if (benny_rotation > 4) { benny_rotation = 1; };
        if (benny_rotation < 1) { benny_rotation = 4; };

        benny.transform.rotation = Quaternion.Euler(0, benny_rotation * 90, 0);
        _nextAllowedInputTime = Time.time + CooldownTime;
    }

    // Update is called once per frame
    void Update()
    {
        ticker += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        if (isHoldingKey && ticker >= timeStepper) // cont. press to cont. move

        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                switch (benny_rotation)
                {
                    case 1:
                        benny_z -= tileSize;
                        break;
                    case 2:
                        benny_x -= tileSize;
                        break;
                    case 3:
                        benny_z += tileSize;
                        break;
                    case 4:
                        benny_x += tileSize;
                        break;
                }

                benny.transform.position = new Vector3(benny_x, 1, benny_z);
                alreadyTurned = false;
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                switch (benny_rotation)
                {
                    case 1:
                        benny_z += tileSize;
                        break;
                    case 2:
                        benny_x += tileSize;
                        break;
                    case 3:
                        benny_z -= tileSize;
                        break;
                    case 4:
                        benny_x -= tileSize;
                        break;
                }
                benny.transform.position = new Vector3(benny_x, 1, benny_z);
                alreadyTurned = false;
            }

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (!alreadyTurned)
                {
                    switch (benny_rotation)
                    {
                        case 1:
                            benny_x += tileSize;
                            break;
                        case 2:
                            benny_z -= tileSize;
                            break;
                        case 3:
                            benny_x -= tileSize;
                            break;
                        case 4:
                            benny_z += tileSize;
                            break;
                    }
                }

                alreadyTurned = true;
                rotateBenny(-1);
                benny.transform.position = new Vector3(benny_x, 1, benny_z);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (!alreadyTurned)
                {
                    switch (benny_rotation)
                    {
                        case 1:
                            benny_x -= tileSize;
                            break;
                        case 2:
                            benny_z += tileSize;
                            break;
                        case 3:
                            benny_x += tileSize;
                            break;
                        case 4:
                            benny_z -= tileSize;
                            break;
                    }
                }

                alreadyTurned = true;
                rotateBenny(1);
                benny.transform.position = new Vector3(benny_x, 1, benny_z);
            }

            ticker = 0;
        }

        if (!isHoldingKey) // instant move w single press
        {
            ticker = timeStepper;
        }

        if (Time.time >= _nextAllowedInputTime)
        {
            if (Input.GetKey(KeyCode.Q))
            {
                rotateBenny(-1);

            }
            if (Input.GetKey(KeyCode.E))
            {
                rotateBenny(1);
            }
        }
        
    }
}