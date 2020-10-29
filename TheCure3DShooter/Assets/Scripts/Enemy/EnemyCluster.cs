//Robert S
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyCluster : MonoBehaviour
{
    public int level;

    [Header("EnemyType")]
    public bool moving = false;
    private GameObject SpawnType;
    public GameObject EnemyPreFab;
    public GameObject PillarPreFab;
    public GameObject BloodCellPreFab;


    [Header("Settings")]
    public bool randomized = true;
    public float spawnAtDistance = 10;
    public int numberOfEnemys = 1;
    public Vector3 offsetFromSorce = Vector3.zero;

    [Header("SpawnCluster")]
    public float maxRadius = 10;
    public float spawnRadius = 10;
    public int spawnSegments = 24;
    public Vector2[] patrolPath;
    public Quaternion rotation = Quaternion.identity;

    [Header("TunnelMotion")]
    public Vector3 addTunnelMotion = Vector3.forward * 0.5f;
    private GameObject player;

    public GameObject railAnchor;


    public void RandomizeSpawnAtLevel(int level)
    {
        int r = (int) Random.Range(-0.9f, 2);
        //r = 1;
        if(r == 0)
        {
            SpawnType = EnemyPreFab;
            
            spawnSegments = Random.Range(2, 4);
            numberOfEnemys = Random.Range(1, spawnSegments);
            if(spawnSegments > 3)
            {
                spawnSegments = 24;
            }

            spawnRadius = Random.Range(numberOfEnemys, maxRadius);
            if (spawnRadius > maxRadius)
                spawnRadius = maxRadius;

            rotation = Random.rotation;
        }
        else if(r == 1)
        {
            SpawnType = PillarPreFab;

            numberOfEnemys = Random.Range(1, Random.Range(1, 3));
            rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward);
            offsetFromSorce = rotation * Vector3.right * Random.Range(2,maxRadius * 2);
        }
        else
        {
            SpawnType = BloodCellPreFab;
        }
    }

    // Start is called before the first sframe update
    void Start()
    {
        RandomizeSpawnAtLevel(0);
        railAnchor = GameObject.FindGameObjectWithTag(Tags.enemyRailAnchor);
        spawnAtDistance *= spawnAtDistance;
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }
    // Update is called once per frame
    void Update()
    {
        if ((transform.position - player.transform.position).sqrMagnitude < spawnAtDistance)
        {
            SpawEnemys();
            DestroySpawer();
        }
    }
    public void SpawEnemys()
    {
        for (int i = 0; i < numberOfEnemys; i++)
        {
            rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
            offsetFromSorce = rotation * Vector3.right * Random.Range(2, maxRadius * 2);
            GameObject spawedEnemy = Instantiate(SpawnType, transform.position + offsetFromSorce, rotation);

            //TODO: fix this nice
            //Spawn is of coded type
            EnemyMovement enemyMoveScript = spawedEnemy.GetComponent<EnemyMovement>();
            if (enemyMoveScript != null)
            {
                enemyMoveScript.SpawnInit(i, CircularPath(), railAnchor, player);
            }
            else
            {
                
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
