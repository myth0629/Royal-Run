using System.Collections;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] float spawnTime = 1f; // 장애물 생성 주기

    void Start()
    {
        // 장애물 생성 시작
        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnTime); // 1초 대기
            Instantiate(obstaclePrefab, transform.position, Random.rotation);
        }
    }
}
