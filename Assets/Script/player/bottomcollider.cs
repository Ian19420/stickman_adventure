using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomcollider : MonoBehaviour
{
    Player playerscript;
    private void Awake()
    {
        playerscript = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "ground")
        {
            playerscript.canjump = true;
            playerscript.myAnim.SetBool("jump", false);
        }
    }

}
