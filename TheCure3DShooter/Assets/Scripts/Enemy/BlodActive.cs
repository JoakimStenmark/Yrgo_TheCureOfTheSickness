using UnityEngine;

public class BlodActive : MonoBehaviour
{
    float force = 10;
    GameObject pl;
    // Start is called before the first frame update
    void Start()
    {
        pl = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(pl.transform.position.z < pl.transform.position.z + 50)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
        }
        

        if(transform.position.z - pl.transform.position.z < -40)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.DrawRay(transform.position, (transform.position - collision.GetContact(0).point) * force, Color.red, 10);
            GetComponent<Rigidbody>().AddForce((transform.position - collision.GetContact(0).point) * force, ForceMode.Impulse);
        }
    }
}
