using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.Threading;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int playerID;
    [SerializeField] Transform transforEnemy;

    bool lookAtEnemey;
    SerialPort dataStream;
    private Thread portReadingThread;

    private SerialPort serialPort;
    private Thread serialThread;
    private bool isRunning = false;
    private string receivedMessage;
    private readonly object lockObject = new object();

    // Adjust the port name and baud rate to match your Arduino settings
    public string portName; 
    public int baudRate = 9600;

    /*    serialport*/
    void Start()
    {
        OpenSerialPort();
    }


    private void Update()
    {
        lock (lockObject)
        {
            if (receivedMessage != null)
            {
                if(receivedMessage == "0x40x350x920xEB0x780x00x0")
                {
                    animator.SetTrigger("Kick");
                }
                else if (receivedMessage == "0xA30x6A0x760x35")
                {
                    animator.SetTrigger("JumpFront");
                }
                else if (receivedMessage == "0x330x2E0x120x35")
                {
                    animator.SetTrigger("BackKick");
                }

                Debug.Log("Received: " + receivedMessage);
                receivedMessage = null;
            }
        }


        if (playerID == 1){
            if(Input.GetKeyDown(KeyCode.A)){
                animator.SetTrigger("Kick");
                lookAtEnemey = false;
            }

            if(Input.GetKeyDown(KeyCode.B)){
                animator.SetTrigger("BackKick");
                lookAtEnemey = false;
            }

            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                animator.SetTrigger("JumpLeft");
            }

            if(Input.GetKeyDown(KeyCode.RightArrow)){
                animator.SetTrigger("JumpRight");
            }

            if(Input.GetKeyDown(KeyCode.UpArrow)){
                animator.SetTrigger("JumpFront");
            }
        }

        if(lookAtEnemey){
            transform.LookAt(transforEnemy);
        }
    }



    /// <summary>
    /// trigger this at the end of any kick
    /// </summary>

    public void enableLookAt(){
        lookAtEnemey = true;
    }

    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log("name = " + othe.name);

    // }



    private void ReadSerial()
    {
        while (isRunning && serialPort != null && serialPort.IsOpen)
        {
            print("message = ");
            try
            {
                string message = serialPort.ReadLine();
                print("message = " + message);
                lock (lockObject)
                {
                    receivedMessage = message;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("Error reading from serial port: " + e.Message);
            }
        }
    }


    void OnApplicationQuit()
    {
        serialPort.Close();
        serialThread.Abort();
        isRunning = false;
    }


    private void OpenSerialPort()
    {
        serialPort = new SerialPort(portName, baudRate);

        try
        {
            serialPort.Open();
            isRunning = true;
            serialThread = new Thread(ReadSerial);
            serialThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Error opening serial port: " + e.Message);
        }
    }
}
