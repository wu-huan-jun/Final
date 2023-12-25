using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float groundDrag;

    [Header("JumpControl")]
    public KeyCode jumpKey = KeyCode.Space;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public Transform orientation;

    [Header("AnimationControl")]
    public Animator m_Animator;

    [Header("ShoesHolder")]
    public Shoes shoes;
    public enum Shoes {Basic,Water,Air};
    public GameObject basic;
    public GameObject waterShoes;
    public GameObject airShoes;
    [Header("Terrians")]
    public GameObject waterHolder;
    public GameObject airHolder;


    [Header("Debug")]

    public bool grounded;
    public bool readyToJump = true;

    float horizontalInput;
    float vertiaclInput;

    Vector3 moveDirection;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;//������ת��

    }

    private void BasicInput()//��ȡ����
    {
        horizontalInput = Input.GetAxis("Horizontal");
        vertiaclInput = Input.GetAxis("Vertical");
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump),jumpCoolDown);
        }
    }
    private void MovePlayer()
    {
        moveDirection = orientation.forward * vertiaclInput + orientation.right * horizontalInput;

       if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f, ForceMode.Force);//����ü����ķ�ʽʵ���˶�
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 20f * airMultiplier, ForceMode.Force);
        }
    }
    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if (flatVel.magnitude > moveSpeed ) 
        {
            Vector3 limitiedVel = flatVel.normalized * moveSpeed ;
            rb.velocity = new Vector3(limitiedVel.x, rb.velocity.y, limitiedVel.z);
        }
        m_Animator.SetFloat("flatVel", flatVel.magnitude); //���ٶȴ���AC����AC������ʲô����
    }
    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);//���ImpulseҲ��������д�������
        m_Animator.SetBool("isJumping", true);
    }
    private void ResetJump()
    {
        readyToJump = true;
        m_Animator.SetBool("isJumping", false);
    }
    private void TerrainDetect()
    {
        waterHolder.SetActive(false);
        airHolder.SetActive(false);
        if (shoes == Shoes.Water)
        {
            waterShoes.SetActive(true);
            waterHolder.SetActive(true);
        }
        if (shoes == Shoes.Air)
        {
            airHolder.SetActive(true);
            airShoes.SetActive(true);
        }
    }
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, whatIsGround);//����Ƿ��ڵ�����
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;
        
        TerrainDetect();
        MovePlayer();
        BasicInput();
        SpeedControl();
    }
}
