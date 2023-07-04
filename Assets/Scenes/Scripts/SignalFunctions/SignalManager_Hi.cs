using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SignalManager_Hi : MonoBehaviour
{
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    
    public void PlayerCount(int count)
    {
        text.SetText("" + count);
    }
}
