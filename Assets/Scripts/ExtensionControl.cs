using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtensionControl : MonoBehaviour
{
    [SerializeField] private float extensionRate;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gameOver)
            return;

        // Expand the collider
        bodyGFX.localScale += new Vector3(0, 0, extensionRate * 0.01f);

        Vector3 newCarSize = originalCarSize;
        newCarSize.z *= bodyGFX.localScale.z;
        carBounds.size = newCarSize;

        // Reposition axles
        Vector3 localBoundsCenter = transform.InverseTransformPoint(carBounds.bounds.center);

        Vector3 localFrontAxlePos = new Vector3(localBoundsCenter.x, localBoundsCenter.y - 0.75f, 0.5f * carBounds.size.z - 1);
        Vector3 localRearAxlePos = new Vector3(localBoundsCenter.x, localBoundsCenter.y - 0.75f, 1 - 0.5f * carBounds.size.z);

        frontAxle.localPosition = localFrontAxlePos;
        rearAxle.localPosition = localRearAxlePos;

        // Stuck check
        if (frontTrigger.isStuck && rearTrigger.isStuck)
            Countdown();
        else
            countdownCounter = 0;

        Debug.Log(countdownCounter);
    }

    private void Countdown()
    {
        countdownCounter += Time.deltaTime;

        if (countdownCounter > maxTimeStuck)
        {
            GameManager.Instance.gameOver = true;
            Debug.Log("GAME OVER!");
        }
    }
}
