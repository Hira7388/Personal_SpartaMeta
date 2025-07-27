using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;

    [Header("Camera Info")]
    [SerializeField] private float smoothSpeed = 0.1f; // 카메라가 부드럽게 따라오는 정도
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // 카메라가 플레이어와 어느정도 거리를 두고 움직인다.

    private void Start()
    {
        if(GameManager.Instance != null && GameManager.Instance.Player != null)
        {
            target = GameManager.Instance.Player;
        }
        else
        {
            Debug.Log("게임 매니저 혹은 플레이어를 찾을 수 없습니다.");
        }
    }

    private void LateUpdate()
    {
        // 타겟이 없다면 동작하지 않는다.
        if (target == null)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothCameraPosition = Vector3.Lerp(target.position, desiredPosition, smoothSpeed);
        transform.position = smoothCameraPosition;
    }
}
