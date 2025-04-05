using UnityEngine;

public class Apple : Pickup
{
    [SerializeField] float speedIncreaseAmount = 3f; // 속도 증가량
    LevelGenerator levelGenerator; // LevelGenerator 컴포넌트

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>(); // LevelGenerator 컴포넌트 찾기
    }

    protected override void OnPickup()
    {
        levelGenerator.ChunkMoveSpeed(speedIncreaseAmount); // 청크 이동 속도 증가
        Debug.Log("Apple picked up!");
    }
}
