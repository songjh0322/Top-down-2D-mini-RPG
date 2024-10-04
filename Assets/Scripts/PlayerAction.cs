using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;
    public float speed = 5f;
    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }
    private void FixedUpdate()
    {
        rigid.velocity = new Vector2(h * speed, v * speed);
    }

    //Soil 타일맵에 들어가면 이동속도가 느려지는 함수
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BackUp(Soil)")
        {
            speed = 3.5f;
        }
    }

    //Soil 타일맵에서 빠져나오면 원래의 이동속도로 돌아가는 함수
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BackUp(Soil)")
        {
            speed = 5f;
        }
    }
}
