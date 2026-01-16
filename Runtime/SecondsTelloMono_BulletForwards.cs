using UnityEngine;

namespace Eloi.SecondsTello { 
    public class SecondsTelloMono_BulletForwards : MonoBehaviour
    {
        private void Reset()
        {
            m_whatToMove = this.transform;
            m_direction = this.transform;
        }
        public Transform m_whatToMove;
        public Transform m_direction;
        public float m_bulletSpeed;

        void Update()
        {
            m_whatToMove.Translate(m_direction.forward * m_bulletSpeed * Time.deltaTime);
        }
    }
}