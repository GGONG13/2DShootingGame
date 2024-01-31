using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public int _health = 2;
    public int hitcount = 0;
    public AudioSource PlayerAudioSource;


    private void Start()
    {
        /**
       // GetComponent<컴포넌트 타입>(); -> 게임 오브젝트의 컴포넌트를 가져오는 메서드
       SpriteRenderer sr = GetComponent<SpriteRenderer>();
       sr.color = Color.white;

        // Transform tr = GetComponent<Transform>();
        // tr.position = new Vector2(0f, -4.0f);
        transform.position = new Vector2(0f, -4.0f);

        PlayerMove playerMove = GetComponent<PlayerMove>();
        Debug.Log(playerMove.Speed);
        playerMove.Speed = 4.5f;
        **/
    }

    public int GetHealth()
    {
        return _health;
    }

    public void AddHealth()
    {
        SetHealth(_health + 1);
    }

    public void MinHealth()
    {
        SetHealth(_health - 1);

        if (_health <= 0)
        {
            Destroy(gameObject);
            Debug.Log($"게임 종료");
        }
    }

    public void SetHealth(int Health)
    {
        _health = Health;

        if (Health < 0)
        {
            _health = 0;
            return;
        }

    }

        private void OnCollisionEnter2D(Collision2D collision)
    {
            PlayerAudioSource.Play();
    }
}