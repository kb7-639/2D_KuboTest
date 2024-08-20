using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    //�{�X�����X�^�[�̂�Prefab
    public GameObject BossMonster;

    //�~�b�V�����e�L�X�g�\����Prefab
    public GameObject MissionUIText;

    //�~�b�V�������̂�UI
    public GameObject MissionUI;

    //�~�b�V�����N���A�j���[�V����
    public GameObject MissionClearAnim;

    //�{�X�o���A�j���[�V����
    public GameObject BossBattle;
    

    //�G�̌��j���̃J�E���^�[
    //�O���̃N���X����A�N�Z�X�\�ȕϐ� ... GameController.EnemyCounter
    static public int EnemyCounter;

    //�G�̌��j���̏����l
    private int Enemydefault = 3;

    //�{�X�𐶐�����t���O
    private bool BossFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        //������
        EnemyCounter = this.Enemydefault;
    }

    // Update is called once per frame
    void Update()
    {
        //�G�̌��j���̏����l�����[�v����
        if(BossFlag)
        {
            //�~�b�V����UI�̃e�L�X�g�ƁA�c��̓G����\��
            MissionUIText.GetComponent<TextMeshProUGUI>().text = "�X�e�[�W���̃����X�^�[��S�ē|�����i" + EnemyCounter + "/" + Enemydefault + "�j";
        }
       

        //EnemyCounter���[����BossFlag��true�Ȃ琶������B
        if (EnemyCounter == 0 && BossFlag)
        {
            Debug.Log("�G�̃J�E���^�[���O�ɂȂ���");
            
            //�{�X�𐶐�����
            GameObject go = Instantiate(BossMonster);
            go.transform.position = new Vector2(-1.76f, 15.05f);

            //�{�X�𐶐������̂�false�ɂ���
            BossFlag = false;

            //�~�b�V����UI��j��
            Destroy(MissionUI);

        }
    }
}
