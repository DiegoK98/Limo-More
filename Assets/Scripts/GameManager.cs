using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public bool gameOver;

    public static GameManager Instance;

    private int currentPoints;

    [SerializeField] private List<CoinBehaviour> coins;

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
            Time.timeScale = 0;

        if (currentPoints >= coins.Count)
        {
            gameOver = true;
            Debug.Log("VICTORY!");
        }
    }

    public void AddPoint()
    {
        currentPoints++;
    }

    public void ResetPoints(int pointsToSet = 0)
    {
        currentPoints = pointsToSet;
    }
}
