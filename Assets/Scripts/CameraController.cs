using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float zoomDuration = 1f; // FOV 변경 속도
    [SerializeField] float minFOV = 60f; // 최소 FOV
    [SerializeField] float maxFOV = 120f; // 최대 FOV
    [SerializeField] float zoomSpeedModifier = 5f; // 줌 속도 조절기


    CinemachineCamera cinemachineCamera;

    void Awake()
    {
        cinemachineCamera = GetComponent<CinemachineCamera>();
    }

    public void ChangeCameraFOV(float speed)
    {
        StopAllCoroutines(); // 기존 코루틴 중지
        StartCoroutine(ChangeFOV(speed)); // 코루틴 시작
    }

    IEnumerator ChangeFOV(float speed)
    {
        float startFOV = cinemachineCamera.Lens.FieldOfView;
        float targetFOV = Mathf.Clamp(startFOV + speed * zoomSpeedModifier, minFOV, maxFOV); // 목표 FOV 설정

        float elapsedTime = 0f;

        while(elapsedTime < zoomDuration)
        {
            float t = elapsedTime / zoomDuration;
            elapsedTime += Time.deltaTime;
            cinemachineCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, elapsedTime / zoomDuration); // FOV 보간
            yield return null; // 다음 프레임까지 대기
        }

        cinemachineCamera.Lens.FieldOfView = targetFOV; // 최종 FOV 설정
    }
}
