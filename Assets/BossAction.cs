using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
    //�G�Ɍ�������ڐG�������̃J�E���^�[
    private int enemyCounter = 0;

    //�ړ�������R���|�[�l���g������
    private Rigidbody2D myRigidbody;
    //�ړ���
    private float velocity = 1.0f;
    //�t���b�v
    private bool isFlip = false;
    //�ėp�^�C�}�[
    private float TimeCounter = 0f;
    //���]�܂ł̎���
    public float FlipTime = 1.5f;

    /*------�C���X�y�N�^����I�u�W�F�N�g���擾------*/
    //����
    public GameObject Bakuhatsu;

    //Hp�Q�[�W�̃I�u�W�F�N�g���擾
    public GameObject HpGauge;

    /*------�����X�^�[Hp�֘A------*/
    //Hp�Q�[�W�̎q�v�f�iGauge�j���擾���邽�߂̕ϐ�
    private GameObject HpGaugeChild;
    //�ő�̗�
    public float PlayerHitPoints = 5;
    //Hp�Q�[�W�p�ɑ̗͂𕪊����邽�߂̕ϐ��i1/�ő�̗́j
    private float PlayerHitPointsSplit;
    //�Q�[�W�̍ő�l�i�I�u�W�F�N�g�̉����j
    private float PlayerHitPointsMax;

    // Start is called before the first frame update
    void Start()
    {

        //�擾����Hp�Q�[�W��Prefab�̎q�v�f�iGauge�j���擾
        this.HpGaugeChild = HpGauge.transform.Find("Gauge").gameObject;
        //�q�v�f�̃C���X�y�N�^�[��̉������擾
        this.PlayerHitPointsMax = HpGaugeChild.transform.localScale.x;

        //������
        HpGaugeChild.transform.localScale = new Vector2(1f, 1f);

        //Rigidbody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody2D>();

        //�Q�[�W��Player��HP�Ŋ���
        this.PlayerHitPointsSplit = 1 / this.PlayerHitPoints;

    }

    // Update is called once per frame
    void Update()
    {
        //n�b�������甽�]������
        if (TimeCounter > FlipTime)
        {
            //�N���A�[
            TimeCounter = 0f;
            //���]
            isFlip = !isFlip;
        }

        //isFlip�ō��E�ړ�
        if (isFlip)
        {
            //�E�ֈړ�
            GetComponent<SpriteRenderer>().flipX = true;
            this.myRigidbody.velocity = new Vector2(velocity, this.myRigidbody.velocity.y);
        }
        else
        {
            //���ֈړ�
            GetComponent<SpriteRenderer>().flipX = false;
            this.myRigidbody.velocity = new Vector2(-velocity, this.myRigidbody.velocity.y);
        }
        //�^�C�}�[���Z
        TimeCounter += Time.deltaTime;

        //HP�Q�[�W�̈ʒu���L�����N�^�[�ɒǏ]����悤�ɍX�V
        HpGaugeChild.transform.localScale = new Vector2(PlayerHitPointsMax, 1f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //�G�ɕ��킪����������
        if (other.gameObject.tag == "Player")
        {
            //Player���A�^�b�N�A�j���[�V���������H
            bool flag = other.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Attack");
            Debug.Log("�U����" + flag);

            //�U�����������H
            if (flag)
            {
                this.enemyCounter += 1;
                //Debug.Log("�v���C���[�ɐ؂�ꂽ" + enemyCounter);
                GetComponent<Animator>().SetTrigger("Hit");

                //3��Ŕj��
                if (this.enemyCounter >= PlayerHitPoints)
                {
                    //�����}�[�N
                    GameObject _object = Instantiate(Bakuhatsu);
                    //�|�W�V�����␳
                    _object.transform.position = this.gameObject.transform.position;
                    //�^�C�}�[�j��
                    Destroy(_object, 0.1f);

                    Destroy(this.gameObject);

                    GameController.BossDeathFlag = true;
                }
                PlayerHitPointsMax -= PlayerHitPointsSplit;
            }
        }
    }
}