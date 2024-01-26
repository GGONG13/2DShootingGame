using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerFire : MonoBehaviour
{
    // [�Ѿ� �߻� ����]
    // ��ǥ : �Ѿ��� ���� �߻��ϰ� �ʹ�.
    // �Ӽ� : 
    // - �Ѿ� ������
    // - �ѱ�
    // ���� ����
    // 1. �߻� ��ư�� ������ (�����̽�)
    // 2. ���������κ��� �Ѿ��� �������� �����,
    // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
    // 4. ��ư�� �� �� ������ ��, 0.6���� �����̰� ����� �����.
    // 5. �Ѿ� ���ʿ� 2�� �߻��ϱ� -> muzzle ����� �������� bullet �ΰ��� �����. -> muzzle ������ ���� bullet ������ �߰� -> Vector2 
    // 6. 1�� Ű�� �ڵ� ���� ��� / 2�� Ű �� ���� ���� ���

    [Header("�Ѿ� ������")]
    public GameObject BulletPrefab; // �Ѿ� ������
    [Header("�ѱ���")]
    public GameObject[] Muzzles;    // �ѱ���
    [Header("���� �Ѿ� ������")]
    public GameObject MuzzlePrefab; // ���� �Ѿ� ������
    [Header("���� �Ѿ�")]
    public GameObject[] SubMuzzles; // ���� �Ѿ�

    [Header("Ÿ�̸�")]
    public float Speed = 1.0f;
    public float Timer = 10f;
    public float coolTime = 0.6f;
    public float CurrentTimer = 0;
    public float BoomcoolTime = 5.0f;

    [Header("�ڵ� ���")]
    public bool AutoMode = false;

    [Header("����� Ŭ��")]
    public AudioSource FireSource;

    [Header("�ʻ��")]
    public GameObject Boom;

    private void Start()
    {
        Timer = 0f;
        AutoMode = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("�ڵ� ���� ���");
            AutoMode = true;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("���� ���� ���");
            AutoMode = false;
        }

        // Ÿ�̸� ���
        Timer -= Time.deltaTime;

        // 1. Ÿ�̸Ӱ� 0���� ���� ���¿��� �߻� ��ư�� ������
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            FireSource.Play();
            // Ÿ�̸� �ʱ�ȭ
            Timer = coolTime;

            // 2. ���������κ��� �Ѿ��� �������� �����,
            // GameObject bulletA = Instantiate(BulletPrefab); // �ν���Ʈȭ �ϴ� �Լ�, GameObject�� ������ ����.
            // GameObject bulletB = Instantiate(BulletPrefab); // �ν���Ʈȭ �ϴ� �Լ�, GameObject�� ������ ����.

            // 3. ���� �Ѿ��� ��ġ�� �ѱ��� ��ġ�� �ٲ۴�.
            // bulletA.transform.position = MuzzleA.transform.position;
            // bulletB.transform.position = MuzzleB.transform.position;
            // 4. 0.6���� �����̰� ����� �����.
            //  nextTime = Time.time + coolTime;

            // ��ǥ : �ѱ� ���� ��ŭ �Ѿ��� �����, ���� �Ѿ��� ��ġ�� �� �ѱ��� ��ġ�� �ٲ۴�.
            for (int i = 0; i < Muzzles.Length; i++)
            {
                // 1. �Ѿ��� �����
                GameObject bullet = Instantiate(BulletPrefab);

                // 2. ��ġ�� �����Ѵ�.
                bullet.transform.position = Muzzles[i].transform.position;
            }
            for (int i = 0; i < SubMuzzles.Length; i++)
            {
                GameObject Subbullets = Instantiate(MuzzlePrefab);
                Subbullets.transform.position = SubMuzzles[i].transform.position;
            }
        }

        CurrentTimer += Time.deltaTime;

        if (CurrentTimer >= BoomcoolTime && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("�ʻ�� �۵�");
            GameObject VFX = Instantiate(Boom);
            VFX.transform.position = Vector3.zero;
            CurrentTimer = 0;
        }

        /**
        Vector2 BulletPositionA;
        Vector2 BulletPositionB;

        Vector2 muzzlePosition = Muzzles.transform.position;
        Vector2 bulletOffSet = new Vector2 (1,0);

        BulletPositionA = muzzlePosition + bulletOffSet;
        BulletPositionB = muzzlePosition - bulletOffSet;
        **/



    }


    }




