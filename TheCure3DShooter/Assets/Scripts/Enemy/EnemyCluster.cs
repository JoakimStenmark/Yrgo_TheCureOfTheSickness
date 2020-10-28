//Robert S
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

    [Header("SpawnPath")]
    public float spawnRadius = 10;
    public int spawnSegments = 24;
    public Vector2[] patrolPath;


    public Vector3 spawnBoxPositionOffset = Vector3.zero;
    public float spawnBoxWidh = 5;
    public float spawnSpaceing = 1;


    [Header("TunnelMotion")]
    public Vector3 addTunnelMotion = Vector3.forward * 0.5f;
    private GameObject player;

    public GameObject railAnchor;

    // Start is called before the first sframe update
    void Start()
    {
        //Get from levelmanger
        railAnchor = GameObject.FindGameObjectWithTag(Tags.enemyRailAnchor);
        spawnAtDistance *= spawnAtDistance;
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }
    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < spawnAtDistance)
        {
            SpawEnemysInBox();
            DestroySpawer();
        }
    }
    public void SpawEnemysInBox()
    {
        float x = 0;
        float y = 0;
        for (int i = 0; i < numberOfEnemys; i++)
        {
            GameObject spawedEnemy = Instantiate(EnemyPreFab, transform.position, Quaternion.identity);

            //TODO: fix this nice
            //Spawn is of coded type
            EnemyMovement enemyMoveScript = spawedEnemy.GetComponent<EnemyMovement>();
            if (enemyMoveScript != null)
            {
                enemyMoveScript.SpawnInit(i, CircularPath(), railAnchor);
            }


            ForceMoveEnemy fme = spawedEnemy.GetComponent<ForceMoveEnemy>();
            //Spaw is of Physics type
            if (fme != null)
            {
                if (x > spawnBoxWidh)
                {
                    y -= spawnSpaceing;
                    x = 0;
                }

                spawedEnemy.transform.position = transform.position + spawnBoxPositionOffset + new Vector3(x, y, 0);
                fme.addTunnelMovement = addTunnelMotion;
                fme.destroyAtDistance = spawnAtDistance + spawnAtDistance;
                x += spawnSpaceing;

                Debug.DrawLine(transform.position + spawnBoxPositionOffset, transform.position + spawnBoxPositionOffset + transform.right * spawnBoxWidh, Color.yellow, 2);
                Debug.DrawLine(transform.position + spawnBoxPositionOffset, transform.position + spawnBoxPositionOffset + transform.up * y, Color.yellow, 2);
                Debug.DrawLine(transform.position + spawnBoxPositionOffset + transform.right * spawnBoxWidh, transform.position + spawnBoxPositionOffset+ transform.right * spawnBoxWidh + transform.up * y, Color.yellow, 2);
            }

        }
    }
    Vector2[] CircularPath()
    {
        Vector3 point = Vector3.up * spawnRadius;
        patrolPath = new Vector2[spawnSegments];

        float angle = 360 / spawnSegments;

        for (int i = 0; i < spawnSegments; i++)
        {
            Quaternion rotstep = Quaternion.AngleAxis(angle * i, Vector3.forward);
            patrolPath[i] = transform.position + rotstep * point;
            Debug.DrawRay(patrolPath[i], Vector3.one * 0.25f, Color.yellow, 10);
        }

        return patrolPath;
    }
    public void DestroySpawer()
    {
        Destroy(gameObject);
    }

   
}
