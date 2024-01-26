using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum MyType //    Ÿ         
{
    healthUp,
    speedUp
}
public class Item : MonoBehaviour
{
    public float _timer = 0f; // 시간을 체크할 변수
    public float _timerchecker = 0f;
    public float _eatTime = 1f;
    public float Speed = 10f;
    private const float _followTime = 3f;
    private Vector2 _dir;

    public MyType MyType;
    public Animator MyAnimator;
    public AudioSource itemAudioH;
    public AudioSource itemAudioS;
    public GameObject ItemPreb;

    private void Start()
    {
        _timer = 0f;

        MyAnimator = GetComponent<Animator>();
        MyAnimator.SetInteger("ItemType", (int)MyType);

    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player") && !otherCollider.CompareTag("Bullet"))
        {
            if (MyType == MyType.healthUp)
            {
                itemAudioH.Play();
            }
            else if (MyType == MyType.speedUp)
            {
                itemAudioS.Play();
            }
        }
            else if (otherCollider.CompareTag("Bullet"))
        {

        }
        /**
        // 목표 : 플레이어의 체력을 올리고 싶다.
        // 순서 :
        // 1. 플레이어 스크립트 받아오기
        GameObject playerGameObject = GameObject.Find("Player");
        Player player = playerGameObject.GetComponent<Player>();
        Player player = otherCollider.gameObject.GetComponent<Player>(); <- 이렇게 해도 됨
        // 2. 플레이어 체력 올리기
        player.Health++;
        Debug.Log($"현재 플레이어의 체력 : {player.Health}");
        Player player = otherCollider.gameObject.GetComponent<Player>();
        player.Health++;
        Debug.Log($"플레이어 체력 : {player.Health}");

        **/
    }
    private void Update()
    {
        _timerchecker += Time.deltaTime;

        if (_timerchecker >= _followTime)
        {
            // 1. 플레이어를 찾고
            GameObject playerObject = GameObject.Find("Player");
            // 2. 방향을 정하고
            _dir = playerObject.transform.position - transform.position;
            _dir.Normalize();
            // 3. 스피드에 맞게 이동
            Vector2 newPosition = transform.position + (Vector3)(_dir * Speed) * Time.deltaTime;
            transform.position = newPosition;
        }
    }



    private void OnTriggerStay2D(Collider2D otherCollider)
    {
        _timer += Time.deltaTime;

        if (_timer >= _eatTime)
        {
            
            if (MyType == MyType.healthUp) 
            {
                Player player = otherCollider.gameObject.GetComponent<Player>();
                player.Health++;
                Debug.Log($"플레이어 체력 : {player.Health}");

            }
            else if (MyType == MyType.speedUp)
            {
                // 타입이 1이면 플레이어의 스피드 올려주기
                PlayerMove playerMove = otherCollider.gameObject.GetComponent<PlayerMove>();
                playerMove.Speed++;
                Debug.Log($"플레이어 스피드 : {playerMove.Speed}");

            }
            Destroy(this.gameObject);
            GameObject VFX = Instantiate(ItemPreb);
            VFX.transform.position = otherCollider.transform.position;
        }
    }
    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        _timer = 0.0f;
        Debug.Log("트리거 종료");
    }


}