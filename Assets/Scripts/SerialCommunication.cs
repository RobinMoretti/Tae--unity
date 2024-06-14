using UnityEngine;
using System.IO.Ports;
using System.Threading;
using System;

public class SerialCommunication : MonoBehaviour
{
    private SerialPort serialPort;
    private Thread serialThread;
    private bool isRunning = true;
    private string data;
    private readonly object dataLock = new object();
    private string portName = "COM8"; // Remplacez par votre port série
    private int baudRate = 9600;
    private int readTimeout = 5000; // Timeout de lecture en millisecondes

    void Start()
    {
        OpenSerialPort();
        serialThread = new Thread(ReadSerialData);
        serialThread.Start();
    }

    void Update()
    {
        lock (dataLock)
        {
            if (!string.IsNullOrEmpty(data))
            {
                Debug.Log("Received: " + data);
                // Vous pouvez traiter les données reçues ici
                data = string.Empty; // Réinitialiser la donnée après traitement
            }
        }
    }

    void OnApplicationQuit()
    {
        isRunning = false;
        if (serialThread != null && serialThread.IsAlive)
        {
            serialThread.Join(); // Attendre que le thread se termine
        }
        CloseSerialPort();
    }

    private void OpenSerialPort()
    {
        serialPort = new SerialPort(portName, baudRate)
        {
            ReadTimeout = readTimeout
        };
        serialPort.RtsEnable = true;
        serialPort.DtrEnable = true;

        try
        {
            serialPort.Open();
            Debug.Log("Serial port opened: " + portName);
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Error opening serial port: " + ex.Message);
        }
    }

    private void CloseSerialPort()
    {
        if (serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("Serial port closed: " + portName);
        }
    }

    private void ReadSerialData()
    {
        while (isRunning)
        {
            if (serialPort.IsOpen)
            {
                try
                {
                    string incomingData = serialPort.ReadLine(); // Utiliser ReadLine pour lire une ligne complète
                    if (!string.IsNullOrEmpty(incomingData))
                    {
                        lock (dataLock)
                        {
                            data = incomingData;
                        }
                    }
                }
                catch (TimeoutException)
                {
                    // Timeout de lecture atteint, continuer la boucle
                    Debug.LogWarning("Serial read timeout");
                }
                catch (System.Exception ex)
                {
                    Debug.LogError("Error reading from serial port: " + ex.Message);
                }
            }
            Thread.Sleep(100); // Attendre un court moment avant de lire à nouveau pour éviter une surcharge du CPU
        }
    }
}
