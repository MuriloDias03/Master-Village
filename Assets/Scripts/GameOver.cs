using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject gameOver;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        GameOverWindow();
    }

    public void GameOverWindow()
    {
        if (player.isDeath)
        {
            gameOver.SetActive(true);

            if (Input.GetKeyUp(KeyCode.S))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                Debug.Log("Saiu do jogo");
                Application.Quit();
            }
        }
    }
}