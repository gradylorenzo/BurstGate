using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BGCore
{
    public class GameManagerComponent : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            GameManager.Events.EUpdatePlayerShip += EUpdatePlayerShip;
            GameManager.Events.EUpdateSelectedTarget += EUpdateSelectedTarget;
            GameManager.Events.EUpdatePendingSelection += EUpdatePendingSelection;
            GameManager.Events.EFloatingOriginOffsetUpdated += EFloatingOriginOffsetUpdated;
        }

        private void EFloatingOriginOffsetUpdated(DoubleVector2 v)
        {
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

    public static class GameManager
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

            public delegate void e_FloatingOriginOffsetUpdated(DoubleVector2 v);
            public static e_FloatingOriginOffsetUpdated EFloatingOriginOffsetUpdated;
        }
        #endregion

        public static class FloatingOrigin
        {
            public enum UpdateOffsetMode
            {
                Additive,
                Overwrite
            }
            private static DoubleVector2 dv2_currentOffset;
            public static DoubleVector2 currentOffset
            {
                get { return dv2_currentOffset; }
                private set { dv2_currentOffset = value; }
            }
            public static void UpdateFloatingOriginOffset(DoubleVector2 offset, UpdateOffsetMode mode)
            {
                if(mode == UpdateOffsetMode.Additive)
                {
                    currentOffset += offset;
                }
                else if(mode == UpdateOffsetMode.Overwrite)
                {
                    currentOffset = offset;
                }
                else
                {
                    Debug.LogError("UpdateOffsetMode must equal 'Additive' or 'Overwrite'. You bad. Stop hacking.");
                }

                Events.EFloatingOriginOffsetUpdated(dv2_currentOffset);
            }

            public static void UpdateFloatingOriginOffset(Vector3 offset, UpdateOffsetMode mode)
            {
                Vector2 newOffset = new Vector2(offset.x, offset.z);
                DoubleVector2 dv2_newOffset = DoubleVector2.FromVector2(newOffset);

                if (mode == UpdateOffsetMode.Additive)
                {
                    dv2_currentOffset += dv2_newOffset;
                }
                else if (mode == UpdateOffsetMode.Overwrite)
                {
                    dv2_currentOffset = dv2_newOffset;
                }
                else
                {
                    Debug.LogError("UpdateOffsetMode must equal 'Additive' or 'Overwrite'. You bad. Stop hacking.");
                }

                Events.EFloatingOriginOffsetUpdated(dv2_currentOffset);
            }
        }
    }
}
