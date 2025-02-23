using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn;

    public float timeToSpawn;
    private float spawnCounter;

    public Transform minSpawn,maxSpawn;

    private Transform target;

    private float despawnDistance;

    private List<GameObject>spawnedEnemies = new List<GameObject>();

    public int checkPerFrame;
    private int enemyToCheck;

    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;

    // Start is called before the first frame update
    void Start()
    {
        spawnCounter = timeToSpawn;

        target = PlayrHealth.Instance.transform;

        despawnDistance = Vector3.Distance(transform.position, maxSpawn.position)+4f;

        currentWave = -1;

    }
    
    // Update is called once per frame 
    void Update()
    {
        //spawnCounter-=Time.deltaTime;
        //if(spawnCounter <= 0 ) {
        //    spawnCounter = timeToSpawn;
        //    GameObject newEnemy = Instantiate(enemyToSpawn,SelectSpawnPoint(), transform.rotation);

        //    spawnedEnemies.Add(newEnemy);
        //}

        if (PlayrHealth.Instance.gameObject.activeSelf)
        {
            if (currentWave<waves.Count)
            {
                waveCounter -=Time.deltaTime;
                if (waveCounter<=0)
                {
                    GotoNextWave();
                }
                spawnCounter-=Time.deltaTime;
                if (spawnCounter<=0) {
                    spawnCounter = waves[currentWave].timeBetweenSpawn;
                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPoint(), Quaternion.identity);
                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        transform.position = target.position;

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck<checkTarget)
        {
            //avoid none zero "enemyToCheck"
            if (enemyToCheck >= spawnedEnemies.Count)
            {
                enemyToCheck = 0;
                checkTarget = 0;
                break;
            }

            //avoid null
            if (spawnedEnemies[enemyToCheck] == null)
            {
                spawnedEnemies.RemoveAt(enemyToCheck);
                checkTarget--;
                continue;
            }

            //destroy enemy
            if (Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
            {
                Destroy(spawnedEnemies[enemyToCheck]);
                spawnedEnemies.RemoveAt(enemyToCheck);
                checkTarget--;
            }
            else
            {
                enemyToCheck++;
            }
        }
    }

    public Vector3 SelectSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalage = Random.Range(0f, 1f)>  .5f;

        if (spawnVerticalage)
        {
            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
            if (Random.Range(0f,1f)>.5f) {
                spawnPoint.x = maxSpawn.position.x;
            }
            else
            {
                spawnPoint.x = minSpawn.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawn.position.y;
            }
            else
            {
                spawnPoint.y = minSpawn.position.y;
            }
        }
        return spawnPoint;
    }

    public void GotoNextWave()
    {
        currentWave++;
        if (currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;

        spawnCounter = waves[currentWave].timeBetweenSpawn;
    }
}


[System.Serializable]
public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawn = 1f;
}
