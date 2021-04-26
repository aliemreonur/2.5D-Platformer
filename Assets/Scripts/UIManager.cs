using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    [SerializeField] Text _scoreText, _livesText, _endScoreText, _gameOverText;
    [SerializeField] GameObject gameOverScene;
    public bool isGameOver;

    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.LogError("UI Manager is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        gameOverScene.gameObject.SetActive(false);
    }

    public void UpdateScore(int score)
    {
        _scoreText.text = score.ToString();
    }

    public void UpdateLives(int lives)
    {
        _livesText.text = "Lives : " + lives.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
        isGameOver = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void GameOver(int coins)
    {
        gameOverScene.SetActive(true);
        _endScoreText.text = "You have Collected : " + coins + " Coins";
        isGameOver = true;
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        while(isGameOver)
        {
            _gameOverText.gameObject.SetActive(true);
            _endScoreText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            _gameOverText.gameObject.SetActive(false);
            _endScoreText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

        }
    }
}
