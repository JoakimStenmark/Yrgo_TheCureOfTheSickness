using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlodActive : MonoBehaviour
{
    float z;
    GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        z = transform.position.z;
        pl = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(z < pl.transform.position.z + 50)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
