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
        }
    }

    public static class GameManagerCore
    {
        #region Mouse control
        //Manager section controls how the mouse is used.
        private static bool _isMovingCamera;
        public static bool isMovingCamera
        {
            get { return _isMovingCamera; }
            set { _isMovingCamera = value; }
        }

        private static bool _isUsingInterface;
        public static bool isUsingInterface
        {
            get { return _isUsingInterface; }
            set { _isUsingInterface = value; }
        }

        public static bool isControllingShip
        {
            get
            {
                if (_isMovingCamera || _isUsingInterface)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        #endregion

        #region Events
        public static class Events
        {
            public delegate void e_UpdatePlayerShip(ShipDynamics sd);
            public static e_UpdatePlayerShip EUpdatePlayerShip;
        }
        #endregion
    }
}
