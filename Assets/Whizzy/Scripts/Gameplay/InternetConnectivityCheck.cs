using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.Net.NetworkInformation;

public class InternetConnectivityCheck : MonoBehaviour
{
    [Header("Internet Check Check")]
    [SerializeField] private float checkInterval = 5.0f;
    [SerializeField] private string host = "www.google.com"; 
    public bool Is_Connected_To_Internet { get; private set; }
    [field:SerializeField]public bool Check_Ping { get;private set; }
    [field:SerializeField]public bool Start_Timer { get;private set; }
    private float _timer;
    System.Net.NetworkInformation.Ping _ping = new System.Net.NetworkInformation.Ping();
    PingReply reply;
    private void Start()
    {
        _timer = checkInterval;
        Check_Ping = true;
    }

    private void FixedUpdate()
    {
        if (Check_Ping)
        {
            Is_Connected_To_Internet = false;
            Check_Ping = false;
            Start_Timer = true;
            _timer = checkInterval;
            reply = _ping.Send(host);
        }
        if (Start_Timer)
        {
            if (reply != null && reply.Status == IPStatus.Success)
            {
                Debug.Log("He is still there");
                Is_Connected_To_Internet = true;
                while (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    Debug.Log("In While");
                }
                if (_timer <= 0)
                {
                    _timer = 0;
                    Start_Timer = false;
                    reply = null;
                    Check_Ping = true;
                }
            }
            else if (_timer <= 0 && !Is_Connected_To_Internet && reply.Status != IPStatus.Success)
            {
                Debug.Log("The Else If");
                Start_Timer = false;
                _timer = 0;
                Is_Connected_To_Internet = false;
                reply = null;
                Check_Ping = true;
                Debug.Log("He understood");
            }
        }
    }
}
