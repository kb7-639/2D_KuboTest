using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    //�G�Ɍ�������ڐG�������̃J�E���^�[
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
        //�G�ɕ��킪����������
        if (other.gameObject.tag == "Player")
        {
            this.enemyCounter += 1;
            Debug.Log("�v���C���[�ɐ؂�ꂽ" + enemyCounter);
            GetComponent<Animator>().SetTrigger("Hit");
        }
    }
}
