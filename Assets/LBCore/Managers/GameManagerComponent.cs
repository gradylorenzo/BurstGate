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
            GameManager.Events.EFloatingOriginOffsetDelta += EFloatingOriginOffsetDelta;
        }

        private void EFloatingOriginOffsetUpdated(DoubleVector2 v)
        {
        }
        private void EFloatingOriginOffsetDelta(DoubleVector2 v)
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
            public static e_FloatingOriginOffsetUpdated EFloatingOriginOffsetDelta;
        }
        #endregion

        public static class FloatingOrigin
        {
            public enum UpdateOffsetMode
            {
                Additive,
                Overwrite
            }
            private static DoubleVector2 dv2_lastOffset;
            private static DoubleVector2 dv2_currentOffset;
            private static DoubleVector2 dv2_offsetDelta;
            public static DoubleVector2 currentOffset
            {
                get { return dv2_currentOffset; }
                private set { dv2_currentOffset = value; }
            }
            public static DoubleVector2 offsetDelta
            {
                get { return dv2_offsetDelta; }
                private set { dv2_offsetDelta = value; }
            }

            public static void UpdateFloatingOriginOffset(Vector3 offset, UpdateOffsetMode mode)
            {
                dv2_lastOffset = dv2_currentOffset;
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

                dv2_offsetDelta = dv2_currentOffset - dv2_lastOffset;
                Debug.Log(dv2_offsetDelta.ToString());
                Events.EFloatingOriginOffsetUpdated(dv2_currentOffset);
                Events.EFloatingOriginOffsetDelta(dv2_offsetDelta);
            }
        }
    }
}
