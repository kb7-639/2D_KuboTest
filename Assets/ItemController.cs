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
        //Player���ڐG������
        if (other.gameObject.tag == "Player")
        {
            //���g������
            Destroy(this.gameObject);
        }
    }
}
