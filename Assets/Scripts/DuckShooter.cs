using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DuckShooter : MonoBehaviour
{
    public AudioClip shotSound;
    public AudioClip gameOverSound;
    public Text scoreText;
    public int maxAttempts = 3;
    private int attemptsLeft;
    public GameObject bulletOne;
    public GameObject bulletTwo;
    public GameObject bulletThree;
    public Image gameOverImage;
    public Text gameOverText;
    public Text gameOverTextTwo;
    public Text gameOverTextThree;
    public Text gameOverTextFour;
    public Text gameOverTextFive;

    private int score = 0;
    private DuckSpawner duckSpawner;
    private bool gameOver = false;
    private bool initialCooldownActive = true;
    private float initialCooldownDuration = 6f;
    private float currentCooldown;

    private void Start()
    {
        duckSpawner = FindObjectOfType<DuckSpawner>();
        attemptsLeft = maxAttempts;
        gameOverImage.enabled = false;
        gameOverText.enabled = false;
        gameOverTextTwo.enabled = false;
        gameOverTextThree.enabled = false;
        gameOverTextFour.enabled = false;
        gameOverTextFive.enabled = false;

        if (initialCooldownActive)
        {
            currentCooldown = initialCooldownDuration;
        } 
    }

    private void Update()
    {
        if (initialCooldownActive && currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (!gameOver && Input.GetMouseButtonDown(0) && currentCooldown <= 0)
        {
            PlayShotSound();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Duck"))
                {
                    ShotDuck(hit.collider.gameObject);
                }
                else if (hit.collider.CompareTag("FastDuck"))
                {
                    ShotDuck(hit.collider.gameObject);
                }
            }
            else
            {
                attemptsLeft--;
                if (attemptsLeft == 2)
                {
                    Destroy(bulletThree);
                }

                else if (attemptsLeft == 1)
                {
                    Destroy(bulletTwo);
                }

                else if (attemptsLeft <= 0)
                {
                    Destroy(bulletOne);
                    ShowGameOverImage();
                }  
            }
            currentCooldown = 0;
            initialCooldownActive = false;
        }
        else if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }

    void PlayShotSound()
    {
        float volume = 0.5f;
        AudioSource.PlayClipAtPoint(shotSound, Camera.main.transform.position, volume);
    }

    void ShotDuck(GameObject duck)
    {
        Destroy(duck);
        if (duck.CompareTag("Duck"))
        {
            score += 100;
        }
        else if (duck.CompareTag("FastDuck"))
        {
            score += 200;
        }
        UpdateScoreText();
        duckSpawner.DuckShot();
        /**if (score == 1000)
        {
            GoToNextLevel();
        }**/
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "" + score.ToString("000000");
        }
    }

    void ShowGameOverImage()
    {
        gameOver = true;
        gameOverImage.enabled = true;
        gameOverText.enabled = true;
        gameOverTextTwo.enabled = true;
        gameOverTextThree.enabled = true;
        gameOverTextFour.enabled = true;
        gameOverTextFive.enabled = true;
        DuckController[] remainingDucks = FindObjectsOfType<DuckController>();
        foreach (DuckController duck in remainingDucks)
        {
            Destroy(duck.gameObject);
        }

        FastDuckController[] remainingFastDucks = FindObjectsOfType<FastDuckController>();
        foreach (FastDuckController fastDuck in remainingFastDucks)
        {
            Destroy(fastDuck.gameObject);
        }

        duckSpawner.StopSpawning();
        PlayGameOverSound();
    }

    void PlayGameOverSound()
    {
        float volume = 0.5f;
        AudioSource.PlayClipAtPoint(gameOverSound, Camera.main.transform.position, volume);
    }

    /**void GoToNextLevel()
    {
        Debug.Log("Congratulations! Going to the next level!");
    }**/
}


