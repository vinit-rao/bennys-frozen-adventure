using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;

public class ArduinoController : MonoBehaviour
{
    public bool useArduinoController = false;
    public string portName = "COM3";
    public int baudRate = 9600;
    public float deadzone = 0.2f;

    private SerialPort serialPort;

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
    private bool middleLedOn = false; // Ignored for now!

    void Start()
    {
        serialPort = new SerialPort(portName, baudRate);
        serialPort.ReadTimeout = 10;
        try { serialPort.Open(); } catch (Exception e) { Debug.LogWarning(e.Message); }
    }

    void Update()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string data = serialPort.ReadLine();
                if (!string.IsNullOrEmpty(data))
                {
                    string[] values = data.Split(',');
                    if (values.Length == 4)
                    {
                        rawJoyX = int.Parse(values[0]);
                        rawJoyY = int.Parse(values[1]);
                        isLeftBtnPressed = int.Parse(values[2]) == 1;
                        isRightBtnPressed = int.Parse(values[3]) == 1;

                        // Normalize the math
                        joyX = -NormalizeAxis(rawJoyX);
                        joyY = NormalizeAxis(rawJoyY);
                    }
                }
            }
            catch (TimeoutException) { }
            catch (Exception e) { Debug.LogWarning(e.Message); }
        }
    }

    private float NormalizeAxis(int rawValue)
    {
        float normalized = (rawValue - 512f) / 512f;
        if (Mathf.Abs(normalized) < deadzone) return 0f;
        return Mathf.Clamp(normalized, -1f, 1f);
    }

    public void BlinkLeftLED()
    {
        StartCoroutine(BlinkRoutine(1, 0.25f)); // blinks for a quarter second
    }

    public void BlinkRightLED()
    {
        StartCoroutine(BlinkRoutine(2, 0.25f));
    }

    private IEnumerator BlinkRoutine(int ledID, float duration)
    {
        //led on
        if (ledID == 1) leftLedOn = true;
        if (ledID == 2) rightLedOn = true;
        SendLEDData();
        yield return new WaitForSeconds(duration);
        //led off
        if (ledID == 1) leftLedOn = false;
        if (ledID == 2) rightLedOn = false;
        SendLEDData();
    }

    private void SendLEDData()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            int l1 = leftLedOn ? 1 : 0;
            int l2 = rightLedOn ? 1 : 0;
            int l3 = middleLedOn ? 1 : 0;

            serialPort.Write($"{l1},{l2},{l3}\n");
        }
    }

    void OnDestroy()
    {
        if (serialPort != null && serialPort.IsOpen) serialPort.Close();
    }
}