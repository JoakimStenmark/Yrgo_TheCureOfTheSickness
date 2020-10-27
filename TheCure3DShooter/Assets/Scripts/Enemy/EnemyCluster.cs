using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyCluster : MonoBehaviour
{
    [Header("EnemyType")]
    public GameObject EnemyPreFab;

    [Header("Settings")]
    public float spawnAtDistance = 10;

    public int numberOfEnemys;
    public Vector3 spawnBoxPositionOffset = Vector3.zero;
    public float spawnBoxWidh = 5;
    public float spawnSpaceing = 1;

    [Header("TunnelMotion")]
    public Vector3 addTunnelMotion = Vector3.forward * 0.5f;

    private GameObject[] spawedEnemys;
    private bool birth = false;
    private GameObject player;
    // Start is called before the first sframe update
    void Start()
    {
        spawnAtDistance *= spawnAtDistance;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if (birth)
        {
            if ((transform.position - player.transform.position).sqrMagnitude > spawnAtDistance)
            {
                DestroyAll();
            }
        }
        else
        {
            if ((transform.position - player.transform.position).sqrMagnitude < spawnAtDistance)
            {
                SpawEnemysInBox();
                birth = true;
            }
        }
    }
    public void SpawEnemysInBox()
    {
        spawedEnemys = new GameObject[numberOfEnemys];

        float x = 0;
        float y = 0;
        for (int i = 0; i < numberOfEnemys; i++)
        {
            if (x > spawnBoxWidh)
            {
                y -= spawnSpaceing;
                x = 0;
            }

            spawedEnemys[i] = Instantiate(EnemyPreFab, transform.position + spawnBoxPositionOffset + new Vector3(x, y, 0), Quaternion.identity);

            //Todo: fix this nice
            ForceMoveEnemy fme = spawedEnemys[i].GetComponent<ForceMoveEnemy>();
            fme.addTunnelMovement = addTunnelMotion;
            fme.destroyAtDistance = spawnAtDistance + spawnAtDistance;

            x += spawnSpaceing;
        }

        Debug.DrawLine(transform.position + spawnBoxPositionOffset, transform.position + spawnBoxPositionOffset + transform.right * spawnBoxWidh, Color.yellow, 2);
        Debug.DrawLine(transform.position + spawnBoxPositionOffset, transform.position + spawnBoxPositionOffset + transform.up * y, Color.yellow, 2);
        
        Debug.DrawLine(transform.position + spawnBoxPositionOffset + transform.right * spawnBoxWidh, transform.position + spawnBoxPositionOffset+ transform.right * spawnBoxWidh + transform.up * y, Color.yellow, 2);
    }

    public void DestroyAll()
    {
        /*
        for (int i = 0; i < spawedEnemys.Length; i++)
        {
            Destroy(spawedEnemys[i]);
        }
        */
        Destroy(gameObject);
    }

   
}
