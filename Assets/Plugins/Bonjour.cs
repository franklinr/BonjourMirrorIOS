using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
// taken from https://docs.unity3d.com/2019.4/Documentation/Manual/UnityasaLibrary-iOS.html

public class Bonjour
{

    /* Interface to native implementation */

    [DllImport("__Internal")]
    private static extern void _StartLookup(string service, string domain);

    [DllImport("__Internal")]
    private static extern string _GetLookupStatus();

    [DllImport("__Internal")]
    private static extern int _GetServiceCount();

    [DllImport("__Internal")]
    private static extern void _Stop();

    [DllImport("__Internal")]
    private static extern string _GetServiceName(int serviceNumber);

    [DllImport("__Internal")]
    private static extern void _startService(string type, string name, int port, string domain);
    //_startService(int port);

    [DllImport("__Internal")]
    private static extern void _stopService();

    //  [DllImport("__Internal")]
    //  private static extern void _triggerLocalNetworkPrivacyAlert();

    /* Public interface for use inside C# / JS code */
    /*   public static void triggerLocalNetPrivacyAlertObjC()
       {
           if (Application.platform != RuntimePlatform.OSXEditor)
               _triggerLocalNetworkPrivacyAlert();
       }*/

    /* Public interface for use inside C# / JS code */
    public static void StartService(string type, string name, int port, string domain)
    {
        Debug.Log("start service" + type + " port " + port);
        if (Application.platform != RuntimePlatform.OSXEditor)
            _startService(type, name, port, domain);
    }

    // Starts lookup for some bonjour registered service inside specified domain
    public static void StartLookup(string service, string domain)
    {
        // Call plugin only when running on real device
        if (Application.platform != RuntimePlatform.OSXEditor)
            _StartLookup(service, domain);
    }

    // Stops lookup current lookup
    public static void StopLookup()
    {
        // Call plugin only when running on real device
        if (Application.platform != RuntimePlatform.OSXEditor)
            _Stop();
    }

    // Returns current lookup status
    public static string GetLookupStatus()
    {
        // Call plugin only when running on real device
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            return _GetLookupStatus();
        }
        // Return mockup values for code running inside Editor
        else
        {
            return "Done";
        }
    }

    // Returns list of looked up service hosts
    public static string[] GetServiceNames()
    {
        // Call plugin only when running on real device
        if (Application.platform != RuntimePlatform.OSXEditor)
        {
            string[] res = new string[_GetServiceCount()];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = _GetServiceName(i);
            }

            return res;
        }
        // Return mockup values for code running inside Editor
        else
        {
            string[] res = { "hostname1", "hostname2", "hostname3", "hostname4" };
            return res;
        }
    }
}
