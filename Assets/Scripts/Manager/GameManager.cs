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
            Debug.Log($"{scene.name} 씬 로드 완료. 플레이어를 발견하여 참조합니다.");
        }
        else
        {
            player = null; // 플레이어가 없다면 null을 넣어서 오류가 발생하지 않게 한다.
            Debug.Log($"{scene.name} 씬 로드 완료. 플레이어가 없어 참조를 비웁니다.");
        }
    }
}
