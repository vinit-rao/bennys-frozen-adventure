using UnityEngine;
using System.IO.Ports;

public class BennyScript : MonoBehaviour
{
    public GameObject benny;
    public float bennny_x;
    public float benny_z;
    [SerializeField] float tileSize = 2;
    public float timeStepper = .2f;
    public float ticker = 0f;
    public bool isHoldingKey = false;

    // Use confirmed port for your Mac
    SerialPort sp = new SerialPort("/dev/cu.usbmodem2101", 9600);
    int joyX = 512, joyY = 512, potVal = 512;

    void Start()
    {
        benny = GameObject.FindWithTag("Player");
        bennny_x = benny.transform.position.x;
        benny_z = benny.transform.position.z;

        try
        {
            if (!sp.IsOpen)
            {
                sp.Open();
                sp.ReadTimeout = 10;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Arduino Error: " + e.Message);
        }
    }

    void Update()
    {
        ReadArduino();
        ticker += Time.deltaTime;

        // Rotation from Potentiometer
        float rotationAngle = (potVal / 1023f) * 360f;
        benny.transform.rotation = Quaternion.Euler(0, rotationAngle, 0);

        // Movement with wider Deadzones (200 and 800) to stop ghosting
        bool joystickMoving = (joyX < 200 || joyX > 800 || joyY < 200 || joyY > 800);

        if (Input.anyKey || joystickMoving)
        {
            isHoldingKey = true;
        }
        else
        {
            isHoldingKey = false;
        }

        if (isHoldingKey && ticker >= timeStepper)
        {
            // Using else-if ensures only one move per tick
            if (Input.GetKey(KeyCode.W) || joyY < 200) MoveBenny(0, tileSize);
            else if (Input.GetKey(KeyCode.S) || joyY > 800) MoveBenny(0, -tileSize);
            else if (Input.GetKey(KeyCode.A) || joyX < 200) MoveBenny(-tileSize, 0);
            else if (Input.GetKey(KeyCode.D) || joyX > 800) MoveBenny(tileSize, 0);

            ticker = 0f;
        }

        if (!isHoldingKey) ticker = timeStepper;
    }

    void MoveBenny(float xOff, float zOff)
    {
        benny.transform.position = new Vector3(bennny_x + xOff, 1, benny_z + zOff);
        bennny_x = benny.transform.position.x;
        benny_z = benny.transform.position.z;
    }

    void ReadArduino()
    {
        if (sp.IsOpen)
        {
            try
            {
                // Keep reading until the buffer is EMPTY so we only have the newest data
                while (sp.BytesToRead > 0)
                {
                    string data = sp.ReadLine();
                    string[] values = data.Split(',');
                    if (values.Length == 3)
                    {
                        joyX = int.Parse(values[0]);
                        joyY = int.Parse(values[1]);
                        potVal = int.Parse(values[2]); // Corrected index for 3 values
                    }
                }
            }
            catch { }
        }
    }

    void OnApplicationQuit()
    {
        if (sp.IsOpen) sp.Close(); // Vital to prevent port lock-up
    }
}