using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
    //敵に剣が何回接触したかのカウンター
    private int enemyCounter = 0;

    //移動させるコンポーネントを入れる
    private Rigidbody2D myRigidbody;
    //移動量
    private float velocity = 1.0f;
    //フリップ
    private bool isFlip = false;
    //汎用タイマー
    private float TimeCounter = 0f;
    //反転までの時間
    public float FlipTime = 1.5f;

    /*------インスペクタからオブジェクトを取得------*/
    //爆発
    public GameObject Bakuhatsu;

    //Hpゲージのオブジェクトを取得
    public GameObject HpGauge;

    /*------モンスターHp関連------*/
    //Hpゲージの子要素（Gauge）を取得するための変数
    private GameObject HpGaugeChild;
    //最大体力
    public float PlayerHitPoints = 5;
    //Hpゲージ用に体力を分割するための変数（1/最大体力）
    private float PlayerHitPointsSplit;
    //ゲージの最大値（オブジェクトの横幅）
    private float PlayerHitPointsMax;

    // Start is called before the first frame update
    void Start()
    {

        //取得したHpゲージのPrefabの子要素（Gauge）を取得
        this.HpGaugeChild = HpGauge.transform.Find("Gauge").gameObject;
        //子要素のインスペクター上の横幅を取得
        this.PlayerHitPointsMax = HpGaugeChild.transform.localScale.x;

        //初期化
        HpGaugeChild.transform.localScale = new Vector2(1f, 1f);

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //ゲージをPlayerのHPで割る
        this.PlayerHitPointsSplit = 1 / this.PlayerHitPoints;

    }

    // Update is called once per frame
    void Update()
    {
        //n秒動いたら反転させる
        if (TimeCounter > FlipTime)
        {
            //クリアー
            TimeCounter = 0f;
            //反転
            isFlip = !isFlip;
        }

        //isFlipで左右移動
        if (isFlip)
        {
            //右へ移動
            GetComponent<SpriteRenderer>().flipX = true;
            this.myRigidbody.velocity = new Vector2(velocity, this.myRigidbody.velocity.y);
        }
        else
        {
            //左へ移動
            GetComponent<SpriteRenderer>().flipX = false;
            this.myRigidbody.velocity = new Vector2(-velocity, this.myRigidbody.velocity.y);
        }
        //タイマー加算
        TimeCounter += Time.deltaTime;

        //HPゲージの位置をキャラクターに追従するように更新
        HpGaugeChild.transform.localScale = new Vector2(PlayerHitPointsMax, 1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //敵に武器があたったら
        if (other.gameObject.tag == "Player")
        {
            //Playerがアタックアニメーション中か？
            bool flag = other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");
            Debug.Log("攻撃中" + flag);

            //攻撃中だった？
            if (flag)
            {
                this.enemyCounter += 1;
                //Debug.Log("プレイヤーに切られた" + enemyCounter);
                GetComponent<Animator>().SetTrigger("Hit");

                //3回で破棄
                if (this.enemyCounter >= PlayerHitPoints)
                {
                    //爆発マーク
                    GameObject _object = Instantiate(Bakuhatsu);
                    //ポジション補正
                    _object.transform.position = this.gameObject.transform.position;
                    //タイマー破棄
                    Destroy(_object, 0.1f);

                    Destroy(this.gameObject);

                    GameController.BossDeathFlag = true;
                }
                PlayerHitPointsMax -= PlayerHitPointsSplit;
            }
        }
    }
}