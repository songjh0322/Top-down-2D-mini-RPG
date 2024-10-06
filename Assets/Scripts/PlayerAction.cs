using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    float h;
    float v;

    bool isHorizonMove; //수평이동인지를 확인하는 플래그
    Vector2 moveVec; //대각선 이동을 막기위한 벡터 변수
    public float speed = 5f;
    Rigidbody2D rigid;
    Animator anim;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h != 0 && v == 0) //수평이동할때
        {
            isHorizonMove = true;
        }
        else if (v != 0 && h == 0) //수직이동할때
        {
            isHorizonMove = false;
        }
        // 애니메이션 상태 변경 감지 및 업데이트
        if (anim.GetInteger("hAxisRaw") != (int)h || anim.GetInteger("vAxisRaw") != (int)v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("hAxisRaw", (int)h);
            anim.SetInteger("vAxisRaw", (int)v);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
        

    }
    private void FixedUpdate()
    {
        if (isHorizonMove)
        {
            moveVec = new Vector2(h, 0);
        }
        else
        {
            moveVec = new Vector2(0, v);
        }
        rigid.velocity = moveVec * speed;
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
