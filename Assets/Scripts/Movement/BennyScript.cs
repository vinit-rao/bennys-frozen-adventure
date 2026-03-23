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
    public bool isAnimating = false;

    void Start()
    {
        benny = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    void Update()
    {
        // this is to pause benny movement controls if pause menu is open
        if (UIManager.isPaused)
        {
            return;
        }

        ticker += Time.deltaTime;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E))
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        if (isHoldingKey && ticker >= timeStepper && !isAnimating) // hold down arrows to move continously
        {
            float moveX = 0f;
            float moveZ = 0f;

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) moveZ += tileSize;
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) moveZ -= tileSize;
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) moveX -= tileSize;
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) moveX += tileSize;

            bool isTurning = false;
            if (Input.GetKey(KeyCode.Q))
            {
                benny_rotation -= 90f;
                if (benny_rotation < 0) benny_rotation = 270;

                StartCoroutine(DoTurn(benny_rotation));
                isTurning = true;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                benny_rotation += 90f;
                if (benny_rotation == 360) benny_rotation = 0;

                StartCoroutine(DoTurn(benny_rotation));
                isTurning = true;
            }

            if (!isTurning && (moveX != 0f || moveZ != 0f))
            {
                Vector3 targetPos = new Vector3(
                    benny.transform.position.x + moveX,
                    benny.transform.position.y,
                    benny.transform.position.z + moveZ
                );

                if (targetPos.x >= -borderSize && targetPos.x <= borderSize &&
                    targetPos.z >= -borderSize && targetPos.z <= borderSize)
                {
                    StartCoroutine(DoHop(targetPos));
                }
            }

            ticker = 0f;
        }

        if (!isHoldingKey) // instant move w single press
        {
            ticker = timeStepper;
        }
    }

    private IEnumerator DoHop(Vector3 targetPos)
    {
        isAnimating = true;

        float hopHeight = 0.4f;
        float duration = timeStepper * 0.75f;
        float elapsed = 0f;

        Vector3 startPos = benny.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float moveT = 1f - Mathf.Pow(1f - t, 3f);
            float jumpT = Mathf.Sin(t * Mathf.PI);

            Vector3 currentPos = Vector3.Lerp(startPos, targetPos, moveT);
            currentPos.y += jumpT * hopHeight;

            benny.transform.position = currentPos;
            yield return null;
        }

        benny.transform.position = targetPos;
        isAnimating = false;
    }

    private IEnumerator DoTurn(float targetAngle)
    {
        isAnimating = true;

        float duration = timeStepper * 0.6f;
        float hopHeight = 0.3f;
        float elapsed = 0f;

        Quaternion startRot = benny.transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, targetAngle, 0);
        Vector3 startPos = benny.transform.position;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            float sprintT = 1f - Mathf.Pow(1f - t, 4f);
            float jumpT = Mathf.Sin(t * Mathf.PI);

            benny.transform.rotation = Quaternion.Slerp(startRot, endRot, sprintT);

            Vector3 currentPos = startPos;
            currentPos.y += jumpT * hopHeight;
            benny.transform.position = currentPos;

            yield return null;
        }

        benny.transform.rotation = endRot;
        benny.transform.position = startPos;
        isAnimating = false;
    }
}