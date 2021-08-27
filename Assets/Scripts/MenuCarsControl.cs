using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCarsControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float zoomOutSpeed;

    [SerializeField] private Image controlsImg;

    [SerializeField] private TextMeshProUGUI title;

    private float startTime;

    private Camera cam;

    private bool showingControls;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();

        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);

        cam.transform.Translate(0, 0, -zoomOutSpeed * 0.1f);

        if (Time.time - startTime < 3)
            return;

        if (!showingControls)
        {
            GameManager.Instance.ShowText("[ESPACIO] PARA CONTINUAR", true);

            if (Input.GetKeyDown(KeyCode.Space))
                ShowControls();
        }
        else
        {
            GameManager.Instance.ShowText("[ESPACIO] PARA JUGAR", true);

            if (Input.GetKeyDown(KeyCode.Space))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void ShowControls()
    {
        GameManager.Instance.ShowText("");
        startTime = Time.time;
        showingControls = true;

        controlsImg.enabled = true;
        title.enabled = false;
    }
}
