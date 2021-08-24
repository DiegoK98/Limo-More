using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaterialController : MonoBehaviour
{

    private WheelCollider wheelCollider;
    private GroundMaterial type;

    private void Start()
    {
        wheelCollider = this.GetComponent<WheelCollider>();
    }

    private void FixedUpdate()
    {
        wheelCollider.GetGroundHit(out WheelHit hit);

        if (hit.collider is null)
            return;

        if (!hit.collider.TryGetComponent<GroundMaterial>(out type))
            return;

        SetStiffness(wheelCollider, type.stiffness);
    }

    private static void SetStiffness(WheelCollider wheelCollider, float stiffness)
    {
        WheelFrictionCurve tempFriction = wheelCollider.forwardFriction;
        tempFriction.stiffness = stiffness;
        wheelCollider.forwardFriction = tempFriction;

        tempFriction = wheelCollider.sidewaysFriction;
        tempFriction.stiffness = stiffness;
        wheelCollider.sidewaysFriction = tempFriction;
    }
}
