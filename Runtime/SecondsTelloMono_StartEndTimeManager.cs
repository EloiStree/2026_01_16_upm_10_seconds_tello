using System;
using UnityEngine;
using UnityEngine.Events;
namespace Eloi.SecondsTello
{
    public class SecondsTelloMono_StartEndTimeManager : MonoBehaviour
    {

        [Header("Unity Event")]
        public UnityEvent m_onPlayerLeaveStartPoint;
        public UnityEvent m_onPlayerArriveEndPoint;
        public UnityEvent<float> m_onTimeInSecondsChanged;
        public UnityEvent m_on10SecondsReached;

        [Header("Configuration")]
        public Transform m_startPoint;
        public float m_radiusStartPoint = 0.2f;

        public Transform m_endPoint;
        public float m_radiusEndPoint = 0.5f;

        public Transform m_playerPosition;


        [Header("Debug value")]
        public long m_timerInTicks;
        public float m_timerInSeconds;
        public long m_timerAtStartTick;
        public long m_timerNowTick;
        public long m_timerAtEndTick;
        public bool m_isPlayerInStartZone;
        public bool m_isPlayerInBetweenZone;
        public bool m_isPlayerInEndZone;

        public float m_distanceFromStartZone;
        public float m_distanceFromEndZone;

        private float m_previousTimeInSeconds;


        public long GetTickNowUtc() { 
            return DateTime.UtcNow.Ticks;
        }

        public float GetTimeInSeconds()
        {
            return (float)(m_timerNowTick - m_timerAtStartTick) / TimeSpan.TicksPerSecond;
        }

        public void Update()
        {
            m_previousTimeInSeconds = m_timerInSeconds;

            m_timerNowTick = GetTickNowUtc();
            
            m_distanceFromStartZone = Vector3.Distance(m_startPoint.position, m_playerPosition.position);
            m_distanceFromEndZone = Vector3.Distance(m_endPoint.position, m_playerPosition.position);
         
            bool wasInStartZone = m_isPlayerInStartZone;
            bool wasInEndZone = m_isPlayerInEndZone;
            m_isPlayerInStartZone = m_distanceFromStartZone <= m_radiusStartPoint;
            m_isPlayerInEndZone = m_distanceFromEndZone <= m_radiusEndPoint;
            m_isPlayerInBetweenZone = !m_isPlayerInStartZone && !m_isPlayerInEndZone;

            if (wasInStartZone && !m_isPlayerInStartZone)
            {
                m_timerAtStartTick = GetTickNowUtc();
                m_timerNowTick = GetTickNowUtc();
                m_timerAtEndTick = 0;   
                m_timerInTicks = m_timerNowTick- m_timerAtStartTick;
                m_timerInSeconds =GetTimeInSeconds();
                m_onPlayerLeaveStartPoint.Invoke();
                m_onTimeInSecondsChanged.Invoke(m_timerInSeconds);
            }

            if (!wasInEndZone && m_isPlayerInEndZone)
            {
                m_timerAtEndTick = GetTickNowUtc();
                m_timerInTicks = m_timerAtEndTick - m_timerAtStartTick;
                m_timerInSeconds = GetTimeInSeconds();
                m_onPlayerArriveEndPoint.Invoke();
                m_onTimeInSecondsChanged.Invoke(m_timerInSeconds);
            }

            if (m_isPlayerInBetweenZone) {
                m_timerNowTick = GetTickNowUtc();
                m_timerAtEndTick = 0;

                m_timerInTicks = m_timerNowTick - m_timerAtStartTick;
                m_timerInSeconds = GetTimeInSeconds();

                m_onTimeInSecondsChanged.Invoke(m_timerInSeconds);
            }
            if (m_previousTimeInSeconds < 10f && m_timerInSeconds >= 10f)
            {
                m_on10SecondsReached.Invoke();
            }
        }
    }
}