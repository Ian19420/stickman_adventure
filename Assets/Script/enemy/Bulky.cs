using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bulky : MonoBehaviour
{
    public Vector3 targetPosition;
    Vector3 originPosition, turnPoint;
    public float mySpeed;
    private Animator myAnim;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        mySpeed = 1;
        originPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);

    }
    void Start()
    {

    }

    void Update()
    {
        if (transform.position.x == targetPosition.x)
        {
            myAnim.SetTrigger("idle");
            turnPoint = originPosition;
            StartCoroutine(Turn(true));

        }
        else if (transform.position.x == originPosition.x)
        {
            myAnim.SetTrigger("idle");
            turnPoint = targetPosition;
            StartCoroutine(Turn(false));
        }
        if (myAnim.GetCurrentAnimatorStateInfo(0).IsName("walk_an"))
        {
            transform.position = Vector3.MoveTowards(transform.position, turnPoint, mySpeed * Time.deltaTime);
        }
    }
    IEnumerator Turn(bool turnright)
    {

        yield return new WaitForSeconds(0.08f);
        if (turnright)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

    }
}
