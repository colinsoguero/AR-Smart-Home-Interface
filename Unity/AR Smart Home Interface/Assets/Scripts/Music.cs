using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void PlayMusic()
    {
        TCPTestServer.S.MessageAll("P\r");
    }
    public void PauseMusic()
    {
        TCPTestServer.S.MessageAll("S\r");
    }
}
