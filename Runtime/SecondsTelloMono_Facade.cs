using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace Eloi.SecondsTello { 
    public class SecondsTelloMono_Facade : MonoBehaviour
    {
        public Transform m_startPoint;
        public Transform m_endPoint;
        public Transform m_dronePoint;
        public Transform m_fireDirection;

        public Vector2 m_joystickLeft;
        public Vector2 m_joystickRight;
        public UnityEvent<Vector2, Vector2> m_onJoystickLeftAndRightUpdated;
        public UnityEvent m_onFireRequested;
        public UnityEvent m_onRestartLevelRequested;
        public UnityEvent m_onNextLevelRequested;

        /// <summary>
        /// Ask the game to restart the level
        /// </summary>
        /// 
        [ContextMenu("Restart Level")]
        public void RestartTheLevel()
        {
            m_onRestartLevelRequested.Invoke();
        }
        /// <summary>
        /// Ask to go in the next level.
        /// </summary>
        [ContextMenu("Next Level")]
        public void NextTheLevel()
        {
            m_onNextLevelRequested.Invoke();
        }

        public void RotateLeft(float percent = 1) => m_joystickLeft.x = -percent; 
        public void RotateRight(float percent = 1) => m_joystickLeft.x = percent; 
        public void StopRotating() => m_joystickLeft.x = 0;
 

        public void MoveUp(float percent = 1) { m_joystickLeft.y = -percent; }
        public void MoveDown(float percent = 1) { m_joystickLeft.y = percent; }
        public void MoveRight(float percent = 1) { m_joystickRight.x = -percent; }
        public void MoveLeft(float percent = 1) { m_joystickRight.x = percent; }
        public void MoveForward(float percent = 1) { m_joystickLeft.y = percent; }
        public void MoveBackward(float percent = 1) { m_joystickLeft.y = -percent; }

        public void StopMovingOnVertical() => m_joystickLeft.y = 0;
        public void StopMovingOnHorizontal() => m_joystickRight.x = 0;
        public void StopMovingOnDepth() => m_joystickRight.y = 0;

        public void SetMoveVertical(float percent) { m_joystickLeft.y = percent; }
        public void SetMoveHorizontal(float percent) { m_joystickRight.x = percent; }
        public void SetMoveDepth(float percent) { m_joystickRight.y = percent; }


        [ContextMenu("Request to Fire")]
        public void RequestToFire() {

            m_onFireRequested.Invoke();
        }

        public void GetTelloPosition(out Vector3 position, out Quaternion rotation)
        {
            position = m_dronePoint.position;
            rotation = m_dronePoint.rotation;
        }
        public void GetGunPosition(out Vector3 position, out Quaternion rotation)
        {
            position = m_fireDirection.position;
            rotation = m_fireDirection.rotation;
        }
    }

}