using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorCollider : MonoBehaviour
{
    public string SceneName;
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
        //プレイヤーがドアに接触したら
        if (other.gameObject.tag == "Player")
        {
            //インゲームへ遷移する
            SceneManager.LoadScene(SceneName);
        }
    }
}
