using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour,IUpdateObserver
{
    public static UIManager Instance;
    [Header("Buttons")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button tryAgainButton;
    [Header("Panels")]
    [Space]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;
    [Header("Texts")]
    [Space]
    [SerializeField] private TextMeshProUGUI countdownerText;    
    [SerializeField] private TextMeshProUGUI scoreText;    
    [SerializeField] private TextMeshProUGUI bestScoreText;    
    [SerializeField] private TextMeshProUGUI inGameScoreText;    
    [SerializeField] private TextMeshProUGUI inGameHealthText;    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        UserInterfaceUpdateBroadcaster.RegisterObserver(this);
        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {        
        StartCoroutine(CountdownText());      
    }

    private IEnumerator CountdownText()
    {
        startPanel.SetActive(false);
        countdownerText.gameObject.SetActive(true);
        for (int i = 3; i >= 0; i--)
        {            
            if( i == 0)
            {
                countdownerText.text = "GO!";                
            }
            else
            {
                countdownerText.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }
        }
        yield return new WaitForSeconds(1f);
        countdownerText.gameObject.SetActive(false);
        GameManager.Instance.IsGameStart = true;
        yield return null;
    }   

    public void GameOverPanel(int score)
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        tryAgainButton.onClick.AddListener(TryAgainButton);
        scoreText.text = $"SCORE : {score}";
        bestScoreText.text = $"BEST SCORE : {PlayerPrefs.GetInt("Score")}";
    }

    public void TryAgainButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OnUiUpdated()
    {
        inGameHealthText.text = $"HEALTH : {GameManager.Instance.Health}";
        inGameScoreText.text = $"SCORE : {GameManager.Instance.Score}";
    }
}
