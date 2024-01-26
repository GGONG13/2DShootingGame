using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType  // �Ѿ� Ÿ�Կ� ���� ������ (����� ����ϱ� ���� �׷�ȭ�ϴ� ��)
{
    Main = 0,
    Sub = 1,
    Pet = 2
}



public class Bullet : MonoBehaviour
{

    // public int BulletType = 0; // 0�̸� ���Ѿ�, 1�̸� �����Ѿ�, 2�� ���� ��� �Ѿ�
    public BulletType BType = BulletType.Main;

    // ��ǥ : �Ѿ��� ���� ��� �̵��ϰ� �ʹ�.
    // �Ӽ� : 
    // - �ӷ� (�̵��ӵ�)
    // ���� ����
    // 1. �̵��� ������ ���Ѵ�.
    // 2. �̵��Ѵ�.

    public float Speed = 1.0f;



    // Update is called once per frame
    void Update()
    {

        // 1. �̵��� ������ ���Ѵ�.
        Vector2 moveDirection = Vector2.up;
        // 2. �̵��Ѵ�.
        // transform.Translate(moveDirection * Speed * Time.deltaTime);
        // ���ο� ��ġ = ���� ��ġ * �ӵ� * �ð�
        transform.position += (Vector3)(moveDirection * Speed) * Time.deltaTime;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // �浹�� �������� ��
        Debug.Log("Enter");
        // 2. ���� �÷��̾ �����Ѵ�
        // ���װ� (��)
        // Destroy(collision.collider.gameObject);
        // ������ (�� �ڽ�)
        Destroy(this.gameObject);
    }


}
