using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSoundControl : MonoBehaviour
{
    private AudioSource engineSound;
    private CarController carControl;

    // Start is called before the first frame update
    void Start()
    {
        engineSound = GetComponent<AudioSource>();
        carControl = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        engineSound.pitch = carControl.currentAcceleration;
    }
}
