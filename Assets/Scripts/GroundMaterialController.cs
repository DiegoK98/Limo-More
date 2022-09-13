using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMaterialController : MonoBehaviour
{

    private WheelCollider wheelCollider;
    private GroundMaterial type;

    private float forwStiffness;
    private float sideStiffness;

    private void Start()
    {
        wheelCollider = this.GetComponent<WheelCollider>();

        forwStiffness = wheelCollider.forwardFriction.stiffness;
        sideStiffness = wheelCollider.sidewaysFriction.stiffness;
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

    private void SetStiffness(WheelCollider wheelCollider, float stiffness)
    {
        WheelFrictionCurve tempFriction = wheelCollider.forwardFriction;
        tempFriction.stiffness = stiffness * forwStiffness;
        wheelCollider.forwardFriction = tempFriction;

        tempFriction = wheelCollider.sidewaysFriction;
        tempFriction.stiffness = stiffness * sideStiffness;
        wheelCollider.sidewaysFriction = tempFriction;
    }
}
