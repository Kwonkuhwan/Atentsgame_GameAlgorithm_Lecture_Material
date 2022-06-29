using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pipe : MonoBehaviour
{
    //public float moveSpeed = 5.0f;
    public float min = 0.0f;
    public float max = 4.0f;

    private Rigidbody2D rigid = null;
    private float randomHeight = 0.0f;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {        
        ResetHeight();  // 시작할 때 높이 결정
    }

    //private void FixedUpdate()
    //{
    //    //rigid.MovePosition((Vector2)rigid.position + Time.fixedDeltaTime * Vector2.left * moveSpeed);
    //}

    /// <summary>
    /// 파이프의 높이를 랜덤으로 리셋하는 함수
    /// </summary>
    public void ResetHeight()
    {
        //rigid.MovePosition(new(rigid.position.x, Random.Range(min, max)));
        randomHeight = Random.Range(min, max);
    }

    /// <summary>
    /// 파이프를 이동시키는 함수
    /// </summary>
    /// <param name="moveDelta">파이프 이동 벡터(방향과 크기)</param>
    public void Move(Vector2 moveDelta)
    {
        // 한 프레임안에 MovePosition을 두번 호출하면 마지막것만 적용이 됨
        Vector2 pos = new(rigid.position.x, randomHeight);  // 높이를 movePosition으로 움직이려고 하니 문제가 많았음.
                                                            // 새 현재 위치 만들어서 이걸 기준으로 움직임.
                                                            // 높이는 항상 randomHeight에 설정된 높이로.
        rigid.MovePosition(pos + moveDelta);    // MovePosition으로 이동시키기
    }

    /// <summary>
    /// 파이프의 위치를 설정하는 함수
    /// </summary>
    /// <param name="pos">이동시킬 위치</param>
    public void Set(Vector2 pos)
    {
        rigid.position = pos;   // 위치 설정
    }
}
