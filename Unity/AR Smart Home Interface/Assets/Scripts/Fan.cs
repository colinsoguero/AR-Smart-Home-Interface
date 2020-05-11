using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.name);
        if (other.name == "Arrow")
        {
         TCPTestServer.S.MessageAll("F\r");
        }
    }

    public void FanOff()
    {
        TCPTestServer.S.MessageAll("O\r");
    }
}
