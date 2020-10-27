using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCluster : MonoBehaviour
{
    public int numberOfEnemys;
    public Vector3 spawnBoxPositionOffset = Vector3.zero;
    public float spawnBoxWidh = 5;
    public float spawnSpaceing = 1;
    public GameObject EnemyPreFab;
    
    private GameObject[] spawedEnemys;
    // Start is called before the first frame update
    void Start()
    {
        SpawEnemysInBox();
    }

    public void SpawEnemysInBox()
    {
        spawedEnemys = new GameObject[numberOfEnemys];

        float x = 0;
        float y = 0;
        for (int i = 0; i < numberOfEnemys; i++)
        {
            if(x > spawnBoxWidh)
            {
                y -= spawnSpaceing;
                x = 0;
            }

            spawedEnemys[i] = Instantiate(EnemyPreFab, transform.position + spawnBoxPositionOffset + new Vector3(x,y,0), Quaternion.identity);
            x += spawnSpaceing;
        }
    }

    public void DestroyAll()
    {
        for (int i = 0; i < spawedEnemys.Length; i++)
        {
            Destroy(spawedEnemys[i]);
        }

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            DestroyAll();
        }
    }
}
