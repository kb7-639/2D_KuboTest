using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    //ボスモンスターののPrefab
    public GameObject BossMonster;

    //ミッションテキスト表示のPrefab
    public GameObject MissionUIText;

    //ミッション自体のUI
    public GameObject MissionUI;

    //ミッションクリアニメーション
    public GameObject MissionClearAnim;

    //ボス出現アニメーション
    public GameObject BossBattle;
    

    //敵の撃破数のカウンター
    //外部のクラスからアクセス可能な変数 ... GameController.EnemyCounter
    static public int EnemyCounter;

    //敵の撃破数の初期値
    private int Enemydefault = 3;

    //ボスを生成するフラグ
    private bool BossFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        //初期化
        EnemyCounter = this.Enemydefault;
    }

    // Update is called once per frame
    void Update()
    {
        //敵の撃破数の初期値分ループする
        if(BossFlag)
        {
            //ミッションUIのテキストと、残りの敵数を表示
            MissionUIText.GetComponent<TextMeshProUGUI>().text = "ステージ内のモンスターを全て倒そう（" + EnemyCounter + "/" + Enemydefault + "）";
        }
       

        //EnemyCounterがゼロ＆BossFlagがtrueなら生成する。
        if (EnemyCounter == 0 && BossFlag)
        {
            Debug.Log("敵のカウンターが０になった");
            
            //ボスを生成する
            GameObject go = Instantiate(BossMonster);
            go.transform.position = new Vector2(-1.76f, 15.05f);

            //ボスを生成したのでfalseにする
            BossFlag = false;

            //ミッションUIを破棄
            Destroy(MissionUI);

        }
    }
}
