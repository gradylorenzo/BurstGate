using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LBCore;

public class temp_ShipSwitch : MonoBehaviour
{
    public ShipDynamics[] ships;
    private int i = 1;

    public void SwitchShips()
    {
        if(i == 1)
        {
            i = 0;
        }
        else
        {
            i = 1;
        }

        GameManagerCore.Events.EUpdatePlayerShip(ships[i]);
    }
}
