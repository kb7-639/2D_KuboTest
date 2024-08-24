using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DialogClose()
    {
        //ボタンが押されたらダイアログを閉じるアニメーションを再生する
        this.GetComponent<Animator>().SetTrigger("DialogClose");
        Debug.Log("ボタンが押された");

        //0.5秒したらダイアログを非アクティブにする
        Invoke("DialogCloseActive", 0.5f);

        //ダイアログの開閉フラグ
        GameController.CloseBtnFlag = true;
    }

    //ダイアログを非アクティブにする
    void DialogCloseActive()
    {
        this.gameObject.SetActive(false);
    }
}
