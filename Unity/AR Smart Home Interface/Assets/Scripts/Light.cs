using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    //public TCPTestServer server;

    void Start()
    {
        //server = gameObject.GetComponent<TCPTestServer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if(other.name == "Arrow")
        {
            //Debug.Log("Triggered");
            Color color;
            color = other.transform.Find("ArrowObject/Tip").gameObject.GetComponent<Renderer>().material.color;
            Debug.Log("Color: " + color);
            TCPTestServer.S.MessageAll("A," + color.r + "," + color.g + "," + color.b + "\r");
        }
    }

    public void LightOff()
    {
        TCPTestServer.S.MessageAll("B\r");
    }
}
