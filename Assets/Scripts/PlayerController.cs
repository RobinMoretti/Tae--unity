using UnityEngine;
using UnityEngine.InputSystem;
using System.IO.Ports;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System;
using System.Threading;
using UnityEngine.UI;
using System.Security.Cryptography;
using NUnit.Framework;
using System.Collections.Generic;

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

    bool lookAtEnemey = true;
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

    [SerializeField] GameController gameController;

    [SerializeField] CardsManager cardsManager;

    List<string> usedCards;

    /*    serialport*/
    void Start()
    {
        usedCards = new List<string>();
        cardsManager = GetComponent<CardsManager>();

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

    void saluToStartGame()
    {

    }


    void playCard(string cardId){
        string action = cardsManager.getCardAction(cardId);

        if (usedCards.Contains(cardId))
        {
            return;
        }

        if (action != "false")
        {
            if (action == "JumpLeft")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "JumpRight")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "JumpFront")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "JumpBack")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "Salu")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "FrontKick")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "SideKick")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "Combo1")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "RunAndKick")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
            }
            if (action == "BackKick")
            {
                if (endurance > 2)
                {
                    animator.SetTrigger(action);
                    endurance -= 2;
                }
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
/*
    public void pauseAnimation()
    {
        animator.speed = 0;
    }
    public void unpauseAnimation()
    {
        animator.speed = 1;
    }*/

    void toggleReadyToPlay()
    {
/*        GameController*/
    }
}
