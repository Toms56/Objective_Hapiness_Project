using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnCounterSystem : MonoBehaviour
{

    public Text turnDisplay;

    private int turn = 0;

    void OnEnable()
    {
        //UI_Manager.OnTimeAdvance += Advance;
    }

    void OnDisable()
    {
        //UI_Manager.OnTimeAdvance -= Advance;

    }

    public void Advance()
    {
        turn++;
        turnDisplay.text = string.Format("Turn: {0}", turn);
    }
}
