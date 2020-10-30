//Robert S
//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;


public class EnemyCluster : MonoBehaviour
{
    public int level;

    [Header("EnemyType")]
    public bool moving = false;
    private GameObject SpawnType;
    private int spawnNumber;
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
    private float spawnRadius = 10;
    private int spawnSegments = 24;
    private Vector2[] patrolPath;
    public int startP;
    private Quaternion rotation = Quaternion.identity;

    [Header("TunnelMotion")]
    private GameObject player;
    private GameObject railAnchor;

    // Start is called before the first sframe update
    void Start()
    {
        if(randomized)
            RandomizeSpawnAtLevel(0);

        railAnchor = GameObject.FindGameObjectWithTag(Tags.enemyRailAnchor);
        spawnAtDistance *= spawnAtDistance;
        player = GameObject.FindGameObjectWithTag(Tags.player);
    }
    public void RandomizeSpawnAtLevel(int level)
    {
        spawnNumber = (int) Random.Range(-0.9f, 3);
        //spawnNumber = 0;
        //Enemy
        if(spawnNumber == 0)
        {
            SpawnType = EnemyPreFab;

            spawnSegments = Random.Range(2, 6);
            numberOfEnemys = Random.Range(1, spawnSegments);
            if(spawnSegments > 3)
            {
                spawnSegments = 24;
            }

            startP = Mathf.RoundToInt(spawnSegments / numberOfEnemys);

            spawnRadius = Random.Range(numberOfEnemys, maxRadius);
            if (spawnRadius > maxRadius)
                spawnRadius = maxRadius;

            rotation = Random.rotation;
        }//Pillar
        else if(spawnNumber == 1)
        {
            SpawnType = PillarPreFab;

            numberOfEnemys = Random.Range(1, 3);
            rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            rotation = Quaternion.AngleAxis(Random.Range(0,360), Vector3.forward);
            offsetFromSorce = rotation * Vector3.right * Random.Range(2,maxRadius * 2);
        }
        //BlodCell
        else
        {
            SpawnType = BloodCellPreFab;

            numberOfEnemys = Random.Range(5, 6);
            rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
            offsetFromSorce = rotation * Vector3.right * Random.Range(2, maxRadius * 2);
        }
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
            //Enemy Bacteria
             if(spawnNumber == 0)
            {
                GameObject spawedEnemy = Instantiate(SpawnType, transform.position + offsetFromSorce, rotation);
                EnemyMovement enemyMoveScript = spawedEnemy.GetComponent<EnemyMovement>();
                //TODO: fix this nice
                //Spawn is of coded type
                if (enemyMoveScript != null)
                {
                    enemyMoveScript.SpawnInit(i*startP, CircularPath(), railAnchor, player);
                    //Move to correct pos
                }
            }
            else if(spawnNumber == 1)
            {
                rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
                rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                offsetFromSorce = rotation * Vector3.right * Random.Range(2, maxRadius * 2);
                offsetFromSorce.z = i;
                GameObject spawedEnemy = Instantiate(SpawnType, transform.position + offsetFromSorce, rotation);
            }
            else
            {
                rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
                rotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.forward);
                offsetFromSorce = rotation * Vector3.right * Random.Range(2, maxRadius * 2);
                offsetFromSorce.z = i;
                GameObject spawedEnemy = Instantiate(SpawnType, transform.position + offsetFromSorce, rotation);
                spawedEnemy.GetComponent<Rigidbody>().velocity = Random.insideUnitSphere;
                spawedEnemy.GetComponent<Rigidbody>().rotation = (Random.rotation);
                spawedEnemy.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere * 5);
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
