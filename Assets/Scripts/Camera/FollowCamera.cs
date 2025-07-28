using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform target;

    [Header("Camera Info")]
    [SerializeField] private float smoothSpeed = 5f; // 카메라가 부드럽게 따라오는 정도
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // 카메라가 플레이어와 어느정도 거리를 두고 움직인다.

    // 콜라이더를 받아와서 제한 설정을 하려고 했으나 도저히 안되서 임시로 직접 좌표를 받도록 설정.
    [Header("경계 설정 (직접 입력)")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;


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

        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);
        Vector3 finalPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        Vector3 smoothCameraPosition = Vector3.Lerp(transform.position, finalPosition, Time.deltaTime * smoothSpeed);
        transform.position = smoothCameraPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 center = new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0);
        Vector3 size = new Vector3(maxX - minX, maxY - minY, 0);
        Gizmos.DrawWireCube(center, size);
    }
}
