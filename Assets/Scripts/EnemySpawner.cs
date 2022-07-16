using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public float Radius = 30f;
    [SerializeField]
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(SpawnRoutine());
        Vector2 randomCircle = Random.insideUnitCircle;
        Vector3 randomPos = new Vector3(randomCircle.x, 0, randomCircle.y) * Radius;
        Instantiate(enemyPrefab, randomPos, Quaternion.Euler(Random.Range(-360, 360), 0f, 0f));
        //NavMeshHit myNavHit;
        //if (NavMesh.SamplePosition(Random.insideUnitCircle * Radius, out myNavHit, 100, -1))
        //{
        //    Instantiate(enemyPrefab, myNavHit.position, Quaternion.Euler(Random.Range(-360, 360), 0f, 0f));
        //}
    }

    //IEnumerator SpawnRoutine()
    //{

    //}

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }
}
