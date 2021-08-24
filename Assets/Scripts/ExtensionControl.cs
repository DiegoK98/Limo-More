using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionControl : MonoBehaviour
{
    [SerializeField] private float extensionRate = 0.1f;
    [SerializeField] private float patternGap = 0.77f;
    [SerializeField] private GameObject patternPrefab;
    [SerializeField] private Transform patternOrigin;

    private Vector3 patternSpawnPoint;

    [Header("Body")]

    [SerializeField] private Transform bodyGFX;

    [Header("Axles")]

    [SerializeField] private Transform frontAxle;
    [SerializeField] private Transform rearAxle;

    [Header("Stuck Control")]

    [SerializeField] private StuckTrigger frontTrigger;
    [SerializeField] private StuckTrigger rearTrigger;

    private BoxCollider carBounds;

    private Vector3 originalCarSize;
    private float savedCarLength;

    private float countdownCounter;

    /// <summary>
    /// Time the car will have to be stuck before triggering Game Over
    /// </summary>
    [SerializeField] private float maxTimeStuck;

    // Start is called before the first frame update
    void Start()
    {
        carBounds = GetComponent<BoxCollider>();
        originalCarSize = carBounds.size;

        patternSpawnPoint = patternOrigin.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
            return;

        // Expand the car
        bodyGFX.localScale += new Vector3(0, 0, extensionRate * 0.01f);

        Vector3 newCarSize = originalCarSize;
        newCarSize.z *= bodyGFX.localScale.z;
        carBounds.size = newCarSize;

        // Repeating pattern every 0.77
        if(carBounds.size.z - savedCarLength >= patternGap)
        {
            savedCarLength = carBounds.size.z;
            RepeatPattern();
        }

        // Reposition axles
        Vector3 localFrontAxlePos = new Vector3(0, 0, 0.5f * carBounds.size.z - 0.84f);
        Vector3 localRearAxlePos = new Vector3(0, 0, 0.95f - (0.5f * carBounds.size.z));

        frontAxle.localPosition = localFrontAxlePos;
        rearAxle.localPosition = localRearAxlePos;

        // Stuck check
        if (frontTrigger.isStuck && rearTrigger.isStuck)
            Countdown();
        else
            countdownCounter = 0;
    }

    private void RepeatPattern()
    {
        Vector3 newSpawnPos = patternSpawnPoint;
        newSpawnPos.z += patternGap;
        patternSpawnPoint = newSpawnPos;

        GameObject go = Instantiate(patternPrefab, patternOrigin.parent, false);
        go.transform.localPosition = patternSpawnPoint;
    }

    private void Countdown()
    {
        countdownCounter += Time.deltaTime;

        if (countdownCounter > maxTimeStuck)
        {
            GameManager.Instance.gameOver = true;
        }
    }
}
