using UnityEngine;
using UnityEngine.Events;
namespace Eloi.SecondsTello
{
    public class SecondsTelloMono_DisplaySecondsTimerAsString : MonoBehaviour {

        public UnityEvent<string> m_onSecondsAsTextUpdated;
        public string m_defaultFormat = "{0:00}";
        public void SetTimeToSeconds(float seconds) {
            m_onSecondsAsTextUpdated.Invoke( string.Format(m_defaultFormat, seconds) );
        }
    }
}