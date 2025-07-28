using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // 게임 매니저 싱글톤
    private static GameManager instance;
    public static GameManager Instance 
    {
        get 
        {
            if (instance == null) // 게임매니저가 없다면
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        } 
    }

    // 플레이어 위치 가져오기
    // 스택쌓기 미니게임으로 넘어가면 player가 없는데 어떻게 하지?
    [SerializeField] private Transform player;
    public Transform Player { get =>  player; }

    // 씬 이동 효과 설정
    [SerializeField] private CanvasGroup fadeOutCanvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // 이미 게임 매니저가 있다면 새로 생긴 것을 파괴
            Destroy(this.gameObject);
        }
        else
        {
            // 첫 인스턴스라면 static 변수에 할당
            instance = this;
            // 씬이 바뀌어도 파괴되지 않게 한다. (모든 씬에서 동일한 정보를 가진 GameManager가 된다.)
            DontDestroyOnLoad(this.gameObject);
        }

        fadeOutCanvasGroup.alpha = 0f;
    }

    // 씬 매니저에서 Awake() 다음으로 호출한다.
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 게임이 종료될 때 호출된다.
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 플레이어를 찾음
        PlayerController playerObject = FindObjectOfType<PlayerController>();

        if (playerObject != null)
        {
            player = playerObject.transform; // 플레이어가 있는 씬이라면 플레이어의 위치를 가져온다.(카메라 팔로우를 위해)
            Debug.Log($"{scene.name} 씬 로드 완료. 플레이어를 발견하여 참조합니다."); // 디버그용
        }
        else
        {
            player = null; // 플레이어가 없다면 null을 넣어서 오류가 발생하지 않게 한다.
            Debug.Log($"{scene.name} 씬 로드 완료. 플레이어가 없어 참조를 비웁니다."); // 디버그용
        }
    }

    // 외부에서 호출할 씬 전환 메서드
    public void LoadSceneWithFade(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    // 어두워지고 -> 씬을 불러오고 -> 밝아지는 과정
    private IEnumerator FadeAndLoadScene(string sceneName)
    {
        // 페이드 아웃 (점점 어두워짐)
        yield return StartCoroutine(Fade(1f));

        // 씬 로드
        yield return SceneManager.LoadSceneAsync(sceneName);

        // 여유 시간
        yield return null;

        // 페이드 인 (점점 밝아짐)
        yield return StartCoroutine(Fade(0f));
    }

    // 어두워지거나 밝아지는 효과
    private IEnumerator Fade(float targetAlpha)
    {
        float speed = Mathf.Abs(fadeOutCanvasGroup.alpha - targetAlpha) / fadeDuration;
        while (!Mathf.Approximately(fadeOutCanvasGroup.alpha, targetAlpha))
        {
            fadeOutCanvasGroup.alpha = Mathf.MoveTowards(fadeOutCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
            yield return null;
        }
        fadeOutCanvasGroup.alpha = targetAlpha; // 정확히 목표 값으로 설정
    }

}
