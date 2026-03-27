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

    public bool isMoving = false;
    public bool isTurning = false;

    public AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public ArduinoController arduino;
    void Start()
    {
        benny = gameObject;
        benny_rotation = transform.eulerAngles.y;

    }

    void Update()
    {
        // this is to pause benny movement controls if pause menu is open
        if (UIManager.isPaused)
        {
            return;
        }

        // check if arduino is enabled
        bool useArd = arduino != null && arduino.useArduinoController;

        // updated movement code to check arrow keys, WASD keys, and arduino stick controls --vinit
        bool up = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || (useArd && arduino.UpPressed);
        bool down = Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) || (useArd && arduino.DownPressed);
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || (useArd && arduino.LeftPressed);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || (useArd && arduino.RightPressed);
        bool rotLeft = Input.GetKey(KeyCode.Q) || (useArd && arduino.RotateLeftPressed);
        bool rotRight = Input.GetKey(KeyCode.E) || (useArd && arduino.RotateRightPressed);

        ticker += Time.deltaTime;

        // holding check
        if (up || down || left || right || rotLeft || rotRight)
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        // benny turning while sliding (no hop tho cause it looks nicer)
        if (!isTurning && ticker >= timeStepper)
        {
            if (rotLeft)
            {
                benny_rotation -= 90f;
                if (benny_rotation < 0) benny_rotation = 270;

                StartCoroutine(DoTurn(benny_rotation));
            }
            else if (rotRight)
            {
                benny_rotation += 90f;
                if (benny_rotation == 360) benny_rotation = 0;

                StartCoroutine(DoTurn(benny_rotation));
            }
        }

        // move benny
        if (isHoldingKey && ticker >= timeStepper && !isMoving)
        {
            float moveX = 0f;
            float moveZ = 0f;

            if (up) moveZ += tileSize;
            if (down) moveZ -= tileSize;
            if (left) moveX -= tileSize;
            if (right) moveX += tileSize;

            if (moveX != 0f || moveZ != 0f)
            {
                Vector3 targetPos = new Vector3(
                    benny.transform.position.x + moveX,
                    benny.transform.position.y,
                    benny.transform.position.z + moveZ
                );

                if (targetPos.x >= -borderSize && targetPos.x <= borderSize &&
                    targetPos.z >= -borderSize && targetPos.z <= borderSize)
                {
                    StartCoroutine(DoMove(targetPos));
                }
            }

            ticker = 0f;
        }

        if (!isHoldingKey) // instant move w single press
        {
            ticker = timeStepper;
        }
    }

    private IEnumerator DoMove(Vector3 targetPos)
    {
        isMoving = true;

        float duration = timeStepper * 0.75f;
        float elapsed = 0f;

        Vector3 startPos = benny.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float curveT = moveCurve.Evaluate(t);

            float newX = Mathf.Lerp(startPos.x, targetPos.x, curveT);
            float newZ = Mathf.Lerp(startPos.z, targetPos.z, curveT);

            benny.transform.position = new Vector3(newX, benny.transform.position.y, newZ);
            yield return null;
        }

        benny.transform.position = new Vector3(targetPos.x, benny.transform.position.y, targetPos.z);
        isMoving = false;
    }

    private IEnumerator DoTurn(float targetAngle)
    {
        isTurning = true;

        float duration = timeStepper * 0.6f;
        float hopHeight = 0.3f;
        float elapsed = 0f;

        // if he is already sliding he does not hop when rotating
        bool doHop = !isMoving;

        Quaternion startRot = benny.transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, targetAngle, 0);
        float startY = benny.transform.position.y;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float sprintT = 1f - Mathf.Pow(1f - t, 4f);

            benny.transform.rotation = Quaternion.Slerp(startRot, endRot, sprintT);

            if (doHop)
            {
                float jumpT = Mathf.Sin(t * Mathf.PI);
                float newY = startY + (jumpT * hopHeight);
                benny.transform.position = new Vector3(benny.transform.position.x, newY, benny.transform.position.z);
            }

            yield return null;
        }

        benny.transform.rotation = endRot;

        if (doHop)
        {
            benny.transform.position = new Vector3(benny.transform.position.x, startY, benny.transform.position.z);
        }

        isTurning = false;
    }
}