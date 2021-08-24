using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckTrigger : MonoBehaviour
{
    [HideInInspector] public bool isStuck;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7)
            return;

        isStuck = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != 7)
            return;

        isStuck = false;
    }
}
