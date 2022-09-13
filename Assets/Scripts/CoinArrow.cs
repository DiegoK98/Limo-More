using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinArrow : MonoBehaviour
{
    [SerializeField] private float rotationRate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, -rotationRate, 0);
    }
}
