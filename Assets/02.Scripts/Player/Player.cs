using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Health = 2;
    public int hitcount = 0;
    public AudioSource PlayerAudioSource;


    private void Start()
    {
        /**
       // GetComponent<������Ʈ Ÿ��>(); -> ���� ������Ʈ�� ������Ʈ�� �������� �޼���
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
            PlayerAudioSource.Play();
    }
}