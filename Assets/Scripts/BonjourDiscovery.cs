using UnityEngine;
using System.Net;
using Mirror;
using UnityEngine.UI;
using System.Net.Sockets;
using System;

public class BonjourDiscovery : MonoBehaviour
{

    public Text statusout, clientText;
    public Button ServerButton, ClientButton;
    bool querying = false;

    string label;
    string status, broadcastStatus = "no broadcast";

    string service = "_bonjourmirror._tcp";

    string[] services = new System.String[0];
    string[] oldservices = new System.String[0];

    public MyNetworkManager manager;

    GUIStyle labelStyle;


    void Start()
    {
        // Screen.SetResolution(640, 480, true);
        clientText = ClientButton.GetComponent<Text>();
    }



    public void StartBroadcastService()
    {
        string add = GetLocalIPAddressSockets();

        // Start lookup for specified service inside "local" domain
        Bonjour.StartService(service + ".", add, 5000, "local");

        broadcastStatus = "broad " + add;
        statusout.text = statusout.text + "\nbroad " + add;
        manager.StartHost();
    }

    public void ToggleLookup()
    {
        if (querying)
        {
            Debug.Log("Stop lookup");
            Bonjour.StopLookup();
            clientText.text = "Query";
            statusout.text = statusout.text + "\nstop";
        }
        else
        {
            // Start lookup for specified service inside "local" domain
            Debug.Log("Start lookup");
            Bonjour.StartLookup(service, "local.");
            querying = true;
            status = "";
            statusout.text = statusout.text + "\nquery ";

            clientText.text = "Stop";
        }

    }

    void ReportServiceConnections()
    {
        // List of looked up services
        for (int i = 0; i < services.Length; i++)
        {
            // check if this service has been reported before
            bool old = false;
            for (int j = 0; j < oldservices.Length; j++)
            {
                if (oldservices[j] == services[i]) old = true;
                Debug.Log(oldservices[j]);
            }


            if (services[i] != GetLocalIPAddressSockets() && !old)
            {
                manager.networkAddress = services[i];

                if (manager.networkAddress != "localhost")
                {
                    statusout.text = statusout.text + "\nstart client " + manager.networkAddress;

                    Debug.Log("start client " + manager.networkAddress);
                    manager.StartClient();
                }
            }
        }
    }

    private void Update()
    {
        if (querying)
        {
            if (Time.frameCount % 10 == 0)
            {
                status = Bonjour.GetLookupStatus();
                oldservices = services;
                services = Bonjour.GetServiceNames();
                label = status;
            }

            if (status == "Done")
                querying = false;
        }

        ReportServiceConnections();
    }


    //// Different ways to get your ip address

    public static string GetLocalIPAddressSockets()
    {
        string localIP = string.Empty;
        using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
        {
            socket.Connect("8.8.8.8", 65530);
            IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
            localIP = endPoint.Address.ToString();
        }
        Console.WriteLine("IP Address = " + localIP);
        return (localIP);
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            Debug.Log("ip " + ip.ToString());
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.GetValue(0).ToString();
    }



}

