using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool gameOver;

    private bool timeScaledecreasing;

    public static GameManager Instance;

    private int currentPoints;

    [SerializeField] private List<CoinBehaviour> coins;

    private float noCoinsTimeCounter;

    [SerializeField] private float noCoinsMaxTime = 40;

    private float resetTimeCounter;

    [SerializeField] private float resetMaxTime = 1;

    private bool alreadyReloaded;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
            timeScaledecreasing = true;

        if (timeScaledecreasing)
            Time.timeScale -= Time.deltaTime;

        if (currentPoints >= coins.Count)
        {
            gameOver = true;
            Debug.Log("VICTORY!");
        }

        noCoinsTimeCounter += Time.deltaTime;

        if(noCoinsTimeCounter > noCoinsMaxTime)
            Debug.Log("Hold [R] to restart");

        if (Input.GetKey(KeyCode.R))
            resetTimeCounter += Time.deltaTime;
        else
            resetTimeCounter = 0;

        if (resetTimeCounter > resetMaxTime)
            ReloadScene();
    }

    private void ReloadScene()
    {
        if (alreadyReloaded)
            return;

        alreadyReloaded = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddPoint()
    {
        Debug.Log(noCoinsTimeCounter);
        noCoinsTimeCounter = 0;
        currentPoints++;
    }

    public void ResetPoints(int pointsToSet = 0)
    {
        currentPoints = pointsToSet;
    }
}
