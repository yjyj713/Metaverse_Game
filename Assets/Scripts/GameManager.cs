using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager gameManager;

    public static GameManager Instance {  get { return gameManager; } }

    private int currentScore = 0;
    private void Awake()
    {
        gameManager = this;
    }

    void Start()
    {
        // 해상도 설정: 1920x1080 해상도로 설정, 창 모드로 실행
        Screen.SetResolution(1920, 1080, true);
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddScore(int score)
    {
        currentScore += score;
        Debug.Log("Score: " + currentScore);
        Debug.Log("Current Score: " + currentScore);  // 갱신된 점수 출력
    }
}
