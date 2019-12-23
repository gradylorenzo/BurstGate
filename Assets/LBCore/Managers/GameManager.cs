using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LBCore
{
    public class GameManager : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameManagerCore.Events.EUpdatePlayerShip += EUpdatePlayerShip;
            GameManagerCore.Events.EUpdateSelectedTarget += EUpdateSelectedTarget;
            GameManagerCore.Events.EUpdatePendingSelection += EUpdatePendingSelection;
        }

        private void EUpdatePlayerShip(ShipDynamics sd)
        {
        }
        private void EUpdateSelectedTarget(Transform t)
        {
        }
        private void EUpdatePendingSelection(Transform t, float progress, float max)
        {
        }
    }

    public static class GameManagerCore
    {
        #region Mouse control
        //Manager section controls how the mouse is used.
        private static bool _isUsingInterface;
        public static bool isUsingInterface
        {
            get { return _isUsingInterface; }
            set { _isUsingInterface = value; }
        }

        #endregion

        #region Events
        public static class Events
        {
            public delegate void e_UpdatePlayerShip(ShipDynamics sd);
            public static e_UpdatePlayerShip EUpdatePlayerShip;

            public delegate void e_UpdateSelectedTarget(Transform t);
            public static e_UpdateSelectedTarget EUpdateSelectedTarget;

            public delegate void e_UpdatePendingTarget(Transform t, float progress, float max);
            public static e_UpdatePendingTarget EUpdatePendingSelection;
        }
        #endregion
    }
}
