using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class arrow : MonoBehaviour
{
    GameObject player;
    Rigidbody2D myrigi;
    public float arrowSpeed;
    private void Awake()
    {
        arrowSpeed = 20.0f;
        player = GameObject.Find("Player");
        myrigi = GetComponent<Rigidbody2D>();
        if (player.transform.localScale.x  == 1.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            myrigi.AddForce(Vector2.right * arrowSpeed, ForceMode2D.Impulse);
        }
        else if (player.transform.localScale.x == -1.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            myrigi.AddForce(Vector2.left * arrowSpeed, ForceMode2D.Impulse);
        }
        Destroy(this.gameObject, 5.0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy") 
        {

        }
        if (collision.tag == "ground")
        {
            Destroy(this.gameObject);
        }
    }

}
