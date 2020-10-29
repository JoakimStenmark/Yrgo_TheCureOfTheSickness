//Robert S
//Ful fix
using UnityEngine;

public class EnemyRandomRotation : MonoBehaviour
{
    public float speed = 100;
    private Vector3 rotAxis;
    // Start is called before the first frame update
    void Start()
    {
        rotAxis = Random.insideUnitSphere;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotAxis, speed * Time.deltaTime);
    }
}
