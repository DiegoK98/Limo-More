using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicPlayer;

    [SerializeField] private Text textAsset;

    [SerializeField] private Slider holdRSlider;

    [SerializeField] private Text holdRText;

    [SerializeField] private Text gameOverText;

    [HideInInspector] public bool gameOver;

    private bool timeScaledecreasing;

    public static GameManager Instance;

    private int currentPoints;

    [SerializeField] private bool playableLevel = true;

    [Header("Coins")]

    [SerializeField] private List<CoinBehaviour> coins;

    private float noCoinsTimeCounter;

    [SerializeField] private float noCoinsMaxTime = 40;

    private float resetTimeCounter;

    [SerializeField] private float resetMaxTime = 1;

    private bool alreadyReloaded;

    private float startTime;

    private bool won;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPoints = 0;

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        musicPlayer.pitch = Mathf.Sqrt(Time.timeScale);

        if (timeScaledecreasing)
            Time.timeScale -= Time.deltaTime;

        if (!playableLevel)
            return;

        if (Time.timeScale <= 0.1f)
        {
            musicPlayer.Stop();

            if (won)
            {
                ShowText("[ESPACIO] PARA CONTINUAR", true);

                if (Input.GetKeyDown(KeyCode.Space))
                    NextLevel();
            }
            else
            {
                ShowText("[ESPACIO] PARA REINICIAR", true);

                if (Input.GetKeyDown(KeyCode.Space))
                    ReloadLevel();
            }
        }

        if (gameOver)
        {
            timeScaledecreasing = true;

            return;
        }

        if (currentPoints >= coins.Count)
        {
            GameOver(true);
        }

        noCoinsTimeCounter += Time.deltaTime;

        if (noCoinsTimeCounter > noCoinsMaxTime)
            ShowText("MANTÉN [R] PARA REINICIAR");
        else
            ShowText("");

        if (Input.GetKey(KeyCode.R) && Time.time - startTime > 1)
        {
            resetTimeCounter += Time.deltaTime;

            holdRSlider.gameObject.SetActive(true);
            holdRText.enabled = true;

            holdRSlider.value = resetTimeCounter / resetMaxTime;
        }
        else
        {
            resetTimeCounter = 0;

            holdRSlider.gameObject.SetActive(false);
            holdRText.enabled = false;
        }

        if (resetTimeCounter > resetMaxTime)
            ReloadLevel();
    }

    public void ReloadLevel()
    {
        if (alreadyReloaded)
            return;

        Time.timeScale = 1;

        alreadyReloaded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddPoint()
    {
        noCoinsTimeCounter = 0;
        currentPoints++;
    }

    public void ResetPoints(int pointsToSet = 0)
    {
        currentPoints = pointsToSet;
    }

    public void ShowText(string text, bool animated = false)
    {
        textAsset.text = text;

        textAsset.GetComponentInParent<Animator>().enabled = animated;
    }

    public void GameOver(bool win = false)
    {
        gameOver = true;

        won = win;

        ShowText("");

        if (win)
        {
            gameOverText.text = "NICE!";
            gameOverText.color = new Color(0, 140, 0);
        }

        if (!win)
        {
            gameOverText.text = "STUCK!";
            gameOverText.color = new Color(229, 19, 0);
        }
    }

    public void NextLevel()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene >= SceneManager.sceneCountInBuildSettings)
            return;

        Time.timeScale = 1;

        SceneManager.LoadScene(nextScene);
    }
}
