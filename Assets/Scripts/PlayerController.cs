using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.Threading;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] int playerID;
    [SerializeField] Transform transforEnemy;

    [SerializeField] float endurance = 0;
    [SerializeField] float enduranceIncreaseRate = 1f;
    [SerializeField] float enduranceMaxValue = 5f;
    [SerializeField] RectTransform enduranceImage, enduranceImageParent;
    [SerializeField] GameObject enduranceUIStep;
    float enduranceImageParentWidth;

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
        enduranceImageParentWidth = enduranceImageParent.sizeDelta.x;

        Debug.Log("enduranceMaxValue = " + enduranceMaxValue );
        
        for (int i = 1; i < enduranceMaxValue; i++)
        {
            Debug.Log("i = " + i );
        
            GameObject step = Instantiate(enduranceUIStep);
            step.transform.parent = enduranceImageParent.transform;
            RectTransform rect = step.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2((enduranceImageParent.sizeDelta.x/enduranceMaxValue) * i,0); 
            // rect.sizeDelta = new Vector2(rect.sizeDelta.x, enduranceImageParent.sizeDelta.y);
        }
    }


    private void Update()
    {
        if(endurance < enduranceMaxValue){
            endurance += enduranceIncreaseRate * Time.deltaTime;
        }
        else{
            endurance = enduranceMaxValue;
        }
        updateEnduranceUI();

        lock (lockObject)
        {
            if (receivedMessage != null)
            {
                playCard(receivedMessage);              

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

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                animator.SetTrigger("JumpFront");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                animator.SetTrigger("JumpBack");
            }
        }

        if(lookAtEnemey){
            transform.LookAt(transforEnemy);
        }
    }

    void playCard(string cardId){
        if(cardId == "0x40x350x920xEB0x780x00x0")
        {
            if(endurance > 2){
                endurance -= 2;
                animator.SetTrigger("Kick");
            }
        }
        else if (cardId == "0xA30x6A0x760x35")
        {
            if(endurance > 1){
                endurance -= 1;
                animator.SetTrigger("JumpFront");
            }
        }
        else if (cardId == "0x330x2E0x120x35")
        {
            if(endurance > 3){
                endurance -= 3;
                animator.SetTrigger("BackKick");
            }
        }
    }

    void updateEnduranceUI(){
        enduranceImage.sizeDelta = new Vector2(endurance * enduranceImageParentWidth / enduranceMaxValue, enduranceImage.sizeDelta.y);
    }

    /// <summary>
    /// trigger this at the end of any kick
    /// </summary>

    public void enableLookAt(){
        lookAtEnemey = true;
    }

    private void ReadSerial()
    {
        while (isRunning && serialPort != null && serialPort.IsOpen)
        {
            try
            {
                string message = serialPort.ReadLine();
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
        try
        {
            serialPort = new SerialPort(portName, baudRate);
            serialPort.Open();
            isRunning = true;
            serialThread = new Thread(ReadSerial);
            serialThread.Start();
        }
        catch (Exception e)
        {
            print("Error opening serial port: " + e.Message);
        }
    }
}
