using UnityEngine;
using System.Net;
using Mirror;

public class BonjourTest : MonoBehaviour
{

    bool querying = false;

    string label;
    string status, broadcastStatus = "no broadcast";
    // Default service name. _http._tcp corresponds to http services (f.e. printers)
    // string service = "_http._tcp.";
    string service = "_tictactoe._tcp";

    string[] services = new System.String[0];

    int centerX = 4 * Screen.width / 5;
    int buttonHeight = 70;
    int fontsize = 30;
    int startY = 140;
    public NetworkManager manager;

    GUIStyle labelStyle;


    void Start()
    {
        // Screen.SetResolution(640, 480, true);
    }

    public string GetLocalIPv4()
    {
        return Dns.GetHostEntry(Dns.GetHostName()).AddressList.GetValue(0).ToString();
    }

    void StartBroadcastService()
    {
        IOSNetworkPermission.TriggerDialog();
        string add = GetLocalIPv4();
        // Start lookup for specified service inside "local" domain
        Bonjour.StartService(service + ".", add, 47777, "local");

        broadcastStatus = "broad " + add;

        manager.StartHost();
    }

    void StartLookup()
    {
        // Start lookup for specified service inside "local" domain
        Bonjour.StartLookup(service, "local.");
        querying = true;
        status = "";


    }

    void OnGUI()
    {


        // GUI.skin.box = labelStyle;
        GUI.skin.button.fontSize = fontsize;
        GUI.skin.textField.fontSize = fontsize;
        GUI.skin.textField.alignment = TextAnchor.MiddleLeft;
        GUI.skin.label.fontSize = fontsize;
        GUI.skin.label.alignment = TextAnchor.MiddleLeft;
        //   GUI.Label(new Rect(centerX - 50, centerY - buttonHeight - 10, buttonHeight * 3, buttonHeight), "Bonjour Client");

        GUI.Label(new Rect(centerX - buttonHeight * 4, startY, buttonHeight * 5, buttonHeight - 10), broadcastStatus);

        if (!querying && GUI.Button(new Rect(centerX + buttonHeight * 2 - 10, startY, buttonHeight * 3, buttonHeight - 10), "Broadcast"))
        {
            StartBroadcastService();
        }

        service = GUI.TextField(new Rect(centerX - buttonHeight * 4, startY + buttonHeight, buttonHeight * 5, buttonHeight - 10), service);

        if (!querying && GUI.Button(new Rect(centerX + buttonHeight * 2, startY + buttonHeight, buttonHeight * 3, buttonHeight - 10), "Query"))
        {
            StartLookup();

        }
        if (querying)
        {
            // Query status only every 10th frame. Managed -> Native calls are quite expensive.
            // Similar coding pattern could be considered as good practice. 
            if (Time.frameCount % 10 == 0)
            {
                status = Bonjour.GetLookupStatus();
                services = Bonjour.GetServiceNames();
                label = status;
            }

            if (status == "Done")
                querying = false;

            //Stop lookup
            if (querying && GUI.Button(new Rect(centerX + buttonHeight * 2, startY + buttonHeight, buttonHeight * 3, buttonHeight - 10), "Stop"))
                Bonjour.StopLookup();

        }

        // Display status
        GUI.Label(new Rect(centerX - buttonHeight * 2, startY + buttonHeight * 2, buttonHeight * 5, buttonHeight - 10), label);

        // List of looked up services
        for (int i = 0; i < services.Length; i++)
        {
            if (services[i] != GetLocalIPv4())
            {
                manager.networkAddress = services[i];
                GUI.Button(new Rect(centerX - buttonHeight * 2, startY + buttonHeight * 3 + i * buttonHeight, buttonHeight * 5, buttonHeight - 10), manager.networkAddress);

                if (manager.networkAddress != "localhost")
                {
                    Debug.Log("start client " + manager.networkAddress);
                    manager.StartClient();
                    //  this.gameObject.SetActive(false);
                }

            }
        }
    }
}
