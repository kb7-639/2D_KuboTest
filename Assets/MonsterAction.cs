using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    //敵に剣が何回接触したかのカウンター
    private int enemyCounter = 0;

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
        //敵に武器があたったら
        if (other.gameObject.tag == "Player")
        {
            this.enemyCounter += 1;
            Debug.Log("プレイヤーに切られた" + enemyCounter);
            GetComponent<Animator>().SetTrigger("Hit");
        }
    }
}
