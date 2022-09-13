using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WindowTint : MonoBehaviour
{
    private List<MeshRenderer> windowParts;

    [SerializeField] private List<Material> colors;

    [SerializeField] private float colorChangePeriod;

    private float counter;

    private int colorLoopCounter;

    // Start is called before the first frame update
    void Start()
    {
        windowParts = GetComponentsInChildren<MeshRenderer>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter >= colorChangePeriod)
        {
            counter = 0;

            ChangeColor(colors[colorLoopCounter]);

            if (colorLoopCounter >= colors.Count - 1)
                colorLoopCounter = 0;
            else
                colorLoopCounter++;
        }
    }

    private void ChangeColor(Material color)
    {
        foreach (MeshRenderer part in windowParts)
        {
            part.material = color;
        }
    }
}
