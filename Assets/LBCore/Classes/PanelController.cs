﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BGCore;
using System;

public class PanelController : MonoBehaviour
{
    public ShipDynamics sd;

    public Text velocityText;

    public Image inertialDampenerIndicator;

    public RectTransform dockingPanel;
    public Image dockingIndicator;
    public Image pendingIndicator;
    private float wantedDockingIndicatorPosition;
    private float currentDockingIndicatorPosition;

    public void Awake()
    {
        GameManager.Events.EUpdatePlayerShip += EUpdatePlayerShip;
        GameManager.Events.EUpdatePendingSelection += EUpdatePendingTarget;
        GameManager.Events.EUpdateSelectedTarget += EUpdateSelectedTarget;
    }

    private void EUpdatePendingTarget(Transform t, float progress, float max)
    {
        if(progress > 0)
        {
            pendingIndicator.rectTransform.position = Input.mousePosition;
            pendingIndicator.fillAmount = 1 - progress / max;
        }
    }

    private void EUpdateSelectedTarget(Transform t)
    {
        pendingIndicator.fillAmount = 0;
    }

    private void EUpdatePlayerShip(ShipDynamics sd)
    {
        this.sd = sd;
    }

    public void Update()
    {
        if (sd != null)
        {
            velocityText.text = "V: " + sd.CurrentVelocity.ToString("0.0") + " m/s";
            if (sd.UseDampeners)
            {
                inertialDampenerIndicator.color = Color.green;
            }
            else
            {
                inertialDampenerIndicator.color = Color.red;
            }

            switch (sd.DockingState)
            {
                case ShipDynamics.DockingStates.None:
                    dockingIndicator.color = Color.red;
                    wantedDockingIndicatorPosition = -200.0f;
                    break;

                case ShipDynamics.DockingStates.OutOfRange:
                    dockingIndicator.color = Color.red;
                    wantedDockingIndicatorPosition = 0f;
                    break;

                case ShipDynamics.DockingStates.WithinRange:
                    dockingIndicator.color = Color.yellow;
                    wantedDockingIndicatorPosition = 0f;
                    break;

                case ShipDynamics.DockingStates.Docked:
                    dockingIndicator.color = Color.green;
                    wantedDockingIndicatorPosition = 0f;
                    break;
            }

            currentDockingIndicatorPosition = Mathf.Lerp(currentDockingIndicatorPosition, wantedDockingIndicatorPosition, 0.1f);
            dockingPanel.position = new Vector3(currentDockingIndicatorPosition, dockingPanel.position.y, dockingPanel.position.z);
        }
    }
}
