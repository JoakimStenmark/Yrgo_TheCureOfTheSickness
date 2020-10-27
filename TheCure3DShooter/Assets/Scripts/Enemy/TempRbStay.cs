using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempRbStay : MonoBehaviour
{
    public Vector3 targetPos;
    Rigidbody rb;

    public float positionDampen = 0.8f;
    public float rotationDampenFactor = 0.8f;
    // Start is called before the first frame update
    void Start()
    {
        targetPos= transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stabilezePosition();
        stabilizeRotation(transform.forward, Vector3.forward);
        stabilizeRotation(transform.up, Vector3.up);
    }

    void stabilezePosition()
    {
        Vector3 forceDirToTarget =targetPos- transform.position;
        float forceToTarget = forceDirToTarget.sqrMagnitude;
        forceToTarget *= positionDampen;
        rb.AddForce(forceDirToTarget * forceToTarget, ForceMode.VelocityChange);
    }

    void stabilizeRotation(Vector3 fromDir, Vector3 toDir)
    {
        //https://stackoverflow.com/questions/58419942/stabilize-hovercraft-rigidbody-upright-using-torque
        Quaternion deltaQuat = Quaternion.FromToRotation(fromDir, toDir);

        Vector3 axis;
        float angle;
        deltaQuat.ToAngleAxis(out angle, out axis);

        rb.AddTorque(-rb.angularVelocity * rotationDampenFactor, ForceMode.Acceleration);

        float adjustFactor = 0.5f; // this value requires tuning
        rb.AddTorque(axis.normalized * angle * adjustFactor, ForceMode.Acceleration);
    }
}
