using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuCarsControl : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float zoomOutSpeed;

    private float startTime;

    private Camera cam;

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

        GameManager.Instance.ShowText("[ESPACIO] PARA JUGAR", true);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
