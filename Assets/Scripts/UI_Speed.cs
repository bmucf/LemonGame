using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class UI_Speed : MonoBehaviour
{
    private TextMeshProUGUI speedModeText;
    public PlayerMovement johnLemon;
    void Start()
    {
        //speedModeText = GetComponent<TextMeshPro>();
        speedModeText = GetComponent<TextMeshProUGUI>();

    }
    void Update()
    {
        if (johnLemon.isSpeeding())
        {
            speedModeText.SetText("Press G to WALK");                                  
        }
        else
        {
            speedModeText.text = "Press G to RUN";
        }

    }
}
