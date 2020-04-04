using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    private int score;
    public Text scoreText;
    public Text winText;
    public Text restartText;
    public Text gameOverText;

    private bool win;
    private bool gameOver;
    private bool restart;


    void Start()
    {
        win = false;
        winText.text = "";
        restart = false;
        restartText.text = "";
        gameOver = false;
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
    }

    void Update()
    {
        if (restart)
        {
            if(Input.GetKeyDown(KeyCode.H))
            {
                SceneManager.LoadScene("Scene");
            }
        }
        
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        
    }


    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while(true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'H' to Restart";
                restart = true;
                break;
            }

            if (win)
            {
                restartText.text = "Press 'H' to Restart";
                restart = true;
                break;
            }
        }
    }

    public void AddScore (int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
        if (score >= 100)
        {

            winText.text = "You Win! Game Created By Hiram Sun!";
            win = true;
        }
    }

    public void GameOver()
    {
        if (win != true)
        {
            gameOverText.text = "Game Over!";
            gameOver = true;

        }
    }
}
