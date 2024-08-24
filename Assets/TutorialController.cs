using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    //�C�x���g�g�[�NUI�̃I�u�W�F�N�g
    public GameObject EventTalk;

    //�C�x���g�g�[�NUI�e�L�X�g
    public GameObject EventTalkTxt;

    //����̃G���A�ɐN���������̃t���O
    static public bool TutorialArea = false;

    //�G���A�̃I�u�W�F�N�g
    public GameObject ArrowTrigger;

    //�O���̃N���X����A�N�Z�X�\�ȕϐ� ... GameController.EnemyCounter
    static public int EnemyCounter;

    //�G�l�~�[��|�������t���O
    private bool TutorialEnemy = true;


    // Start is called before the first frame update
    void Start()
    {
        //������
        EnemyCounter = 1;

        EventTalkWindwOpen("�L�[�{�[�h��<color=#ff3000>�u��v�u���v�u���v�u�E�v</color>�L�[�ňړ��ł��܂��B\n�D���ȃL�[�������Ĉړ����Ă݂܂��傤");
        Invoke("EventTalkWindowClose", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        //���������Arrow�L�[�������ꂽ��E�B���h�E��\��
        if (TutorialArea)
        {
            EventTalkWindwOpen("���͉E�ɐi�񂾂Ƃ���ɂ��郂���X�^�[��|���Ă݂܂��傤�I\n<color=#ff3000>�u�X�y�[�X�v</color>�L�[�ōU�����J��o���܂�");
            Invoke("EventTalkWindowClose", 3f);
            TutorialArea = false;
            Destroy(ArrowTrigger);
        }

        if (EnemyCounter == 0 && TutorialEnemy)
        {
            EventTalkWindwOpen("�����X�^�[��|���܂����ˁI\n����ł�<color=#ff3000>�E���̃h�A</color>�ɓ����Đ�ɐi�݂܂��傤");
            Invoke("EventTalkWindowClose", 3f);
            TutorialEnemy = false;
        }
    }
    void GameEnd()
    {
        SceneManager.LoadScene("Title");
        Debug.Log("������" + Time.time);
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
