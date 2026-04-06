using UnityEngine;
using System.Collections;
using System;

#if !UNITY_WEBGL || UNITY_EDITOR
using System.IO.Ports;
#endif

public class ArduinoController : MonoBehaviour
{
    public bool useArduinoController = false;
    public string portName = "/dev/cu.usbmodem2101";
    public int baudRate = 9600;
    public float deadzone = 0.2f;

#if !UNITY_WEBGL || UNITY_EDITOR
    private SerialPort serialPort;
#endif

    public int rawJoyX;
    public int rawJoyY;
    public bool isLeftBtnPressed;
    public bool isRightBtnPressed;

    public float joyX;
    public float joyY;

    public bool UpPressed => joyY > 0.5f;
    public bool DownPressed => joyY < -0.5f;
    public bool RightPressed => joyX > 0.5f;
    public bool LeftPressed => joyX < -0.5f;
    public bool RotateLeftPressed => isLeftBtnPressed;
    public bool RotateRightPressed => isRightBtnPressed;

    private bool leftLedOn = false;
    private bool rightLedOn = false;
    private bool middleLedOn = false;

    void Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 10;
        try { serialPort.Open(); } catch (Exception e) { Debug.LogWarning(e.Message); }
#endif
    }

    void Update()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (serialPort != null && serialPort.IsOpen)
        {
            string latestValidData = "";
            try
            {
                while (serialPort.BytesToRead > 0)
                {
                    latestValidData = serialPort.ReadLine().Trim();
                }
            }
            catch (TimeoutException) { }
            catch (Exception e) { Debug.LogWarning(e.Message); }
            if (!string.IsNullOrEmpty(latestValidData))
            {
                string[] values = latestValidData.Split(',');
                if (values.Length == 4)
                {
                    if (int.TryParse(values[0], out int rx) &&
                        int.TryParse(values[1], out int ry) &&
                        int.TryParse(values[2], out int lb) &&
                        int.TryParse(values[3], out int rb))
                    {
                        rawJoyX = rx;
                        rawJoyY = ry;
                        isLeftBtnPressed = lb == 1;
                        isRightBtnPressed = rb == 1;

                        joyX = -NormalizeAxis(rawJoyX);
                        joyY = NormalizeAxis(rawJoyY);
                    }
                }
            }
        }
#endif
    }

    private float NormalizeAxis(int rawValue)
    {
        float normalized = (rawValue - 512f) / 512f;
        if (Mathf.Abs(normalized) < deadzone) return 0f;
        return Mathf.Clamp(normalized, -1f, 1f);
    }

    public void BlinkLeftLED()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        StartCoroutine(BlinkRoutine(1, 0.25f)); // blinks for a quarter second
#endif
    }

    public void BlinkRightLED()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        StartCoroutine(BlinkRoutine(2, 0.25f));
#endif
    }

    public void BlinkMiddleLED()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        StartCoroutine(BlinkRoutine(3, 0.5f)); // blinks for half a second for a completed order
#endif
    }

    private IEnumerator BlinkRoutine(int ledID, float duration)
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        //led on
        if (ledID == 1) leftLedOn = true;
        if (ledID == 2) rightLedOn = true;
        if (ledID == 3) middleLedOn = true;
        SendLEDData();

        yield return new WaitForSeconds(duration);

        //led off
        if (ledID == 1) leftLedOn = false;
        if (ledID == 2) rightLedOn = false;
        if (ledID == 3) middleLedOn = false;
        SendLEDData();
#else
        yield return null; 
#endif
    }

    private void SendLEDData()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (serialPort != null && serialPort.IsOpen)
        {
            int l1 = leftLedOn ? 1 : 0;
            int l2 = rightLedOn ? 1 : 0;
            int l3 = middleLedOn ? 1 : 0;

            serialPort.Write($"{l1},{l2},{l3}\n");
        }
#endif
    }

    void OnDestroy()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        if (serialPort != null && serialPort.IsOpen) serialPort.Close();
#endif
    }
}