using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //�{�X�����X�^�[�̂�Prefab
    public GameObject BossMonster;
    //�O���̃N���X����A�N�Z�X�\�ȕϐ� ... GameController.EnemyCounter
    static public int EnemyCounter;
    bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        //������
        EnemyCounter = 2;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        //�L���[�u�̐���
        if (EnemyCounter == 0)
        {
            Debug.Log("�G�̃J�E���^�[���O�ɂȂ���");
            GameObject go = Instantiate(BossMonster);

        }
        
    }
}
