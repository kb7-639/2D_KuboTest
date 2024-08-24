using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
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
        //Playerが接触したら
        if (other.gameObject.tag == "Player")
        {
            //自身を消す
            Destroy(this.gameObject);
        }
    }
}
