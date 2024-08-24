using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;
        private Animator animator;

        //敵に剣が何回接触したかのカウンター
        private float enemyCounter = 1f;

        //スピードアイテム初期値
        private int item_Speed = 1;

        /*------インスペクタからオブジェクトを取得------*/

        //Hpゲージのオブジェクトをインスペクターから取得
        public GameObject HpGauge;
        //HPゲージの高さ調整（X軸）の値をインスペクターから取得
        public float HpPosY;
        //HPゲージの位置調整（Y軸）の値をインスペクターから取得
        public float HpPosX;
        //Playerの最大体力
        public float PlayerHitPoints = 3;
        //生成されたPrefabを格納するための変数
        private GameObject PlayerHpGauge;
        //ゲームオーバーPrefabをインスペクターから取得
        public GameObject GameOver;


        /*------プレイヤーHp関連------*/

        //Hpゲージの子要素（Gauge）を取得するための変数
        private GameObject HpGaugeChild;

        //Hpゲージ用に体力を分割するための変数（1/最大体力）
        private float PlayerHitPointsSplit;
        //ゲージの最大値（オブジェクトの横幅）
        private float PlayerHitPointsMax;
        //Playerの死亡判定
        private bool PlayerDeathFlag = true;


        private void Start()
        {
            //取得したHpゲージのPrefabの子要素（Gauge）を取得
            this.HpGaugeChild = HpGauge.transform.Find("Gauge").gameObject;
            //子要素のインスペクター上の横幅を取得
            this.PlayerHitPointsMax = HpGaugeChild.transform.localScale.x;


            //アニメーターを取得
            animator = GetComponent<Animator>();

            //Hpゲージ用にGaugeの横幅をPlayerのHPで割る
            this.PlayerHitPointsSplit = PlayerHitPointsMax / this.PlayerHitPoints;
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //左移動
                dir.x = -1;
                animator.SetInteger("Direction", 3);
                this.transform.localScale = new Vector2(-1.0f, 1.0f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                //右移動
                dir.x = 1;
                animator.SetInteger("Direction", 2);
                this.transform.localScale = new Vector2(1.0f, 1.0f);
            }

            if (Input.GetKey(KeyCode.UpArrow))
            {
                //下移動
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                //上移動
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            //攻撃
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Amnimationコンポーネントを取得し、トリガーをtrueにする
                GetComponent<Animator>().SetTrigger("Attack");
            }

            dir.Normalize();
            animator.SetBool("IsMoving", dir.x != 0);
            //スピードアイテムを取得したら、スピードの値をかける
            GetComponent<Rigidbody2D>().velocity = speed * dir*this.item_Speed;

            //HPゲージの位置をキャラクターに追従するように更新
            HpGauge.transform.position = new Vector2(this.gameObject.transform.position.x + HpPosX, this.gameObject.transform.position.y + HpPosY);
            //ゲージのバーの長さを小さくする
            HpGaugeChild.transform.localScale = new Vector2(PlayerHitPointsMax, 1f);

            //体力が3割以下になったらHPゲージの色を赤色にする
            if (PlayerHitPointsMax < 0.3)
            {
                Debug.Log("体力が0.3以下になった");
                HpGaugeChild.GetComponent<SpriteRenderer>().color = Color.red;
            }
            else{
                HpGaugeChild.GetComponent<SpriteRenderer>().color = Color.green;
            }

            //プレイヤー死亡時
            if (PlayerHitPointsMax <= 0 && PlayerDeathFlag)
            {
                //SampleSceneのときの処理
                if (SceneManager.GetActiveScene().name == "SampleScene")
                {
                    PlayerDeathFlag = false;

                    //ゲームオーバーPrefabを表示
                    GameOver.gameObject.SetActive(true);
                    //プレイヤーのDeathアニメーションを再生
                    GetComponent<Animator>().SetTrigger("Death");
                    //ゲームオーバー演出を再生
                    GameOver.GetComponent<Animator>().SetTrigger("GameOver");
                    //ゲームオーバーになってから二秒後にプレイヤーを消す
                    Invoke("Destroy", 2f);
                    //1秒後にタイトルに遷移する
                }

                //チュートリアルのときの処理
                if (SceneManager.GetActiveScene().name == "Tutorial")
                {
                    //プレイヤーのHPを満タンにする
                    PlayerHitPointsMax = 1;
                }
            }
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            //敵に武器があたったら
            if (other.gameObject.tag == "Enemy")
            {
                //Playerがアクション中か？
                bool flag = this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");
                Debug.Log("攻撃中" + flag);

                //通常ならダメージを受ける
                if (flag == false)
                {
                    GetComponent<Animator>().SetTrigger("Damage");
                    Debug.Log("あたった");
                    //分割したHPの値を、現在のHPから引く
                    PlayerHitPointsMax -= PlayerHitPointsSplit;
                }
            }

            //回復アイテムと接触したら
            if (other.gameObject.tag == "Item_Heart")
            {
                //プレイヤーのHPを満タンにする
                PlayerHitPointsMax = 1;
            }

            //スピードアップアイテムと接触したら
            if (other.gameObject.tag == "Item_Speed")
            {
                //プレイヤーのスピードにかける
                this.item_Speed = 2;
            }
        }

        //プレイヤーの死亡時に自身のオブジェクト削除&シーン移動
        void Destroy()
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Title");
            Debug.Log("消えた時間" + Time.time);
        }

        //特定のエリアに侵入したら
        void OnTriggerEnter2D(Collider2D other)
        {
            //チュートリアル時に攻撃チュートリアルのエリアに侵入した
            if (other.gameObject.tag == "TriggerTutorialArrow")
            {
                TutorialController.TutorialArea = true;
            }
            //ミッションを表示するエリアに侵入した
            if (other.gameObject.tag == "TriggerMission")
            {
                GameController.TriggerMissionFlag = true;
            }

            //ボス付近のエリアに侵入した
            if (other.gameObject.tag == "TriggerBossBattle")
            {
                GameController.TriggerBossBattleFlag = true;
            }
        }

    }
}
