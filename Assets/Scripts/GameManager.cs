using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int m_playerLives = 3;

    private void Awake()
    {
        int gameManagerCount = FindObjectsOfType<GameManager>().Length;
        if (gameManagerCount > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void ProcessPlayerDeath()
    {
        if (m_playerLives > 1)
            TakeLife();
        else
            ResetGameSession();

    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject); // Ensure fresh GameManager instance
    }

    private void TakeLife()
    {
        m_playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        
    }
}
