using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType  // 총알 타입에 대한 열거형 (상수를 기억하기 좋게 그룹화하는 것)
{
    Main = 0,
    Sub = 1,
    Pet = 2
}



public class Bullet : MonoBehaviour
{

    // public int BulletType = 0; // 0이면 주총알, 1이면 보조총알, 2면 펫이 쏘는 총알
    public BulletType BType = BulletType.Main;

    // 목표 : 총알이 위로 계속 이동하고 싶다.
    // 속성 : 
    // - 속력 (이동속도)
    // 구현 순서
    // 1. 이동할 방향을 구한다.
    // 2. 이동한다.

    public float Speed = 1.0f;



    // Update is called once per frame
    void Update()
    {

        // 1. 이동할 방향을 구한다.
        Vector2 moveDirection = Vector2.up;
        // 2. 이동한다.
        // transform.Translate(moveDirection * Speed * Time.deltaTime);
        // 새로운 위치 = 현재 위치 * 속도 * 시간
        transform.position += (Vector3)(moveDirection * Speed) * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌을 시작했을 때
        Debug.Log("Enter");
        // 2. 적과 플레이어를 삭제한다
        // 너죽고 (적)
        // Destroy(collision.collider.gameObject);
        // 나죽자 (나 자신)
        Destroy(this.gameObject);
    }


}
