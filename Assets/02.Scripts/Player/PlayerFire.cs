using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public List<GameObject> Bullets = null;
    public List<GameObject> SubBullets = null;
   

    private void Awake()
    {
        Bullets = new List<GameObject>(20);

        for (int i = 0; i < Bullets.Count; i++)
        {
            GameObject bullet = Instantiate(BulletPrefab);  // 예시: 프리팹을 이용한 Bullet 생성
            bullet.gameObject.SetActive(false);  // 처음에는 모든 총알을 비활성화 상태로 두기
            Bullets.Add(bullet);
        }


    }

    private void Start()
    {
        // 전처리 단계 : 코드가 컴파일(해석) 되기 전에 미리 처리되는 단계 
        // 전처리문 코드를 이용해서 미리 처리 되는 코드를 작성할 수 있다.
        // C#의 모든 전처리 코드는 '#'으로 시작한다. (#if, #elif, #endif)

#if UNITY_EDITOR || UNITY_STANDALONE
        GameObject.Find("Joystick canvas XYBZ").SetActive(false); // 만약 빌드시 에디터거나 유니티면 조이스틱 꺼버리겠다
#endif

#if UNITY_ANDROID
        Debug.Log("안드로이드 입니다.");
#endif

        Timer = 0f;
        AutoMode = false;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {

            AutoFire();

        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StopFire();
        }

        // 타이머 계산
        Timer -= Time.deltaTime;

        // 1. 타이머가 0보다 작은 상태에서 발사 버튼을 누르면
        bool ready = AutoMode || Input.GetKeyDown(KeyCode.Space);
        if (ready)
        {
            FireSource.Play();


            // 2. 프리팹으로부터 총알을 동적으로 만들고,
            // GameObject bulletA = Instantiate(BulletPrefab); // 인스턴트화 하는 함수, GameObject는 생략이 가능.
            // GameObject bulletB = Instantiate(BulletPrefab); // 인스턴트화 하는 함수, GameObject는 생략이 가능.

            // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.
            // bulletA.transform.position = MuzzleA.transform.position;
            // bulletB.transform.position = MuzzleB.transform.position;
            // 4. 0.6초의 딜레이가 생기게 만든다.
            //  nextTime = Time.time + coolTime;

            // 목표 : 총구 개수 만큼 총알을 만들고, 만든 총알의 위치를 각 총구의 위치로 바꾼다.
            Fire();
        }

        CurrentTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Bomb();
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

    private void Bomb()
    {

        if (CurrentTimer >= BoomcoolTime)
        {
            Debug.Log("필살기 작동");
            GameObject VFX = Instantiate(Boom);
            VFX.transform.position = Vector3.zero;
            CurrentTimer = 0;
        }

    }

    private void Fire()
    {
        if (Timer <= 0)
        {
            // 타이머 초기화
            Timer = coolTime;
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
    }

    private void AutoFire()
    {
        Debug.Log("자동 공격 모드");
        AutoMode = true;
    }

    private void StopFire()
    {
        Debug.Log("수동 공격 모드");
        AutoMode = false;
    }

    private void ChangeAuto()
    {
        if (AutoMode)
        {
            StopFire();
        }
        else
        {
            AutoFire();
        }
    }

    // 총알 발사
    public void OnClickXbutton()
    {
        Debug.Log("X버튼이 클릭되었습니다.");
        Fire();
    }
    // 자동 공격 on / off
    public void OnClickYbutton() 
    {
        ChangeAuto();
        Debug.Log("Y버튼이 클릭되었습니다.");
    }
    // 궁극기 사용
    public void OnClickBbutton() 
    {
        Debug.Log("B버튼이 클릭되었습니다.");
        Bomb();
    }
}




