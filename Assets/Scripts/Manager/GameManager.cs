using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
