using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float mySpeed;
    public float jumpforce;
    public GameObject attackcollider, arrowPrefab;

    int playerlife;

    [HideInInspector]
    public Animator myAnim;
    private Rigidbody2D myRigi;
    private SpriteRenderer mySr;

    [HideInInspector]
    public bool isjumpPressed, canjump, isattack, isinjured, protect, isdied;
    private void Awake()
    {
        mySpeed = 5f;
        jumpforce = 20f;
        myAnim = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        mySr = GetComponent<SpriteRenderer>();
        isjumpPressed = false;
        canjump = true;
        isattack = false;
        isinjured = false;
        protect = false;
        isdied = false;
        playerlife = 3;

    }
    //void Start()
//   {
    
  //  }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canjump == true && isinjured == false)
        {
            isjumpPressed = true;
            canjump = false;
        }
        if (Input.GetKeyDown(KeyCode.T) && isinjured == false)
        {
            myAnim.SetTrigger("attack");
            isattack = true;
            canjump = false;
        }
        if (Input.GetKeyDown(KeyCode.G) && isinjured == false)
        {
            myAnim.SetTrigger("shot");
            isattack = true; 
            canjump = false;
        }

    }
    private void FixedUpdate()
    {
        float move_hor = Input.GetAxisRaw("Horizontal");

        if (isattack || isdied || isinjured)
        {
            move_hor = 0;
        }
        if (move_hor > 0 )
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if(move_hor < 0 )
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        myAnim.SetFloat("run", Mathf.Abs(move_hor));
        if (isjumpPressed)
        {
            myRigi.AddForce(Vector2.up*jumpforce, ForceMode2D.Impulse);
            isjumpPressed = false;
            myAnim.SetBool("jump", true);
        }
        if (!isinjured)
        {
            myRigi.velocity = new Vector2(move_hor * mySpeed, myRigi.velocity.y);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy"  && isinjured == false && protect == false)
        {
            playerlife--;
            if (playerlife < 1)
            {
                myAnim.SetBool("died", true);
                isinjured = true;
                isdied = true;
                myRigi.velocity = new Vector2(0, 0);
            }
            else
            {
                isinjured = true;
                mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
                myAnim.SetBool("injured", true);
                if (transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.5f, 1.0f);
                }
                else if (transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.5f, 1.0f);
                }
                StartCoroutine(SetisinjuredFalse());
            }  
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "enemy" && isinjured == false && protect == false)
        {
            playerlife--;
            if (playerlife < 1)
            {
                myAnim.SetBool("died", true);
                isinjured = true;
                isdied = true;
                myRigi.velocity = new Vector2(0, 0);
            }
            else
            {
                isinjured = true;
                mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
                myAnim.SetBool("injured", true);
                if (transform.localScale.x == 1.0f)
                {
                    myRigi.velocity = new Vector2(-2.5f, 1.0f);
                }
                else if (transform.localScale.x == -1.0f)
                {
                    myRigi.velocity = new Vector2(2.5f, 1.0f);
                }
                StartCoroutine(SetisinjuredFalse());
            }
        }

    }
    IEnumerator SetisinjuredFalse()
    {
        yield return new WaitForSeconds(1);
        myAnim.SetBool("injured", false);
        protect = true;
        isinjured = false;
        for (int i = 0; i< 4; i++)
        {
            mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 1);
            yield return new WaitForSeconds(0.5f);
            mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
        protect = false;
        mySr.color = new Color(mySr.color.r, mySr.color.g, mySr.color.b, 1);
    }
    public void SetisattackFalse()
    {
        isattack = false;
        canjump = true;
        myAnim.ResetTrigger("attack");
        myAnim.ResetTrigger("shot");
    }
    public void Forisinjured()
    {
        isattack = false;
        myAnim.ResetTrigger("attack");
        myAnim.ResetTrigger("shot");
        attackcollider.SetActive(false);

    }
    public void SetattackcolliderOn()
    {
        attackcollider.SetActive(true);
    }
    public void SetattackcolliderOff()
    {
        attackcollider.SetActive(false);
    }
    public void arrowInstanticate()
    {
        Vector3 temp = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Instantiate(arrowPrefab, temp, Quaternion.identity);

    }
}
