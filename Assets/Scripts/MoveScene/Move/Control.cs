using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float speed;
    public float run;
    public int blood;

    public float Jumpforce;//跳跃力度
    public Transform GroundCheck;//改善手感
    private bool isGround;//用于判断是否能进行跳跃操作
    private int extraJump;//二段跳
    public LayerMask ground;

    private int flip;
    Transform player;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        flip = 1;
        player = this.gameObject.transform;
        rb = this.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, ground);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (flip != 1)
                playerFlip();
            if(Input.GetKey(KeyCode.LeftShift))
                player.position += player.right * run * Time.deltaTime;
            else
                player.position += player.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            if (flip != -1)
                playerFlip();
            if (Input.GetKey(KeyCode.LeftShift))
                player.position -= player.right * run * Time.deltaTime;
            else
                player.position -= player.right * speed * Time.deltaTime;
        }
        Jump();

    }
    public void playerFlip()
    {
        player.localScale = new Vector3(-player.localScale.x,player.localScale.y,player.localScale.z);
        flip = -flip;
    }
    void Jump()
    {
        //二段跳
        if (isGround)
        {
            extraJump = 1;//踩在地面上的时候，可以跳两次       
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.velocity = Vector2.up * Jumpforce;
            extraJump--;
            //Anim.SetBool("jumping", true);
            //jumpAudio.Play();
        }
    }
    public void Slower()
    {
        speed /= 2;
        run /= 2;
        Jumpforce /= 2;
    }
}
