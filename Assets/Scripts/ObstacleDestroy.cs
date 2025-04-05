using UnityEngine;

public class ObstacleDestroy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject); // 충돌한 오브젝트 삭제   
    }
}
