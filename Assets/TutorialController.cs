using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    //イベントトークUIのオブジェクト
    public GameObject EventTalk;

    //イベントトークUIテキスト
    public GameObject EventTalkTxt;

    //特定のエリアに侵入したかのフラグ
    static public bool TutorialArea = false;

    //エリアのオブジェクト
    public GameObject ArrowTrigger;

    //外部のクラスからアクセス可能な変数 ... GameController.EnemyCounter
    static public int EnemyCounter;

    //エネミーを倒したかフラグ
    private bool TutorialEnemy = true;


    // Start is called before the first frame update
    void Start()
    {
        //初期化
        EnemyCounter = 1;

        EventTalkWindwOpen("キーボードの<color=#ff3000>「上」「下」「左」「右」</color>キーで移動できます。\n好きなキーを押して移動してみましょう");
        Invoke("EventTalkWindowClose", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //何かしらのArrowキーが押されたらウィンドウを表示
        if (TutorialArea)
        {
            EventTalkWindwOpen("次は右に進んだところにいるモンスターを倒してみましょう！\n<color=#ff3000>「スペース」</color>キーで攻撃を繰り出します");
            Invoke("EventTalkWindowClose", 3f);
            TutorialArea = false;
            Destroy(ArrowTrigger);
        }

        if (EnemyCounter == 0 && TutorialEnemy)
        {
            EventTalkWindwOpen("モンスターを倒せましたね！\nそれでは<color=#ff3000>右側のドア</color>に入って先に進みましょう");
            Invoke("EventTalkWindowClose", 3f);
            TutorialEnemy = false;
        }
    }
    void GameEnd()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("きえた" + Time.time);
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


}
