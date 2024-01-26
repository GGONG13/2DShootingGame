using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Purchasing;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{

    // 역할 : 일정시간마다 적을 프리펩으로부터 생성해서 내 위치에 갖다 놓고 싶다.
    // 필요 속성
    // - 적 프리펩
    // - 일정 시간
    // - 현재 시간
    // 구현 순서 :

    public GameObject EnemyPrefab_basic;
    public GameObject EnemyPrefab_target;
    public GameObject EnemyPrefab_follow;



    public float SpawnTime = 1.2f;
    public float CurrentTimer = 0;


    /**
    [Header ("타이머")]
    public float Timer = 10f;
    public float coolTime = 0.6f;

    [Header("적 프리팹")]
    public GameObject EnemyPrefab;

    [Header("적 생성 위치")]
    public GameObject[] EnemyPosition;

    [Header("적 공격 모드")]
    public bool AutoMode = true;
    **/

    // 목표 : 적 생성 시간을 랜덤하게 하고 싶다.
    // 필요 속성 :
    // - 최소 시간
    // - 최대 시간
    public float MinTime = 0.1f;
    public float MaxTime = 10.0f;

    private void Start()
    {
        // 시작할 때 적 생성 시간을 랜덤하게 설정한다
        SetRendomTime();
    }

    private void SetRendomTime()
    {
        SpawnTime = Random.Range(MinTime, MaxTime);
    }

    void Update()
    {
        // 1. 시간이 흐르다가 
        CurrentTimer += Time.deltaTime;



        // 2. 만약에 시간이 일정시간이 되면
        if (CurrentTimer >= SpawnTime)
        {
            // 타이머 초기화
            CurrentTimer = 0f;

            SetRendomTime();

            // 30% 확률로 Target형, 나머지 확률 Basic형 적 생성하게 하기
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
         //   Debug.Log("적 공격 모드");

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
            Debug.Log("적 대기 모드");
        }
        **/

