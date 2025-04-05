using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f; // 회전 속도
    const string playerString = "Player"; // 플레이어 태그

    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime); // 오브젝트 회전
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(playerString)) // 플레이어와 충돌했을 때
        {
            OnPickup(); // 자식 클래스에서 구현한 메서드 호출
            Destroy(gameObject); // 오브젝트 삭제
        }
    }

    protected abstract void OnPickup(); // 추상 메서드, 자식 클래스에서 구현해야 함
}
