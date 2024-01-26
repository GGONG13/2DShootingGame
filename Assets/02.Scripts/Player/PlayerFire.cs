using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerFire : MonoBehaviour
{
    // [총알 발사 제작]
    // 목표 : 총알을 만들어서 발사하고 싶다.
    // 속성 : 
    // - 총알 프리팹
    // - 총구
    // 구현 순서
    // 1. 발사 버튼을 누르면 (스페이스)
    // 2. 프리팹으로부터 총알을 동적으로 만들고,
    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
    // 4. 버튼을 한 번 눌렀을 때, 0.6초의 딜레이가 생기게 만든다.
    // 5. 총알 양쪽에 2개 발사하기 -> muzzle 가운데를 기점으로 bullet 두개를 만든다. -> muzzle 포지션 값에 bullet 포지션 추가 -> Vector2 
    // 6. 1번 키→ 자동 공격 모드 / 2번 키 → 수동 공격 모드

    [Header("총알 프리팹")]
    public GameObject BulletPrefab; // 총알 프리랩
    [Header("총구들")]
    public GameObject[] Muzzles;    // 총구들
    [Header("보조 총알 프리팹")]
    public GameObject MuzzlePrefab; // 보조 총알 프리랩
    [Header("보조 총알")]
    public GameObject[] SubMuzzles; // 보조 총알

    [Header("타이머")]
    public float Speed = 1.0f;
    public float Timer = 10f;
    public float coolTime = 0.6f;
    public float CurrentTimer = 0;
    public float BoomcoolTime = 5.0f;

    [Header("자동 모드")]
    public bool AutoMode = false;

    [Header("오디오 클립")]
    public AudioSource FireSource;

    [Header("필살기")]
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
            Debug.Log("자동 공격 모드");
            AutoMode = true;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("수동 공격 모드");
            AutoMode = false;
        }

        // 타이머 계산
        Timer -= Time.deltaTime;

        // 1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (Timer <= 0 && ready)
        {
            FireSource.Play();
            // 타이머 초기화
            Timer = coolTime;

            // 2. 프리팹으로부터 총알을 동적으로 만들고,
            // GameObject bulletA = Instantiate(BulletPrefab); // 인스턴트화 하는 함수, GameObject는 생략이 가능.
            // GameObject bulletB = Instantiate(BulletPrefab); // 인스턴트화 하는 함수, GameObject는 생략이 가능.

            // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
            // bulletA.transform.position = MuzzleA.transform.position;
            // bulletB.transform.position = MuzzleB.transform.position;
            // 4. 0.6초의 딜레이가 생기게 만든다.
            //  nextTime = Time.time + coolTime;

            // 목표 : 총구 개수 만큼 총알을 만들고, 만든 총알의 위치를 각 총구의 위치로 바꾼다.
            for (int i = 0; i < Muzzles.Length; i++)
            {
                // 1. 총알을 만들고
                GameObject bullet = Instantiate(BulletPrefab);

                // 2. 위치를 설정한다.
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
            Debug.Log("필살기 작동");
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




