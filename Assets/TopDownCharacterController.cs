using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;

        //敵に剣が何回接触したかのカウンター
        private int enemyCounter = 0;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }


        private void Update()
        {
            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                //左移動
                dir.x = -1;
                animator.SetInteger("Direction", 3);
                GetComponent<SpriteRenderer>().flipX = true;
            }
            else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                //右移動
                dir.x = 1;
                animator.SetInteger("Direction", 2);
                GetComponent<SpriteRenderer>().flipX = false;
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                //下移動
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                //上移動
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }

            //攻撃

            if (Input.GetKeyDown(KeyCode.Space) && GetComponent<SpriteRenderer>().flipX == true)
            {
                Debug.Log("左向きの攻撃");
                //Animationコンポーネントを取得し、トリガーををtrueにする
                GetComponent<Animator>().SetTrigger("Attack_Left");
            }
            else if(Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log("みぎ向きの攻撃");
                //Animationコンポーネントを取得し、トリガーををtrueにする
                GetComponent<Animator>().SetTrigger("Attack");
            }

            dir.Normalize();
            //animator.SetBool("IsMoving", dir.magnitude > 0);
            animator.SetBool("IsMoving", dir.x != 0);

            GetComponent<Rigidbody2D>().velocity = speed * dir;
        }
        void OnCollisionEnter2D(Collision2D other)
        {
            //敵に武器があたったら
            if (other.gameObject.tag == "Enemy")
            {
                this.enemyCounter += 1;
                Debug.Log("敵に衝突した" + enemyCounter);
            }
        }
    }
}
