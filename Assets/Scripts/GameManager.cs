using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int MobCounter;

    [SerializeField] GameObject playerObject;
    [SerializeField] GameObject[] mobPrefabs;
    [SerializeField] GameObject milkPrefab;

    private PlayerController playerController;
    private Bullet bullet;
    private Cow cowScript;
    private Vector3 mobSpawnPoint;
    private int currentWave;
    private float timeToNextWave = 7;
    private int totalMobToSpawn;
    private float spawnRate = 1;
    private bool isWaveActive;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        bullet = GetComponent<Bullet>();
        cowScript = FindObjectOfType<Cow>();
        currentWave = 0;
        MobCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MobSpawner();
    }

    public void MobSpawner()
    {
        MobCounter = FindObjectsOfType<EmptyScriptToMobCount>().Length;
        Debug.Log(MobCounter);

        if (MobCounter <= 0 && !isWaveActive)
        {
            isWaveActive = true;
            StartCoroutine(WaveCooldown());
            Debug.Log("isWaveActive " + isWaveActive);
            Debug.Log(MobCounter);
        }
    }
    // Wait between waves and increase currenWave value +1
    IEnumerator WaveCooldown()
    {
        yield return new WaitForSeconds(timeToNextWave);
        currentWave++;
        totalMobToSpawn = currentWave * 5;
        Debug.Log("currentWave " + currentWave);

        while (totalMobToSpawn > 0)
        {
            // adjust dog spawn rate
            int randomMobIndex = Random.Range(0, mobPrefabs.Length);
            int cowCounter = FindObjectsOfType<Cow>().Length;
            // if cow spawned
            if (randomMobIndex == 0 && cowCounter < 2)
            {
                float spawnRangeX = 15;
                float spawnRangeY = 7;
                mobSpawnPoint = new Vector2(spawnRangeX, Random.Range(-spawnRangeY, spawnRangeY));
                Instantiate(mobPrefabs[randomMobIndex], mobSpawnPoint, mobPrefabs[randomMobIndex].transform.rotation);
            }
            // if bird spawned
            else if (randomMobIndex == 1 && currentWave == 3)
            {
                float spawnRangeX = 20;
                float spawnRangeY = 10;
                mobSpawnPoint = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));
                while (mobSpawnPoint.magnitude < 12)
                {
                    mobSpawnPoint = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), Random.Range(-spawnRangeY, spawnRangeY));
                }
                Debug.Log(mobSpawnPoint.magnitude);
                Instantiate(mobPrefabs[randomMobIndex], mobSpawnPoint, mobPrefabs[randomMobIndex].transform.rotation);
            }
            // if mouse spawn
            else if (randomMobIndex == 2)
            {
                float spawnRangeX = 10;
                float spawnRangeY = 15;
                mobSpawnPoint = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnRangeY);
                Instantiate(mobPrefabs[randomMobIndex], mobSpawnPoint, mobPrefabs[randomMobIndex].transform.rotation);
            }
            // if dog spawn
            else if (randomMobIndex == 3)
            {
                float spawnRangeX = 15;
                float spawnRangeY = 15;
                mobSpawnPoint = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnRangeY);
                Instantiate(mobPrefabs[randomMobIndex], mobSpawnPoint, mobPrefabs[randomMobIndex].transform.rotation);
            }
            totalMobToSpawn--;

            yield return new WaitForSeconds(spawnRate);
            Debug.Log("totalMobToSpawn " + totalMobToSpawn);
        }
        isWaveActive = false;
    }

    public void MilkSpawner()
    {
        //Debug.Log("Milk dropped");
        Instantiate(milkPrefab, playerController.cowLastPosition, mobPrefabs[0].gameObject.transform.rotation);
    }
}
