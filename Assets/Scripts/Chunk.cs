using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab; // 장애물 프리팹
    [SerializeField] GameObject applePrefab; 
    [SerializeField] GameObject coinPrefab; 
    [SerializeField] float appleSpawnChance = 0.5f; // 사과 생성 확률
    [SerializeField] float coinSpawnChance = 0.5f; // 동전 생성 확률
    [SerializeField] float[] lanes = { -2.5f, 0f, 2.5f }; // 장애물 생성 위치 (3개)
    [SerializeField] float coinSeperationLength = 2f;
    
    List<int> availableLanes = new List<int>{0, 1, 2}; // 사용 가능한 장애물 위치를 저장할 리스트

    void Start()
    {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length); // 0~2개 장애물 생성

        for(int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count == 0) // 사용 가능한 장애물 위치가 없으면 종료
            {
                break;
            }

            int randomLaneIndex = SelectLane();

            Vector3 spawanPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z); // 장애물 생성 위치
            Instantiate(fencePrefab, spawanPosition, Quaternion.identity, transform); // 장애물 생성
            // transform을 붙여야 장애물이 Chunk의 자식으로 생성되어 같이 움직임 
        }
    }

    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count); // 0~2 랜덤 생성
        int selectedLane = availableLanes[randomLaneIndex]; // 랜덤으로 선택된 장애물 위치
        availableLanes.RemoveAt(randomLaneIndex); // 선택된 장애물 위치는 리스트에서 제거
        return selectedLane; // 선택된 장애물 위치 반환
    }

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) // 랜덤으로 사과 생성 여부 결정
        {
            return;
        }

        int randomLaneIndex = SelectLane();

        Vector3 spawanPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, transform.position.z); // 장애물 생성 위치
        Instantiate(applePrefab, spawanPosition, Quaternion.identity, transform); // 장애물 생성
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) // 랜덤으로 코인 생성 여부 결정
        {
            return;
        }

        int randomLaneIndex = SelectLane();

        int maxCoinsToSpawn = 6;
        int coinsToSpawn = Random.Range(1, maxCoinsToSpawn); 

        float topOfChunkZPos = transform.position.z + (coinSeperationLength * 2f); // 코인 생성 위치 (장애물 위쪽)

        for (int i = 0; i < coinsToSpawn; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (coinSeperationLength * i);

            Vector3 spawanPosition = new Vector3(lanes[randomLaneIndex], transform.position.y, spawnPositionZ); // 코인 생성 위치
            Instantiate(coinPrefab, spawanPosition, Quaternion.identity, transform); // 코인 생성
        }
    }
}
