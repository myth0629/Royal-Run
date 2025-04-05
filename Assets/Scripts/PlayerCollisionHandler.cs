using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] Animator animator; // 애니메이터 컴포넌트
    [SerializeField] float collisionCooldown = 1f;
    [SerializeField] float changeSpeedAmount = -2f; // 속도 변경량

    const string hitAnimTrigger = "Hit";

    float cooldownTimer = 0f; // 쿨타임 타이머

    LevelGenerator levelGenerator;

    void Start()
    {
        levelGenerator = FindFirstObjectByType<LevelGenerator>(); // LevelGenerator 컴포넌트 찾기
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime; // 쿨타임 타이머 증가
    }

    void OnCollisionEnter(Collision collision)
    {
        if(cooldownTimer < collisionCooldown) // 쿨타임이 지나지 않았으면 종료
        {
            return;
        }

        levelGenerator.ChunkMoveSpeed(changeSpeedAmount); // 청크 이동 속도 감소
        animator.SetTrigger(hitAnimTrigger);
        cooldownTimer = 0f; // 쿨타임 초기화
    }

}
