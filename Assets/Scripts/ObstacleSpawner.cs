using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] float spawnTime = 1f; // 장애물 생성 주기
    [SerializeField] Transform obstacleParent;
    [SerializeField] float spawnWidth = 4f; // 장애물 생성 범위

    void Start()
    {
        // 장애물 생성 시작
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while(true)
        {
            // 장애물 프리팹 중 랜덤으로 선택하여 생성
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);
            yield return new WaitForSeconds(spawnTime); // 1초 대기
            Instantiate(obstaclePrefab, spawnPosition, Random.rotation, obstacleParent);
        }
    }
}
