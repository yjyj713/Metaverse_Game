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
        // �ػ� ����: 1920x1080 �ػ󵵷� ����, â ���� ����
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
        Debug.Log("Current Score: " + currentScore);  // ���ŵ� ���� ���
    }
}
