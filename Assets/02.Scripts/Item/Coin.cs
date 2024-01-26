using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 충돌을 시작했을 때
        Debug.Log("Enter");
        // 나죽자 (나 자신)
        Destroy(this.gameObject);
    }

}
