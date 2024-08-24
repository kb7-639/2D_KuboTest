using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    //ボスモンスターののPrefab
    public GameObject BossMonster;

    //ミッションテキスト表示のPrefab
    public GameObject MissionUIText;

    //ミッション自体のUI
    public GameObject MissionUI;

    //ミッションUIのタイトル
    public GameObject MissionUITitle;

    //ゲームクリア演出
    public GameObject AnimGameCler;

    //ミッションクリアの演出
    public GameObject AnimMissionClear;

    //障害物１のオブジェクト
    public GameObject Obstacle01;

    //障害物２のオブジェクト
    public GameObject Obstacle02;

    //イベントトークUIのオブジェクト
    public GameObject EventTalk;

    //イベントトークUIテキスト
    public GameObject EventTalkTxt;

    //ダイアログオブジェクト
    public GameObject Dialog;

    //ダイアログのUIテキスト
    public GameObject DialogTxt;

    //ダイアログの見出し
    public GameObject DialogTitle;

    //ダイアログの画像
    public GameObject DialogImage;

    //Mission説明用のコライダー
    public GameObject TriggerMission;

    //ボスバトルエリアに接近した際の演出表示用のコライダー
    public GameObject TriggerBossBattle;

    //ボスバトル演出用ののPrefab
    public GameObject AnimBossBattle;



    //敵の撃破数のカウンター
    //外部のクラスからアクセス可能な変数 ... GameController.EnemyCounter
    static public int EnemyCounter;

    //敵の撃破数の初期値
    private int Enemydefault = 6;

    //ボスを生成するフラグ
    private bool BossFlag = true;

    //ボスの死亡フラグ
    static public bool BossDeathFlag = false;

    //精霊にアクセスするためのフラグ
    static public bool FairyFlag = false;

    //特定のエリア（ミッションエリア）に接触したフラグ
    static public bool TriggerMissionFlag = false;

    //ボタンが押されたか
    static public bool CloseBtnFlag = false;

    private bool CloseBtnFlag02 = false;

    //特定のエリア（ボスバトル）に接触したフラグ
    static public bool TriggerBossBattleFlag = false;




    // Start is called before the first frame update
    void Start()
    {
        //初期化
        EnemyCounter = this.Enemydefault;

        DialogOpen("囚われている妖精を助けよう", "悪いモンスターによって優しい妖精が囚われてしまいました。奥の部屋まで救出に向かいましょう。", "tutorial01");
        //ボタンのフラグを初期化


    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerMissionFlag)
        {
            CloseBtnFlag = false;
            Debug.Log("ミッションフラグは" + CloseBtnFlag);
            Camera.main.GetComponent<Animator>().SetTrigger("Event01");

            //2秒後に実行する
            StartCoroutine(DelayMethod(2f, () =>
            {
                DialogOpen("ステージ内のモンスターを倒そう", "モンスター達によって道が閉じています。モンスターを全て倒して扉を開きましょう。", "tutorial02");
            }));

            Destroy(TriggerMission);
            TriggerMissionFlag = false;
            Debug.Log("無限ループ？");
            Debug.Log("ミッションフラグは" + CloseBtnFlag);
            this.CloseBtnFlag02 = true;

        }
        //ダイアログののOKボタンが押されたらミッションUIを表示
        if (CloseBtnFlag && CloseBtnFlag02)
        {
            MissionUIOpen("扉を開くために", "ステージ内のモンスターを全て倒そう（" + EnemyCounter + "/" + Enemydefault + "）");
            CloseBtnFlag = false;
            Debug.Log("ミッションUIの生成");
        }

        if (BossFlag)
        {
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

            //ミッションUIを非表示
            MissionUIClose();

            //新しいミッションUIを表示
            MissionUIOpen("ボスを倒そう！", "奥にいるボスを倒しに行こう");

            //障害物消滅演出
            Obstacle01.GetComponent<Animator>().SetTrigger("Obstacle01");

            EventTalkWindwOpen("奥の部屋へ進めるようになったようです");
            Invoke("EventTalkWindowClose", 3f);
        }
        //ボスバトルエリアに接近した際の演出表示
        if (TriggerBossBattleFlag)
        {
            Destroy(TriggerBossBattle);
            TriggerBossBattleFlag = false;

            //ボスバトル演出を表示する
            AnimBossBattle.gameObject.SetActive(true);
            AnimBossBattle.GetComponent<Animator>().SetTrigger("BossBattle");
        }


        //ボス撃破後の処理
        if (BossDeathFlag)
        {
            Debug.Log("<color=red>ボスが倒された</color>");
            BossDeathFlag = false;
            //障害物消滅演出
            Obstacle02.GetComponent<Animator>().SetTrigger("Obstacle01");
            

            //ミッションUIを非表示
            MissionUIClose();

            //新しいミッションUIを表示
            MissionUIOpen("妖精に会いに行こう！", "上の部屋にいる妖精を助けに行こう");
        }

        //救出後の処理
        if (FairyFlag)
        {
            //ミッションUIを非表示
            MissionUIClose();

            AnimMissionClear.GetComponent<Animator>().SetTrigger("MissionClear");
            BossDeathFlag = false;

            //ゲームクリア演出を再生
            AnimGameCler.gameObject.SetActive(true);
            AnimGameCler.GetComponent<Animator>().SetTrigger("GameCler");

            EventTalkWindwOpen("助けてくれてありがとう！");
            FairyFlag = false;


            //1秒後にタイトルに遷移する
            Invoke("GameEnd", 3f);
        }

    }
    void GameEnd()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("きえた" + Time.time);
        AnimGameCler.gameObject.SetActive(false);
    }

    void EventTalkWindwOpen(string messageTxt)
    {
        EventTalk.gameObject.SetActive(true);
        EventTalkTxt.GetComponent<TextMeshProUGUI>().text = messageTxt;
        EventTalk.GetComponent<Animator>().SetTrigger("EventTalk");
    }
    void EventTalkWindowClose()
    {
        EventTalk.gameObject.SetActive(false);
    }

    //ダイアログを開く
    void DialogOpen(string titleTxt, string messageTxt, string dialogImage)
    {
        Dialog.gameObject.SetActive(true);
        DialogTitle.GetComponent<TextMeshProUGUI>().text = titleTxt;
        DialogTxt.GetComponent<TextMeshProUGUI>().text = messageTxt;

        Sprite sprite = Resources.Load<Sprite>(dialogImage);
        Image image = DialogImage.GetComponent<Image>();
        image.sprite = sprite;

        Dialog.GetComponent<Animator>().SetTrigger("DialogOpen");
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }

    //ミッションUIを開く
    void MissionUIOpen(string titleTxt, string messageTxt)
    {
        bool AnimationFlag = MissionUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MissionUIClose");
        if (AnimationFlag == false)
        {
            MissionUI.gameObject.SetActive(true);
            MissionUITitle.GetComponent<TextMeshProUGUI>().text = titleTxt;
            MissionUIText.GetComponent<TextMeshProUGUI>().text = messageTxt;
            //Openアニメーションを再生
            MissionUI.GetComponent<Animator>().SetTrigger("MissionUIOpen");
        }

    }

    //ミッションUIをを閉じる
    void MissionUIClose()
    {
        MissionUI.GetComponent<Animator>().SetTrigger("MissionUIClose");
        //Playerがアクション中か？
        bool AnimationFlag = MissionUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MissionUIClose");
        Debug.Log("再生中" + AnimationFlag);

        //通常ならダメージを受ける
        if (AnimationFlag == false)
        {
            MissionUI.gameObject.SetActive(false);
        }

    }



}
