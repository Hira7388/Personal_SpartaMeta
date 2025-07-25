using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 스텟 데이터를 관리하는 클래스 컴포넌트
public class StatHandler : MonoBehaviour
{
    // 이동 속도
    [SerializeField][Range(1f, 20f)] private float moveSpeed = 2;
    public float MoveSpeed
    { 
        get => moveSpeed; 
        set => moveSpeed = value;
    }

    // 체력
    [SerializeField][Range(1, 100)] private int health = 10;
    public int Health
    {
        get => health;
        set => health = value;
    }
}
