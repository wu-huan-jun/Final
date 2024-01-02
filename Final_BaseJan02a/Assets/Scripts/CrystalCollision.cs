using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//这个脚本其实已经是控制所有可以被踢爆的东西的脚本了
public class CrystalCollision : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int kickTime;
    [SerializeField] private int kickCoolDownTime = 0;
    [SerializeField] private Light m_light;
    [SerializeField] private GameObject self;
    [SerializeField] private float kickLightMultiplier = 8f;
    [SerializeField] private int intensity = 3;

    [Header("UI")]//这个是tutorial用的，平时不要开
    public CanvasGroup fadeOutGroup;
    private bool fadeOutControl = false;
    public GameObject previousCollider;

    [Header("Drop items")]
    public bool doDrop;
    public GameObject item;
    public Vector3 dropPosition;
    public Quaternion dropRotation = new Quaternion(0, 0, 0, 0);

    private void Start()
    {
        if (dropPosition == Vector3.zero)
        {
            dropPosition = self.transform.position;
            dropRotation = self.transform.rotation;
        }
    }
    private void FixedUpdate()
    {
        if (kickCoolDownTime > 0)
            kickCoolDownTime -= 1;
        if (fadeOutControl == true)
        {
            UIManager.FadeOut(fadeOutGroup, 1f, true);
            if (fadeOutGroup.alpha == 0)
            {
                Destroy(self);
            }
        }
    }
    public void OnTriggerEnter(Collider feet)
    {
        if (feet.gameObject.tag == "Feet" && kickCoolDownTime == 0)
        {
            kickCoolDownTime = 60;
            kickTime += 1;
            //feet.gameObject.GetComponent<PlayerMovement>().r_foot.SetActive(false);
            //这行虽然现在不用了但是作为修改其他脚本变量示例留着
            if (kickTime < intensity && m_light)
                m_light.intensity = kickTime * kickLightMultiplier;
            if (kickTime >= intensity)
            {
                if (m_light) m_light.intensity = 0;//有灯关灯
                if (doDrop)
                {
                    GameObject go = GameObject.Instantiate(item, dropPosition, dropRotation) as GameObject;/*实例化一个perfab*/
                }
                if (!fadeOutGroup) { Destroy(self); }//跟UI无关就直接把自己删了
                if (fadeOutGroup)
                {
                    if (previousCollider) previousCollider.SetActive(false);
                    fadeOutControl = true;
                }
            }
        }

    }
}
