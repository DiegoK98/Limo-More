using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxlesRepositioning : MonoBehaviour
{
    [SerializeField] private Transform frontAxle;
    [SerializeField] private Transform rearAxle;

    [SerializeField] private Transform extendablePart;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("g");
    }

    // Update is called once per frame
    void Update()
    {
        //rearAxle.transform.localPosition = extendablePart.;
    }
}
