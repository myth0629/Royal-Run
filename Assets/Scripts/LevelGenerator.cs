using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] GameObject chunkPrefab;
    [SerializeField] int startingChunksAmount = 12;
    [SerializeField] Transform chunkParent;
    [SerializeField] float chunkLength = 10f; // 청크의 길이
    [SerializeField] float moveSpeed = 8f; // 청크 이동 속도
    [SerializeField] float minMoveSpeed = 2f;
    [SerializeField] float maxMoveSpeed = 20f; // 최대 이동 속도


    List<GameObject> chunks = new List<GameObject>(); // 청크 리스트

    void Start()
    {
        // 레벨 생성 시작
        GenerateChunk();
    }

    void Update()
    {
        MoveChunks();
        // 청크를 이동시키고, 필요에 따라 새로운 청크를 생성합니다.
    }

    public void ChunkMoveSpeed(float speed)
    {
        moveSpeed += speed;
        // 청크 이동 속도를 설정합니다.

        if(moveSpeed < minMoveSpeed)
        {
            moveSpeed = minMoveSpeed;
            // 최소 이동 속도를 설정합니다.
        }
        else if(moveSpeed > maxMoveSpeed)
        {
            moveSpeed = maxMoveSpeed;
            // 최대 이동 속도를 설정합니다.
        }

        Physics.gravity = new Vector3(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speed);
    }

    private void GenerateChunk()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            float spawnPositionZ = StartSpawnPositionZ();
            // 청크의 Z축 위치를 계산합니다.

            Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
            // 청크를 생성하고 위치를 설정합니다.
            GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

            chunks.Add(newChunk);
            // 생성된 청크를 리스트에 추가합니다.
        }
    }

    float StartSpawnPositionZ()
    {
        float spawnPositionZ = transform.position.z - (chunkLength * startingChunksAmount / 2f);
        // 청크의 Z축 위치를 계산합니다.
        return spawnPositionZ;
    }

    float GetSpawnPositionZ()
    {
        float spawnPositionZ;

        if(chunks.Count == 0)
        {
            spawnPositionZ = transform.position.z;
        }
        else
        {
            spawnPositionZ = chunks[chunks.Count - 1].transform.position.z + chunkLength;
            // 마지막 청크의 Z축 위치에 청크 길이를 더하여 새로운 청크의 Z축 위치를 계산합니다.
        }

        return spawnPositionZ;
    }

    void MoveChunks()
    {
        for (int i = 0; i < chunks.Count; i++)
        {
            if (chunks[i] != null)
            {
                GameObject chunk = chunks[i];
                // 청크를 가져옵니다.
                chunk.transform.position += Vector3.back * (moveSpeed * Time.deltaTime);
                // 청크를 뒤로 이동시킵니다.

                if (chunk.transform.position.z < transform.position.z - chunkLength)
                {
                    chunks.Remove(chunk);
                    // 청크가 화면 밖으로 나가면 리스트에서 제거합니다.
                    Destroy(chunk);
                    // 청크가 화면 밖으로 나가면 파괴합니다.
                    GenerateNewChunk();
                }
            }
        }
    }

    void GenerateNewChunk()
    {
        float spawnPositionZ = GetSpawnPositionZ();
        // 새로운 청크의 Z축 위치를 계산합니다.

        Vector3 chunkSpawnPos = new Vector3(transform.position.x, transform.position.y, spawnPositionZ);
        // 새로운 청크를 생성하고 위치를 설정합니다.
        GameObject newChunk = Instantiate(chunkPrefab, chunkSpawnPos, Quaternion.identity, chunkParent);

        chunks.Add(newChunk);
    }
}

