using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FairyController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //妖精にプレイヤーがアクセスしたら
        if (other.gameObject.tag == "Player")
        {
            GameController.FairyFlag = true;
        }
    }
}
