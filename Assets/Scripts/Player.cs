using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class Player : NetworkBehaviour
{
    public Material pcolor1, pcolor2;

    // Start is called before the first frame update
    void Start()
    {
        if (isLocalPlayer)
        {
            Button[] buts = GameObject.FindObjectsOfType<Button>();
            foreach (Button b in buts)
            {
                if (b.name == "LeftButton")
                    b.onClick.AddListener(delegate { transform.eulerAngles = transform.eulerAngles + new Vector3(0, -20, 0); });
                if (b.name == "RightButton")
                    b.onClick.AddListener(delegate { transform.eulerAngles = transform.eulerAngles + new Vector3(0, 20, 0); });
                if (b.name == "ForwardButton")
                    b.onClick.AddListener(delegate { transform.position = transform.position + transform.forward; });
                if (b.name == "BackButton")
                    b.onClick.AddListener(delegate { transform.position = transform.position + -1 * transform.forward; });
            }
        }
    }

    public override void OnStartServer()
    {
        Debug.Log("on start server");
    }

    public override void OnStartClient()
    {
        Debug.Log("on start client");
    }


    // Update is called once per frame
    void Update()
    {
        if (isLocalPlayer)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
                transform.position = transform.position + transform.forward;
            if (Input.GetKeyDown(KeyCode.DownArrow))
                transform.position = transform.position + -1 * transform.forward;
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                transform.eulerAngles = transform.eulerAngles + new Vector3(0, -20, 0);
            if (Input.GetKeyDown(KeyCode.RightArrow))
                transform.eulerAngles = transform.eulerAngles + new Vector3(0, 20, 0);

        }
    }
}
