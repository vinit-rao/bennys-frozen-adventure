using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BennyScript : MonoBehaviour
{
    // set up a tile based movement system for the player character, using the arrow keys to move up, down, left, and right. The player should only be able to move one tile at a time, and should not be able to move through walls or other obstacles.
    // Start is called before the first frame update
    public GameObject benny;
    public float bennny_x;
    public float benny_z;
    [SerializeField] float tileSize = 2;
    public float timeStepper = .2f;
    public float ticker = 0f;
    public bool isHoldingKey = false;
    void Start()
    {
        benny = GameObject.FindWithTag("Player");
        bennny_x = benny.transform.position.x;
        benny_z = benny.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        ticker += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        if (isHoldingKey && ticker >= timeStepper) // cont. press to cont. move

        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                benny.transform.position = new Vector3(bennny_x, 1, benny_z + tileSize);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                benny.transform.position = new Vector3(bennny_x, 1, benny_z - tileSize);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                benny.transform.position = new Vector3(bennny_x - tileSize, 1, benny_z);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                benny.transform.position = new Vector3(bennny_x + tileSize, 1, benny_z);
                bennny_x = benny.transform.position.x;
                benny_z = benny.transform.position.z;
            }
            ticker = 0f;
        }

        if (!isHoldingKey) // instant move w single press
        {
            ticker = timeStepper;
        }
    }
}
