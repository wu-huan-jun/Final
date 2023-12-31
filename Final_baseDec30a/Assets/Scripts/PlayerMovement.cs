using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;
    public float moveSpeedMultiplier;

    [Header("JumpControl")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;

    [Header("KickControl")]
    public GameObject l_foot;
    public GameObject r_foot;
    public GameObject cam;
    public float kickCoolDown = 5;
    public KeyCode kickKey = KeyCode.Mouse0;
    public float kickKeyDownTime = 0;//按住鼠标的时间
    public bool readyToKick = true;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public Transform orientation;

    [Header("AnimationControl")]
    public Animator m_Animator;

    [Header("ShoesHolder")]
    public LeftShoe l_shoe;
    public RightShoe r_shoe;
    public enum LeftShoe {None,Basic,Water,Cloud};
    public enum RightShoe {None,Basic,Water,Cloud};
    public GameObject basicShoeLeft;
    public GameObject waterShoeLeft;
    public GameObject cloudShoeLeft;
    public GameObject basicShoeRight;
    public GameObject waterShoeRight;
    public GameObject cloudShoeRight;

    [Header("Terrians")]
    public bool inWater = false;
    public bool inCloud = false;
    public GameObject waterHolder;
    public GameObject cloudHolder;
    

    //[Header("UIController")]
    
    [Header("Debug")]

    public bool grounded;
    public bool readyToJump = true;
    public Vector3 flatVel;

    float horizontalInput;
    float vertiaclInput;

    Vector3 moveDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;//冻结旋转？
        readyToKick = true;
        l_foot.SetActive(false);
        r_foot.SetActive(false);
    }

    private void BasicInput()//获取输入
    {
        horizontalInput = Input.GetAxis("Horizontal");
        vertiaclInput = Input.GetAxis("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded)//跳输入
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump),jumpCoolDown);
        }
        if (Input.GetKey(kickKey) && readyToKick && grounded)//踢输入
        {
            kickKeyDownTime += 1;
            if (kickKeyDownTime >= 20)
            {
                Kick();
            }
        }
        else if (!Input.GetKey(kickKey) && kickKeyDownTime >=1 && readyToKick && grounded)//长踢
        {
            Kick();
        }
    }
    private void MovePlayer()//WASD的移动函数
    {
        moveDirection = orientation.forward * vertiaclInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.MovePosition(rb.position + moveDirection * moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime);//直接移刚体！
        }
        else
        {
            //rb.AddForce(moveDirection.normalized * moveSpeed * 20f * airMultiplier, ForceMode.Force);
            rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime * airMultiplier);
        }
    }
    private void Jump()//跳
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);//这个Impulse也可以用来写后面的踢
        m_Animator.SetBool("isJumping", true);//其实这里可以直接用 m_Animator.Play("jump");这种，有空优化一下
    }
    private void Kick()//踢
    {
        r_foot.SetActive(true);
        if(kickKeyDownTime >= 12f)
        {
            l_foot.SetActive(true);
        }
        m_Animator.SetFloat("kickTime", kickKeyDownTime);
        kickKeyDownTime = 0;
        readyToKick = false;
        Invoke(nameof(ResetKick),kickCoolDown);
        Invoke(nameof(ResetKickAC), 1f);
    }
    private void ResetJump()//重置跳
    {
        readyToJump = true;
        m_Animator.SetBool("isJumping", false);
    }
    private void ResetKickAC()//重置踢
    {
        l_foot.SetActive(false);
        r_foot.SetActive(false);
        m_Animator.SetFloat("kickTime", 0);
    }
    private void ResetKick()
    {
        readyToKick = true;
        kickKeyDownTime = 0;
    }
    /*
    public void LeftShoeMeshSelection(LeftShoe left)
    {
        if (left == LeftShoe.None)
        {
            
        }
    }
    public void LeftShoeMeshSelection(RightShoe right)
    {
        if (right == RightShoe.None)
        {

        }
    }*/
    //这段用来控制鞋子的mesh
    private void TerrainDetect()
    {
        //Water
        if (inWater == true && ((l_shoe == LeftShoe.Water && r_shoe != RightShoe.Water)||(l_shoe != LeftShoe.Water && r_shoe == RightShoe.Water)))
        {
            rb.AddForce(transform.up * 20, ForceMode.Force);
        }
        else if (l_shoe == LeftShoe.Water && r_shoe == RightShoe.Water)
        {
            waterHolder.SetActive(true);
        }
        else waterHolder.SetActive(false);

        //Basic(Speed Enhance)
        //不知道为什么突然开始写英文注释了捏
        if ((l_shoe == LeftShoe .Basic && r_shoe != RightShoe.Basic)|| (l_shoe != LeftShoe.Basic && r_shoe == RightShoe.Basic))
        {
            moveSpeedMultiplier = 1.05f;
        }
        else if (l_shoe == LeftShoe.Basic && r_shoe == RightShoe.Basic)
        {
            moveSpeedMultiplier = 1.10f;
        }
        else
        {
            moveSpeedMultiplier = 1.0f;
        }
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, whatIsGround);//检测是否在地面上
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        TerrainDetect();
        MovePlayer();
        BasicInput();
        if (Input.GetKey(KeyCode.D) ||Input.GetKey(KeyCode.W) ||Input.GetKey(KeyCode.S) ||Input.GetKey(KeyCode.A) )m_Animator.SetBool("isWalking", true);//按下WASD时唤起AC动画
        else m_Animator.SetBool("isWalking",false);//放开WASD停止动画
    }
}
