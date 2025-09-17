using UnityEngine;

public class PowerUpSpawner : MonoBehaviour            //script to manage powerups
{
    public static PowerUpSpawner Instance;                //so that other scripts can access

    public GameObject[] powerUpPrefabs;                  //to assign Speed, Score, Time prefabs
    private GameObject[] spawnPoints;                    //to store spawn points automatically 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);                                  //singleton

        spawnPoints = GameObject.FindGameObjectsWithTag("PowerUp");                  //tagged all points where a power up can spawn as PowerUp, so fetches them
    }

    public void SpawnRandomPowerUp()
    {
        if (spawnPoints.Length == 0 || powerUpPrefabs.Length == 0) return;                  //safety measure, so nothing happens, if there are no power ups or spawn points defined

        GameObject point = spawnPoints[Random.Range(0, spawnPoints.Length)];                //a random location from points
        GameObject prefab = powerUpPrefabs[Random.Range(0, powerUpPrefabs.Length)];         //a random power up prefab to spawn there

        Instantiate(prefab, point.transform.position, Quaternion.identity);                 //creates the power up prefab at the point without rotating it unnecessarily 
    }
}