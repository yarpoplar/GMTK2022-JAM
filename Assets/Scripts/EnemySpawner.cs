using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public struct SpawnStruct
{
    public Enemy[] EnemyTypes;
    public int MaxEnemies;
    public float SpawnInterval;
}

public class EnemySpawner : MonoBehaviour
{
    public bool DoSpawn = true;
    public float Radius = 30f;
    [SerializeField]
    public GameObject enemyPrefab;
    [SerializeField]
    public SpawnStruct[] SpawnRules;
    //In seconds
    float TimePassed = 0f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        TimePassed += Time.deltaTime;
    }

    IEnumerator SpawnRoutine()
    {
        int index = Mathf.FloorToInt(TimePassed/60);
        SpawnStruct currentSpawnStruct = SpawnRules[index];
        if (DoSpawn)
            SpawnEnemy(currentSpawnStruct.EnemyTypes[Random.Range(0, currentSpawnStruct.EnemyTypes.Length)]);
        yield return new WaitForSeconds(currentSpawnStruct.SpawnInterval);
        StartCoroutine(SpawnRoutine());
    }

    void SpawnEnemy(Enemy EnemyType)
    {
        Vector2 randomCircle = (Random.insideUnitCircle).normalized;
        Vector3 randomPos = new Vector3(randomCircle.x, 0, randomCircle.y) * Radius;
        Instantiate(EnemyType, randomPos, Quaternion.identity);
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
