using UnityEngine;
using Telepathy;

/// <summary>
/// Only needed on iOS. Triggers network access request message.
/// </summary>
public class NetworkAccessDialogTrigger : MonoBehaviour
{
    /// <summary>
    /// Whether or not to ask user for network access at startup.
    /// </summary>
    public bool AskAtStartup;

    /// <summary>
    /// Network client.
    /// </summary>
    private Client client;

    /// <summary>
    /// Whether or not the "client" is currently active.
    /// </summary>
    private bool active = false;

    void Start()
    {
        if (AskAtStartup)
        {
            TriggerNetworkPermissionRequest();
        }
    }

    /// <summary>
    /// Workaround to trigger the iOS local network access
    /// permission dialog by connecting to 255.255.255.255
    /// </summary>
    public void TriggerNetworkPermissionRequest()
    {


        Debug.Log("Requesting network access permission.");

        // active = true;

        // Trigger dialog.
        client = new Client(2000);
        client.Connect("255.255.255.255", 5001);

        // Abort connection shortly after.
        Invoke(nameof(Disconnect), 2);
    }

    /// <summary>
    /// Abort connection.
    /// </summary>
    private void Disconnect()
    {
        if (client.Connected || client.Connecting)
        {
            client.Disconnect();
        }

        active = false;
    }
}