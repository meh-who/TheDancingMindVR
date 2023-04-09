using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class JoinSoloRoom : MonoBehaviour
{
    private Realtime _realtime;
    private string _roomName;
    private bool _connected = false;

    private void Awake()
    {
        _realtime = GetComponent<Realtime>();
        ConnectUnqueRoom();
    }


    public void ConnectUnqueRoom()
    {
        // Set a unique seed value using the current time
        int seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);

        int Randint = Random.Range(0, 100);
        _roomName = "Lobby" + Randint;
        //Debug.Log(_roomName);

        if (!_connected)
        {
            _realtime.Connect(_roomName);
            _connected = true;
        }
        
        
    }

}
