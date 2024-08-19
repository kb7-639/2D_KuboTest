using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    //敵に剣が何回接触したかのカウンター
    private int enemyCounter = 0;
    //爆発
    public GameObject Bakuhatsu;
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


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //n秒動いたら反転させる
        if(TimeCounter > FlipTime)
        {
            //クリアー
            TimeCounter = 0f;

            //反転
            isFlip = !isFlip;
        }

        //isFlipで左右移動
        if(isFlip)
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //敵に武器があたったら
        if (other.gameObject.tag == "Player")
        {
            this.enemyCounter += 1;
            //Debug.Log("プレイヤーに切られた" + enemyCounter);
            GetComponent<Animator>().SetTrigger("Hit");

            //3回で破棄
            if(this.enemyCounter >= 1)
            {

                //爆発マーク
                GameObject _object = Instantiate(Bakuhatsu);

                //ポジション補正
                _object.transform.position = this.gameObject.transform.position; 

                //タイマー破棄
                Destroy( _object, 0.1f);

                Destroy(this.gameObject);

                GameController.EnemyCounter -= 1;
            }
        }
    }
}
