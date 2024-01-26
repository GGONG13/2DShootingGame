using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Purchasing;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{

    // ���� : �����ð����� ���� ���������κ��� �����ؼ� �� ��ġ�� ���� ���� �ʹ�.
    // �ʿ� �Ӽ�
    // - �� ������
    // - ���� �ð�
    // - ���� �ð�
    // ���� ���� :

    public GameObject EnemyPrefab_basic;
    public GameObject EnemyPrefab_target;
    public GameObject EnemyPrefab_follow;



    public float SpawnTime = 1.2f;
    public float CurrentTimer = 0;


    /**
    [Header ("Ÿ�̸�")]
    public float Timer = 10f;
    public float coolTime = 0.6f;

    [Header("�� ������")]
    public GameObject EnemyPrefab;

    [Header("�� ���� ��ġ")]
    public GameObject[] EnemyPosition;

    [Header("�� ���� ���")]
    public bool AutoMode = true;
    **/

    // ��ǥ : �� ���� �ð��� �����ϰ� �ϰ� �ʹ�.
    // �ʿ� �Ӽ� :
    // - �ּ� �ð�
    // - �ִ� �ð�
    public float MinTime = 0.1f;
    public float MaxTime = 10.0f;

    private void Start()
    {
        // ������ �� �� ���� �ð��� �����ϰ� �����Ѵ�
        SetRendomTime();
    }

    private void SetRendomTime()
    {
        SpawnTime = Random.Range(MinTime, MaxTime);
    }

    void Update()
    {
        // 1. �ð��� �帣�ٰ� 
        CurrentTimer += Time.deltaTime;



        // 2. ���࿡ �ð��� �����ð��� �Ǹ�
        if (CurrentTimer >= SpawnTime)
        {
            // Ÿ�̸� �ʱ�ȭ
            CurrentTimer = 0f;

            SetRendomTime();

            // 30% Ȯ���� Target��, ������ Ȯ�� Basic�� �� �����ϰ� �ϱ�
            GameObject enemy = null;
            int randomNumber = Random.Range(0, 10);

            if (randomNumber < 1)
            {
                enemy = Instantiate(EnemyPrefab_follow);
            }

            else if (randomNumber < 4)
            {
                enemy = Instantiate(EnemyPrefab_target);
            }

            else
            {
                enemy = Instantiate(EnemyPrefab_basic);
            }

            enemy.transform.position = this.transform.position;
        }
        

        /**
        if (Timer <= 0)
        {
            Timer = coolTime;
         //   AutoMode = true;
         //   Debug.Log("�� ���� ���");

            for (int i = 0; i < EnemyPosition.Length; i++)
            {
                GameObject enemy = Instantiate(EnemyPrefab);
                enemy.transform.position = EnemyPosition[i].transform.position;
             


                /**
                GameObject enemyPre = Instantiate(EnemyPrefab);
                Enemy enemy = enemyPre.GetComponent<Enemy>();
                Vector2 moveDir = Vector2.down;
                enemyPre.transform.position += (Vector3)(moveDir * enemy.Speed) * Time.deltaTime;
                **/
    }
 }

    /**
        else if (Time.deltaTime > 0f) 
        {
            AutoMode = true;
            Debug.Log("�� ��� ���");
        }
        **/

