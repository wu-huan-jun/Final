using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_trigger : MonoBehaviour
{
    [SerializeField] private bool entered = false;
    [SerializeField] private bool trigged = false;
    [SerializeField] private bool keyDown = false;
    [SerializeField] private CanvasGroup m_canavasGroup;//�����ײ����ʾ������
    public CanvasGroup next_canavasGroup;//��Ұ��°�������ʾ�����֣��Ǳ��
    [SerializeField] private GameObject previous;//Ҫ���õ���һ����ײ
    [SerializeField] private GameObject next;//Ҫ���õ���һ����ײ
    [SerializeField] private KeyCode m_key;//��Ҫ��Ұ��µİ���
    [Header("Player Control")]//����������Ƿ�Ҫ��������ƶ�
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
