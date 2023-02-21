using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip m_pickupSFX;
    [SerializeField] int m_points = 100;
    bool m_hasBeenPickedup = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !m_hasBeenPickedup)
        {
            m_hasBeenPickedup = true;
            AudioSource.PlayClipAtPoint(m_pickupSFX, Camera.main.transform.position);
            FindObjectOfType<GameManager>().ProcessCoinPickup(m_points);
            Destroy(gameObject);
        }
    }
}
