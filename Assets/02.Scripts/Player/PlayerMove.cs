using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /**
       ��ǥ : �÷��̾ �̵��ϰ� �ʹ�. 
       �ʿ� �Ӽ� :
        - �̵� �ӵ�
       ���� : 
       1. Ű���� �Է��� �޴´�. 
       2. Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
       3. �̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
     **/

    public float Speed = 3f;  // �̵� �ӵ� : �ʴ� 3��ŭ �̵��ϰڴ�.
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
        // deltaTime�� 1��, ������ �� �ð� ������ ��ȯ�Ѵ�.
        // 30fps: d(��ŸŸ��)-> 0.03��
        // 60fps: d(��ŸŸ��)-> 0.016��

        // 1. Ű���� �Է��� �޴´�.
        // float h = Input.GetAxis("Horizontal"); // Ű���� �¿� �Է¿� ���� -1.0f ~ 0f ~ +1.0f ��ȯ
        // float v = Input.GetAxis("Vertical");
        float h = Input.GetAxisRaw("Horizontal"); // Ű���� �¿� �Է¿� ���� -1.0f, 0f, +1.0f ��ȯ
        float v = Input.GetAxisRaw("Vertical");   // Ű���� ���Ʒ� �Է¿� ���� -1.0f, 0f, +1.0f ��ȯ
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

        // 2. Ű���� �Է¿� ���� �̵��� ������ ����Ѵ�.
        // Vector2 dir = Vector2.right * h + Vector2.up * v;
        //                   (1,0) * h + (0,1) * v = (h, v)

        // ������ �� �������� ����
        Vector2 dir = new Vector2(h, v); // �̷��� ���� ������

        dir = dir.normalized;
        //  Debug.Log($"����ȭ �� : {dir.magnitude}");

        // �̵� ������ ����ȭ (������ ������ ���̸� 1�� ������� = �밢������ �̵��Ҷ��� �Ȱ��� ���ǵ�� �����̰�)
        dir = dir.normalized;
        //   Debug.Log($"����ȭ �� : {dir.magnitude}");

        // 3. �̵��� ����� �̵� �ӵ��� ���� �÷��̾ �̵���Ų��.
        // Debug.Log(Time.deltaTime);
        // transform.Translate( dir * Speed * Time.deltaTime );
        // ������ �̿��� �̵�
        // ���ο� ��ġ = ���� ��ġ + �ӵ� * �ð�

        Vector2 newPosition = transform.position + (Vector3)(dir * Speed) * Time.deltaTime;
        /**
        ���ο� ��ġ�� �� �����غ���. 
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

        transform.position = newPosition; // �÷��̾��� ��ġ = ���ο� ��ġ

        // 4. ���� ��ġ ���
        //    Debug.Log(transform.position);
        // transfrom.position = new Vector2 (dir.x,dir.y);

        // ��ǥ : Q/E ��ư�� ������ �ӷ��� �ٲٰ� �ʹ�.
        // �Ӽ� 
        // - �ӷ� (Speed)
        // ���� :

        // 1. Q/E ��ư �Է��� �Ǵ��Ѵ�
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 2. Q ��ư�� ���ȴٸ� ���ǵ� 1�ٿ�
            Speed++;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 3. E ��ư�� ���ȴٸ� ���ǵ� 1��
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
            Debug.Log($"���� ����");
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            PlayerHitR.Play("RightHit");
            Debug.Log($"������ ����");
        }
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            PlayerHitC.Play("CenterHit");
            Debug.Log($"��� ����");
        }
    }

}

