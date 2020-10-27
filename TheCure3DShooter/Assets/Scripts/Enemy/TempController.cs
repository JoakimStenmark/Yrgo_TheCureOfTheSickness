//Robert S
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempController : MonoBehaviour
{
    float tunnelRadius = 10;
    Vector3 center = Vector3.zero;
    Vector3 inputAxis;

    public float speed;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputAxis = Vector3.right * Input.GetAxis("Horizontal") + Vector3.up * Input.GetAxis("Vertical");
        inputAxis = inputAxis.normalized;

        target.position += inputAxis * speed * Time.deltaTime;
        if((target.position - center).sqrMagnitude > tunnelRadius * tunnelRadius)
        {
            target.position = center + (target.position - center).normalized * tunnelRadius;
        }
    }
}
