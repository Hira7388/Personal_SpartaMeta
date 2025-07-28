using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortarStackScene : MonoBehaviour
{
    [Header("전환 설정")]
    [Tooltip("이동할 씬의 이름을 정확히 입력하세요.")]
    [SerializeField] private string sceneToLoad;

    [Header("기즈모 설정")]
    [SerializeField] private Color gizmoColor = new Color(0, 1, 1, 0.5f); // 청록색, 50% 투명도

    private bool isTransitioning = false;
    private Collider2D triggerZone;

    private void Awake()
    {
        // 최적화를 위해 콜라이더를 미리 찾아둡니다.
        triggerZone = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTransitioning || !other.CompareTag("Player"))
        {
            return;
        }

        isTransitioning = true;
        Debug.Log($"'{sceneToLoad}' 씬으로 전환을 시작합니다.");
        GameManager.Instance.LoadSceneWithFade(sceneToLoad);
    }

    /// <summary>
    /// 이 게임 오브젝트가 Scene 뷰에서 선택되었을 때만 기즈모를 그립니다.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        // 스크립트에 연결된 콜라이더의 영역을 가져와 기즈모를 그립니다.
        // 이 방식은 Rect를 쓰는 것보다 시각적으로 영역을 편집할 수 있어 더 편리합니다.
        if (triggerZone == null)
        {
            triggerZone = GetComponent<Collider2D>();
        }

        Gizmos.color = gizmoColor;
        Gizmos.DrawCube(triggerZone.bounds.center, triggerZone.bounds.size);
    }
}
