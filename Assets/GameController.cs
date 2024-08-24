using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{
    //�{�X�����X�^�[�̂�Prefab
    public GameObject BossMonster;

    //�~�b�V�����e�L�X�g�\����Prefab
    public GameObject MissionUIText;

    //�~�b�V�������̂�UI
    public GameObject MissionUI;

    //�~�b�V����UI�̃^�C�g��
    public GameObject MissionUITitle;

    //�Q�[���N���A���o
    public GameObject AnimGameCler;

    //�~�b�V�����N���A�̉��o
    public GameObject AnimMissionClear;

    //��Q���P�̃I�u�W�F�N�g
    public GameObject Obstacle01;

    //��Q���Q�̃I�u�W�F�N�g
    public GameObject Obstacle02;

    //�C�x���g�g�[�NUI�̃I�u�W�F�N�g
    public GameObject EventTalk;

    //�C�x���g�g�[�NUI�e�L�X�g
    public GameObject EventTalkTxt;

    //�_�C�A���O�I�u�W�F�N�g
    public GameObject Dialog;

    //�_�C�A���O��UI�e�L�X�g
    public GameObject DialogTxt;

    //�_�C�A���O�̌��o��
    public GameObject DialogTitle;

    //�_�C�A���O�̉摜
    public GameObject DialogImage;

    //Mission�����p�̃R���C�_�[
    public GameObject TriggerMission;

    //�{�X�o�g���G���A�ɐڋ߂����ۂ̉��o�\���p�̃R���C�_�[
    public GameObject TriggerBossBattle;

    //�{�X�o�g�����o�p�̂�Prefab
    public GameObject AnimBossBattle;



    //�G�̌��j���̃J�E���^�[
    //�O���̃N���X����A�N�Z�X�\�ȕϐ� ... GameController.EnemyCounter
    static public int EnemyCounter;

    //�G�̌��j���̏����l
    private int Enemydefault = 6;

    //�{�X�𐶐�����t���O
    private bool BossFlag = true;

    //�{�X�̎��S�t���O
    static public bool BossDeathFlag = false;

    //����ɃA�N�Z�X���邽�߂̃t���O
    static public bool FairyFlag = false;

    //����̃G���A�i�~�b�V�����G���A�j�ɐڐG�����t���O
    static public bool TriggerMissionFlag = false;

    //�{�^���������ꂽ��
    static public bool CloseBtnFlag = false;

    private bool CloseBtnFlag02 = false;

    //����̃G���A�i�{�X�o�g���j�ɐڐG�����t���O
    static public bool TriggerBossBattleFlag = false;




    // Start is called before the first frame update
    void Start()
    {
        //������
        EnemyCounter = this.Enemydefault;

        DialogOpen("�����Ă���d���������悤", "���������X�^�[�ɂ���ėD�����d���������Ă��܂��܂����B���̕����܂ŋ~�o�Ɍ������܂��傤�B", "tutorial01");
        //�{�^���̃t���O��������


    }

    // Update is called once per frame
    void Update()
    {
        if (TriggerMissionFlag)
        {
            CloseBtnFlag = false;
            Debug.Log("�~�b�V�����t���O��" + CloseBtnFlag);
            Camera.main.GetComponent<Animator>().SetTrigger("Event01");

            //2�b��Ɏ��s����
            StartCoroutine(DelayMethod(2f, () =>
            {
                DialogOpen("�X�e�[�W���̃����X�^�[��|����", "�����X�^�[�B�ɂ���ē������Ă��܂��B�����X�^�[��S�ē|���Ĕ����J���܂��傤�B", "tutorial02");
            }));

            Destroy(TriggerMission);
            TriggerMissionFlag = false;
            Debug.Log("�������[�v�H");
            Debug.Log("�~�b�V�����t���O��" + CloseBtnFlag);
            this.CloseBtnFlag02 = true;

        }
        //�_�C�A���O�̂�OK�{�^���������ꂽ��~�b�V����UI��\��
        if (CloseBtnFlag && CloseBtnFlag02)
        {
            MissionUIOpen("�����J�����߂�", "�X�e�[�W���̃����X�^�[��S�ē|�����i" + EnemyCounter + "/" + Enemydefault + "�j");
            CloseBtnFlag = false;
            Debug.Log("�~�b�V����UI�̐���");
        }

        if (BossFlag)
        {
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

            //�~�b�V����UI���\��
            MissionUIClose();

            //�V�����~�b�V����UI��\��
            MissionUIOpen("�{�X��|�����I", "���ɂ���{�X��|���ɍs����");

            //��Q�����ŉ��o
            Obstacle01.GetComponent<Animator>().SetTrigger("Obstacle01");

            EventTalkWindwOpen("���̕����֐i�߂�悤�ɂȂ����悤�ł�");
            Invoke("EventTalkWindowClose", 3f);
        }
        //�{�X�o�g���G���A�ɐڋ߂����ۂ̉��o�\��
        if (TriggerBossBattleFlag)
        {
            Destroy(TriggerBossBattle);
            TriggerBossBattleFlag = false;

            //�{�X�o�g�����o��\������
            AnimBossBattle.gameObject.SetActive(true);
            AnimBossBattle.GetComponent<Animator>().SetTrigger("BossBattle");
        }


        //�{�X���j��̏���
        if (BossDeathFlag)
        {
            Debug.Log("<color=red>�{�X���|���ꂽ</color>");
            BossDeathFlag = false;
            //��Q�����ŉ��o
            Obstacle02.GetComponent<Animator>().SetTrigger("Obstacle01");
            

            //�~�b�V����UI���\��
            MissionUIClose();

            //�V�����~�b�V����UI��\��
            MissionUIOpen("�d���ɉ�ɍs�����I", "��̕����ɂ���d���������ɍs����");
        }

        //�~�o��̏���
        if (FairyFlag)
        {
            //�~�b�V����UI���\��
            MissionUIClose();

            AnimMissionClear.GetComponent<Animator>().SetTrigger("MissionClear");
            BossDeathFlag = false;

            //�Q�[���N���A���o���Đ�
            AnimGameCler.gameObject.SetActive(true);
            AnimGameCler.GetComponent<Animator>().SetTrigger("GameCler");

            EventTalkWindwOpen("�����Ă���Ă��肪�Ƃ��I");
            FairyFlag = false;


            //1�b��Ƀ^�C�g���ɑJ�ڂ���
            Invoke("GameEnd", 3f);
        }

    }
    void GameEnd()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("������" + Time.time);
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

    //�_�C�A���O���J��
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

    //�~�b�V����UI���J��
    void MissionUIOpen(string titleTxt, string messageTxt)
    {
        bool AnimationFlag = MissionUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MissionUIClose");
        if (AnimationFlag == false)
        {
            MissionUI.gameObject.SetActive(true);
            MissionUITitle.GetComponent<TextMeshProUGUI>().text = titleTxt;
            MissionUIText.GetComponent<TextMeshProUGUI>().text = messageTxt;
            //Open�A�j���[�V�������Đ�
            MissionUI.GetComponent<Animator>().SetTrigger("MissionUIOpen");
        }

    }

    //�~�b�V����UI�������
    void MissionUIClose()
    {
        MissionUI.GetComponent<Animator>().SetTrigger("MissionUIClose");
        //Player���A�N�V���������H
        bool AnimationFlag = MissionUI.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("MissionUIClose");
        Debug.Log("�Đ���" + AnimationFlag);

        //�ʏ�Ȃ�_���[�W���󂯂�
        if (AnimationFlag == false)
        {
            MissionUI.gameObject.SetActive(false);
        }

    }



}
