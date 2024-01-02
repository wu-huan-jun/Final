using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CanvasGroup BasicEnterPannel;
    public CanvasGroup tutorial1;
    public GameObject cam;
    public int crtstalValue = 0;
    public int coin = 0;
    [SerializeField] private float fadeOutSpeed =1f;
    [SerializeField] private float fadeInSpeed = 1f;
    [SerializeField] private bool mouse0Down = false;
    // Start is called before the first frame update
    void Start()
    {
        BasicEnterPannel.alpha = 1;
        tutorial1.alpha = 0;
        cam.SetActive(false);
    }
    public static void FadeOut(CanvasGroup m_group,float fadeOutSpeed,bool mouse0Down)//淡入淡出函数公用，要写static
    {
        Debug.Log("UIFadeoutAlpha"+ m_group.alpha);
        m_group.alpha -= fadeOutSpeed * Time.deltaTime;
        m_group.interactable = false;
        m_group.blocksRaycasts = false;
        if (m_group.alpha == 0f)
        {
            mouse0Down = false;
        }
    }
    public static void FadeIn(CanvasGroup m_group,float fadeInSpeed,bool mouse0Down)
    {
        Debug.Log("UIFadeinAlpha" + m_group.alpha);
        m_group.alpha += fadeInSpeed * Time.deltaTime;
        m_group.interactable = true;
        m_group.blocksRaycasts = true;
        if (m_group.alpha == 1f)
        {
            mouse0Down = false;
        }
    }
    public static void FadeInandOut(CanvasGroup m_group,float fadeInSpeed,float stayTime,float fadeOutSpeed,float time)
    {
        if(m_group.alpha == 0f) m_group.alpha = 0.01f;
        FadeIn(m_group, fadeInSpeed, true);
        if (time < stayTime) time += (1 / 60);
        else FadeOut(m_group, fadeOutSpeed, true);
    }
    public static void AddCrystal(GameObject UIManager, TMP_Text text, int value)
    {
        value += int.Parse(text.text);//直接把现有数字转回int再加
        UIManager.GetComponent<UIManager>().crtstalValue = value;
        PlayerPrefs.SetInt("CrystalValue",value);
        text.text = value.ToString();
    }
    public static void AddCoin(GameObject UIManager, TMP_Text text, int value)
    {
        value += int.Parse(text.text);//直接把现有数字转回int再加
        UIManager.GetComponent<UIManager>().coin = value;
        PlayerPrefs.SetInt("Coin", value);
        text.text = value.ToString();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0)&&BasicEnterPannel.alpha >0)//鼠标检测是通用的
        {
            mouse0Down = true;
            Debug.Log("enter");
        }
        
        if(mouse0Down == true )
        {
            if(BasicEnterPannel.alpha != 0)
            {
                FadeOut(BasicEnterPannel, fadeOutSpeed, mouse0Down);//淡出开始屏幕
                FadeIn(tutorial1, fadeInSpeed, mouse0Down);//淡入WASD
                cam.SetActive(true);
            }
        }
    }
}
