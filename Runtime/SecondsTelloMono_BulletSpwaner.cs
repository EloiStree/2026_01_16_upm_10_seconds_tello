using UnityEngine;

namespace Eloi.SecondsTello {
    public class SecondsTelloMono_BulletSpwaner : MonoBehaviour
    {
        public GameObject m_bulletPrefab;
        public Transform m_spawnPoint;
        public float m_bulletLifeTime = 5f;
        public float m_cooldownBetweenShots = 0.5f;
        private float m_timeSinceLastShot = 0f;
        public int m_ammoCount = 10;

        [ContextMenu("Spwan Bullet")]
        public void SpwanBullet()
        {
            if (m_bulletPrefab == null || m_spawnPoint == null)
                return;
            if (m_timeSinceLastShot < m_cooldownBetweenShots)
                return;
            if (m_ammoCount <= 0)
                return;

            m_timeSinceLastShot = 0f;
            GameObject created = Instantiate(m_bulletPrefab, m_spawnPoint.position, m_spawnPoint.rotation);
            Destroy(created, m_bulletLifeTime);
            m_ammoCount--;
        }
        public void Update()
        {
            m_timeSinceLastShot += Time.deltaTime;
        }
    }
}