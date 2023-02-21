using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int m_playerLives = 3;
    int m_score = 0;

    [SerializeField] TextMeshProUGUI m_livesText;
    [SerializeField] TextMeshProUGUI m_scoreText;

    private void Awake()
    {
        int gameManagerCount = FindObjectsOfType<GameManager>().Length;
        if (gameManagerCount > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        UpdateLivesText();
        UpdateScoreText();
    }

    public void ProcessPlayerDeath()
    {
        if (m_playerLives > 1)
            TakeLife();
        else
            ResetGameSession();

    }

    public void ProcessCoinPickup(int pointsToAdd)
    {
        m_score += pointsToAdd;
        UpdateScoreText();
    }

    private void ResetGameSession()
    {
        m_score = 0;
        SceneManager.LoadScene(0);
        FindObjectOfType<ScenePersist>().EnsureThatNewScenePersistIsCreated();
        Destroy(gameObject); // Ensure fresh GameManager instance
    }

    void UpdateLivesText()
    {
        m_livesText.text = m_playerLives.ToString();
    }

    void UpdateScoreText()
    {
        m_scoreText.text = m_score.ToString();
    }

    private void TakeLife()
    {
        m_playerLives--;
        UpdateLivesText();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
        
    }
}
