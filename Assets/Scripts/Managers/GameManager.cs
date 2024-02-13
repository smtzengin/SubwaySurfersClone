using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool isGameStart;
    public bool IsGameStart { get { return isGameStart; } set { isGameStart = value; } }

    private bool isGameOver;
    public bool IsGameOver { get { return isGameOver; } set { isGameOver = value; } }

    [SerializeField] private int score;
    public int Score { get { return score; } }

    [SerializeField] private int health;

    public int Health { get { return health; } }


    private void Awake()
    {
        isGameStart = false;
        isGameOver = false;
        Debug.Log($"Best Score: {PlayerPrefs.GetInt("Score")}");
        if(Instance == null)
        {
            Instance = this;            
        }       
    }

    public void TakeDamage(int damage)
    {
        if(isGameStart && !isGameOver )
        {
            health -= damage;
            Debug.Log($"Current Health : {health}");
            UIManager.Instance.OnUiUpdated();
            if (health <= 0)
            {
                health = 0;
                GameOver();
            }
        }       
    }

    public void IncreaseGold()
    {
        score++;
        Debug.Log($"Score : {score}");
        UIManager.Instance.OnUiUpdated();
    }

    public void GameOver()
    {
        isGameOver = true;
        if (!PlayerPrefs.HasKey("Score"))
        {
            PlayerPrefs.SetInt("Score", score);
        }
        else
        {
            if(score >= PlayerPrefs.GetInt("Score"))
            {
                PlayerPrefs.SetInt("Score", score);
            }
        }
        UIManager.Instance.GameOverPanel(score);
    }

}
