using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //ボスモンスターののPrefab
    public GameObject BossMonster;
    //外部のクラスからアクセス可能な変数 ... GameController.EnemyCounter
    static public int EnemyCounter;
    bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        //初期化
        EnemyCounter = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //キューブの生成
        if (EnemyCounter == 0)
        {
            Debug.Log("敵のカウンターが０になった");
            GameObject go = Instantiate(BossMonster);

        }
        
    }
}
