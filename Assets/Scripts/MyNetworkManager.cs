using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MyNetworkManager : NetworkManager
{

    public override void OnServerConnect(NetworkConnection nc)
    {

        // it means "here on the server" one of the clients has connected
        base.OnServerConnect(nc);

        Debug.Log("a client connected " + nc.connectionId + " " + nc.ToString());
        Debug.Log("address? " + nc.address);
    }

    public override void OnServerDisconnect(NetworkConnection nc)
    {
    }
}
