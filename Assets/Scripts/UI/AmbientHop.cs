using UnityEngine;

public class AmbientHop: MonoBehaviour
{
    [Header("Hop Settings")]
    public float hopSpeed = 2f;
    public float hopHeight = 0.1f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float hopOffset = Mathf.Abs(Mathf.Sin(Time.time * hopSpeed)) * hopHeight;
        transform.localPosition = new Vector3(startPos.x, startPos.y + hopOffset, startPos.z);
    }
}