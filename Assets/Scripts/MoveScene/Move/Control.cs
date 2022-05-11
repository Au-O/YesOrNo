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
        //������
        if (isGround)
        {
            extraJump = 1;//���ڵ����ϵ�ʱ�򣬿���������       
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
