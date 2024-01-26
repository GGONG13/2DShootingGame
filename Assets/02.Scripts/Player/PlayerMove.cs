using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /**
       목표 : 플레이어를 이동하고 싶다. 
       필요 속성 :
        - 이동 속도
       순서 : 
       1. 키보드 입력을 받는다. 
       2. 키보드 입력에 따라 이동할 방향을 계산한다.
       3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
     **/

    public float Speed = 3f;  // 이동 속도 : 초당 3만큼 이동하겠다.
    public const float MinX = -3f;
    public const float MaxX = 3f;
    public const float MinY = -6f;
    public const float MaxY = 0f;

    public Animator MyAnimator;
    public Animator PlayerHitC;
    public Animator PlayerHitR;
    public Animator PlayerHitL;

    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Update()
    {



        // transform.Translate(Vector2.up * Speed * Time.deltaTime);
        // (0, 1) * 3 -> (0, 3) * Time.deltaTime 
        // deltaTime은 1초, 프레임 간 시간 간격을 반환한다.
        // 30fps: d(델타타임)-> 0.03초
        // 60fps: d(델타타임)-> 0.016초

        // 1. 키보드 입력을 받는다.
        // float h = Input.GetAxis("Horizontal"); // 키보드 좌우 입력에 따라 -1.0f ~ 0f ~ +1.0f 반환
        // float v = Input.GetAxis("Vertical");
        float h = Input.GetAxisRaw("Horizontal"); // 키보드 좌우 입력에 따라 -1.0f, 0f, +1.0f 반환
        float v = Input.GetAxisRaw("Vertical");   // 키보드 위아래 입력에 따라 -1.0f, 0f, +1.0f 반환
                                                  // Debug.Log($"h : {h}/ s : {v}");

        MyAnimator.SetInteger("h", (int)h);

        /**
        if (Input.GetKeyUp(KeyCode.E))
        {
            Speed += 1f;
        }
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Speed -= 1f;
        }
        **/

        // 2. 키보드 입력에 따라 이동할 방향을 계산한다.
        // Vector2 dir = Vector2.right * h + Vector2.up * v;
        //                   (1,0) * h + (0,1) * v = (h, v)

        // 방향을 각 성분으로 제작
        Vector2 dir = new Vector2(h, v); // 이렇게 정리 가능함

        dir = dir.normalized;
        //  Debug.Log($"정규화 전 : {dir.magnitude}");

        // 이동 방향을 정규화 (방향은 같지만 길이를 1로 만들어줌 = 대각선으로 이동할때도 똑같은 스피드로 움직이게)
        dir = dir.normalized;
        //   Debug.Log($"정규화 후 : {dir.magnitude}");

        // 3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
        // Debug.Log(Time.deltaTime);
        // transform.Translate( dir * Speed * Time.deltaTime );
        // 공식을 이용한 이동
        // 새로운 위치 = 현재 위치 + 속도 * 시간

        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;
        /**
        새로운 위치를 잘 수정해본다. 
         **/

        /**
        if (newPosition.x < MinX)
        {
            newPosition.x = MinX;
        }
        else if (newPosition.x > MaxX)
        { 
            newPosition.x = MaxX;
        }
        **/

        // newPosition.y = Mathf.Max(MinY, newPosition.y);
        // newPosition.y = Mathf.Min(newPosition.y, MaxY);
        // newPosition.x = Mathf.Clamp(newPosition.x, MinX, MaxX);
        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }

        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        /* if (newPosition.y > MaxY)
         {
             newPosition.y = MaxY;
         }
         else if (newPosition.y < MinY)
         {
             newPosition.y = MinY;
         }
        */

        transform.position = newPosition; // 플레이어의 위치 = 새로운 위치

        // 4. 현재 위치 출력
        //    Debug.Log(transform.position);
        // transfrom.position = new Vector2 (dir.x,dir.y);

        // 목표 : Q/E 버튼을 누르면 속력을 바꾸고 싶다.
        // 속성 
        // - 속력 (Speed)
        // 순서 :

        // 1. Q/E 버튼 입력을 판단한다
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 2. Q 버튼이 눌렸다면 스피드 1다운
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 3. E 버튼이 눌렸다면 스피드 1업
            Speed--;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /**
          PlayerHitC.Play("CenterHit");
          PlayerHitR.Play("RightHit");
          PlayerHitL.Play("LeftHit");
        **/
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            PlayerHitL.Play("LeftHit");
            Debug.Log($"왼쪽 어택");
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            PlayerHitR.Play("RightHit");
            Debug.Log($"오른쪽 어택");
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            PlayerHitC.Play("CenterHit");
            Debug.Log($"가운데 어택");
        }
    }

}

