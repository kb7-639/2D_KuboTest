using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAction : MonoBehaviour
{
    //�G�Ɍ�������ڐG�������̃J�E���^�[
    private int enemyCounter = 0;
    //����
    public GameObject Bakuhatsu;
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


    // Start is called before the first frame update
    void Start()
    {
        //Rigidbody�R���|�[�l���g���擾
        this.myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //n�b�������甽�]������
        if(TimeCounter > FlipTime)
        {
            //�N���A�[
            TimeCounter = 0f;

            //���]
            isFlip = !isFlip;
        }

        //isFlip�ō��E�ړ�
        if(isFlip)
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //�G�ɕ��킪����������
        if (other.gameObject.tag == "Player")
        {
            this.enemyCounter += 1;
            //Debug.Log("�v���C���[�ɐ؂�ꂽ" + enemyCounter);
            GetComponent<Animator>().SetTrigger("Hit");

            //3��Ŕj��
            if(this.enemyCounter >= 1)
            {

                //�����}�[�N
                GameObject _object = Instantiate(Bakuhatsu);

                //�|�W�V�����␳
                _object.transform.position = this.gameObject.transform.position; 

                //�^�C�}�[�j��
                Destroy( _object, 0.1f);

                Destroy(this.gameObject);

                GameController.EnemyCounter -= 1;
            }
        }
    }
}
