using UnityEngine;

public class Pickup : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name); // 충돌한 오브젝트의 이름을 출력
    }
}
