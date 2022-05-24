using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{
    public float speed;
    public float run;
    public int blood;

    public float Jumpforce;//��Ծ����
    public Transform GroundCheck;//�����ָ�
    private bool isGround;//�����ж��Ƿ��ܽ�����Ծ����
    private int extraJump;//������
    public LayerMask ground;

    private int flip;
    Transform player;
    Rigidbody2D rb;
    private Animator anim;
    public GameObject bag;
    public bool canOpen;

    private float time;
    private bool turnRed;
    // Start is called before the first frame update
    void Start()
    {
        flip = 1;
        player = this.gameObject.transform;
        rb = this.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        time = 0;
        turnRed = false;
    }
    void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(GroundCheck.position, 0.1f, ground);
    }
    // Update is called once per frame
    void Update()
    {
        bagOpen();
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("walk",true);
            if (flip != 1)
                playerFlip();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);
                player.position += player.right * run * Time.deltaTime;
            }
            else
            {
                anim.SetBool("run", false);
                player.position += player.right * speed * Time.deltaTime;
            }
                
        }
        if (Input.GetKeyUp(KeyCode.D)|| Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("walk", false);
            anim.SetBool("run", false);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("walk",true);
            if (flip != -1)
                playerFlip();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetBool("run", true);
                player.position -= player.right * run * Time.deltaTime;
            }
            else
            {
                anim.SetBool("run", false);
                player.position -= player.right * speed * Time.deltaTime;
            }
        }
        Jump();
        if (turnRed)
        {
            time -= Time.deltaTime;
            if(time<=0)
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
                turnRed = false;
            }
        }
    }
    public void playerFlip()
    {
        player.localScale = new Vector3(-player.localScale.x,player.localScale.y,player.localScale.z);
        flip = -flip;
    }
    void Jump()
    {
        //������
        if (isGround)
        {
            extraJump = 1;//���ڵ����ϵ�ʱ�򣬿���������       
        }
        if (Input.GetKeyDown(KeyCode.Space) && extraJump > 0)
        {
            rb.velocity = Vector2.up * Jumpforce;
            extraJump--;
            anim.SetTrigger("jump");
            //jumpAudio.Play();
        }
    }
    public void Slower()
    {
        speed /= 2;
        run /= 2;
    }
    public void bagOpen()
    {
        if (Input.GetKeyDown(KeyCode.Tab)&&canOpen)
        {
            bag.SetActive(!bag.activeSelf);
        }
    }
    public void beAttacked(int hurt)
    {
        blood -= hurt;
        this.GetComponent<SpriteRenderer>().color = Color.red;
        time = 0.5f;
        turnRed = true;
    }
}
