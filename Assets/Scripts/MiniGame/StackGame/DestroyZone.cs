using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Rubble")) // Rubble 태그를 가지고 있다면 삭제함 
        {
            Destroy(collision.gameObject);
        }
    }
}
