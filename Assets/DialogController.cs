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
        //�{�^���������ꂽ��_�C�A���O�����A�j���[�V�������Đ�����
        this.GetComponent<Animator>().SetTrigger("DialogClose");
        Debug.Log("�{�^���������ꂽ");

        //0.5�b������_�C�A���O���A�N�e�B�u�ɂ���
        Invoke("DialogCloseActive", 0.5f);

        //�_�C�A���O�̊J�t���O
        GameController.CloseBtnFlag = true;
    }

    //�_�C�A���O���A�N�e�B�u�ɂ���
    void DialogCloseActive()
    {
        this.gameObject.SetActive(false);
    }
}
