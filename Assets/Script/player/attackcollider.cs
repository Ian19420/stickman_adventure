using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackcollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {

        }
    }
}
