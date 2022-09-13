using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSwiping : MonoBehaviour
{
    [SerializeField] private float sweepSpeed;

    private float patternTileSize = 14.11f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, sweepSpeed * 0.1f, Space.World);

        if (transform.position.z >= patternTileSize)
            transform.position = Vector3.zero;
    }
}
