using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeshAnimation : MonoBehaviour
{
    public Rigidbody rb;
    public Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        m_Animator.SetFloat("flatVel", flatVel.magnitude); //把速度传给AC，让AC决定用什么动画
    }
}
