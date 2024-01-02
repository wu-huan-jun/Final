using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_trigger : MonoBehaviour
{
    [SerializeField] private bool entered = false;
    [SerializeField] private bool trigged = false;
    [SerializeField] private bool keyDown = false;
    [SerializeField] private CanvasGroup m_canavasGroup;//这个碰撞会显示的文字
    public CanvasGroup next_canavasGroup;//玩家按下按键后显示的文字（非必填）
    [SerializeField] private GameObject previous;//要禁用的上一个碰撞
    [SerializeField] private GameObject next;//要启用的下一个碰撞
    [SerializeField] private KeyCode m_key;//需要玩家按下的案件
    [Header("Player Control")]//在这里决定是否要冻结玩家移动
    public GameObject player;
    public bool banningMovement = false;
    private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        if(next == true)
        {
            next.SetActive(false);
        }
    }

    // Update is called once per frame
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && !entered)
        {
            entered = true;
        }
    }
    private void FixedUpdate()
    {
        if (entered && m_canavasGroup.alpha <1 && !Input.GetKey(m_key) && !trigged)
        {
            UIManager.FadeIn(m_canavasGroup, 2f, true);
            if(banningMovement == true && player.GetComponent<PlayerMovement>().moveSpeed != 0)
            {
                moveSpeed = player.GetComponent<PlayerMovement>().moveSpeed;
                player.GetComponent<PlayerMovement>().moveSpeed = 0;
            }
        }
        if(m_canavasGroup.alpha ==1 )
        {
            trigged = true;
            if(next == true)
            {
                next.SetActive(true);
            }
            if (previous == true)
            {
                previous.SetActive(false);
            }
            
        }
        if (Input.GetKey(m_key))
        {
            keyDown = true;
            if (banningMovement == true)
            {
                player.GetComponent<PlayerMovement>().moveSpeed = moveSpeed;
            }
        }
        if (keyDown == true && m_canavasGroup.alpha >0 )
        {
            UIManager.FadeOut(m_canavasGroup, 1f, true);
            if (next_canavasGroup)
            {
                UIManager.FadeIn(next_canavasGroup, 1f, true);
            }
        }
    }

}
