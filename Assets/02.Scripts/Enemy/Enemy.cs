
using UnityEngine;

public enum EnemyType //    Ÿ         
{
    Basic,
    Target,
    Follow,
}


public class Enemy : MonoBehaviour
{


    //   ǥ :
    // EnemyType.Basic Ÿ   -  Ʒ     ̵ 
    // EnemyType.Target Ÿ   - ó    ¾      ÷  ̾  ִ            ̵ 
    //  Ӽ 
    // - EnemyType Ÿ  
    //           :
    // 1.                     Ѵ  ( ÷  ̾  ִ      )
    // 2.              ̵    Ų  
    public EnemyType Etype;

    private Vector2 _dir;

    private GameObject _target;

    public GameObject ItemPrebs_health;
    public GameObject ItemPrebs_speed;

    public Animator MyAnimator;

    public GameObject ExplosionVFXprfab;

    public AudioSource EnemyAudioSource;


    public float Speed = 2.0f;
    public int health = 4;
    private void Start()
    {
        // ĳ   :                ͸             ҿ       صΰ   ʿ                     
        //            ÷  ̾ ã Ƽ      صд .
        _target = GameObject.Find("Player");

        MyAnimator = GetComponent<Animator>();

        if (Etype == EnemyType.Target)
        {
            // GetComponent<Player>();

            // 1.                     Ѵ  ( ÷  ̾  ִ      )

            // 1-1.  ÷  ̾ ã ´ 
            // GameObject target = GameObject.Find("Player"); //  ̸         ϴ     
                                                           // GameObject.FindGameObjectsWithTag("Player");  ±׷     ϴ     
            // 2.              ̵    Ų   (target - me)
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize(); //        ũ ⸦ 1        .

            // 1. 각도를 구한다.
            // tan@ = y/x   -> @ = y/x*atan
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            //  Debug.Log(radian); // 호도법 -> 라디안 값
            float degree = radian * Mathf.Rad2Deg;
            // Debug.Log(degree);

            // 2. 각도에 맞게 회전한다.
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90)); // 이미지 리소스에 맞게 90도를 뺀다.
            // transform.LookAt(_target.transform); - 이렇게 해도 되는데 쓰면 나중에 힘듦 / 3D에서 많이 씀
            //  transform.eulerAngles = new Vector3(0, 0, degree + 90); <- 이렇게 간단하게 써도 됨
        }

        else if (Etype == EnemyType.Follow)
        {
           // GameObject follow = GameObject.Find("Player");
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize(); //        ũ ⸦ 1        .
            float radian = Mathf.Atan2 (_dir.y, _dir.x);
            float degree = radian * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90));
        }

        else
        {
            _dir = Vector2.down;
            _dir.Normalize();
        }
        
    }


    void Update()
    {
        //   ǥ :       Ʒ     ̵    Ű    ʹ .
        //  Ӽ  :
        // -  ӷ 
        //          
        // 1.         ϱ 
        // Vector2 moveDir = Vector2.down;
        //       Ǯ   : Vector2 dir = new Vector2(0, -1)
        // 2.  ̵    Ų  
        transform.position += (Vector3)(_dir * Speed) * Time.deltaTime;

        if (Etype == EnemyType.Follow)
        {
           // GameObject follow = GameObject.Find("Player");
            _dir = _target.transform.position - this.transform.position;
            _dir.Normalize(); //        ũ ⸦ 1        .
            float radian = Mathf.Atan2(_dir.y, _dir.x);
            float degree = radian * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(new Vector3(0, 0, degree + 90));
        }

    }

    //   ǥ :  浹 ϸ        ÷  ̾      ϰ   ʹ .
    //           :
    // 1.    ࿡  浹    Ͼ  
    // 2.       ÷  ̾      Ѵ .

    //  浹    Ͼ   ȣ  Ǵ   ̺ Ʈ  Լ 
    private void OnCollisionEnter2D(Collision2D collision) 
    {

        if (collision.collider.tag == "Player")
        {
            //  ÷  ̾    ũ  Ʈ        ´ 

            Player player = collision.collider.GetComponent<Player>();
            Die();
            //  ÷  ̾  ü     -= 1
            player.MinHealth();
            Debug.Log($"플레이어 체력 : {player.GetHealth()}");
     
            PlayerMove playerMove = collision.collider.GetComponent<PlayerMove>();
            playerMove.RemoveSpeed(1);
            Debug.Log($"플레이어 스피드 : {player.GetHealth()}");
            //  ÷  ̾  ü        ٸ ..

        }
        else if (collision.collider.tag == "Bullet")
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            EnemyAudioSource.Play();
            if (bullet.BType == BulletType.Main)
            {
                health -= 2;
            }
            else if (bullet.BType == BulletType.Sub)
            {
                health -= 1;
            }
            if (health <= 0)
            {
                Die();
                Debug.Log("적 죽음");
                GameObject vfx = Instantiate(ExplosionVFXprfab);
                vfx.transform.position = this.transform.position;
                // 목표 : 50% 확률로 체력 올려주는 아이템, 50% 확률로 이동속도 올려주는 아이템
                MakeItems();
            }
            else
            {
                MyAnimator.Play("Hit");
            }
        }

        // public BulletType BType = BulletType.Main;
        /**
        if (hit != null)
        {
            hit.hitcount++;
            Debug.Log("Hit Count: " + hit.hitcount);

            if (collision.collider.tag == "Player" && hit.hitcount == 3)
                {
                    Debug.Log("Destroy Player");
                    Destroy(this.gameObject);
                }

            
            else if (collision.collider.tag == "Enemy")
            {
                Destroy(collision.collider.gameObject);
            }

        }
        **/

        // 2.  浹    ʿ            Ѵ 
        //    װ  (  )
        //  Destroy(collision.collider.gameObject);
        //        (    ڽ )
        // Destroy(this.gameObject);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //  浹          Ź 
        Debug.Log("Stay");
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //  浹            
        Debug.Log("Exit");
    }
    // 1. 만약에 적을 잡으면?
    public void Die()
    {
        // 1. 만약에 적을 잡으면?
            // 나죽자
            Destroy(this.gameObject);
            GameObject vfx = Instantiate(ExplosionVFXprfab);
            vfx.transform.position = this.transform.position;

            // 목표: 스코어를 증가시키고 싶다.
            // 1. 씬에서 ScoreManager 게임 오브젝트를 찾아온다.
            GameObject smGameObject = GameObject.Find("ScoreManager");
            // 2. ScoreManager 게임 오브젝트에서 ScoreManager 스크립트 컴포넌트를 얻어온다.
            ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
             // 3. 컴포넌트의 Score 속성을 증가시킨다.
            int scoreCount = scoreManager.GetScoreCount();
            scoreManager.AddScore();
            Debug.Log(scoreManager.GetScoreCount());
    }

    public void MakeItems()
    {
        if (UnityEngine.Random.Range(0, 2) == 0)
        {
            // 아이템 만들고
            GameObject itemH = Instantiate(ItemPrebs_health);
            // 위치를 나의 위치로 수정
            itemH.transform.position = transform.position;
        }
        else
        {
            // 이동속도 올려주는 아이템 만들고
            GameObject itemS = Instantiate(ItemPrebs_speed);
            // 위치를 나의 위치로 수정
            itemS.transform.position = transform.position;
        }
    }
}

