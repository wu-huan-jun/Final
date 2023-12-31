using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int kickTime;
    [SerializeField] private int kickCoolDownTime = 0;
    [SerializeField] private Light m_light;
    [SerializeField] private GameObject self;
    [SerializeField] private float kickLightMultiplier = 8f;
    [SerializeField] private int intensity = 3;
    


    [Header("Drop items")]
    public bool doDrop;
    public GameObject item;
    public Vector3 dropPosition;
    public Quaternion dropRotation = new Quaternion(0, 0, 0, 0);

    private void FixedUpdate()
    {
        if(kickCoolDownTime >0)
        kickCoolDownTime -= 1;
        if (dropPosition == Vector3.zero)
        {
            dropPosition = self.transform.position;
            dropRotation = self.transform.rotation;
        }
    }
    public void OnTriggerEnter(Collider feet)
    {
        if (feet.gameObject.tag == "Feet" && kickCoolDownTime == 0)
        {
            kickCoolDownTime = 60;
            kickTime += 1;
            //feet.gameObject.GetComponent<PlayerMovement>().r_foot.SetActive(false);
            //������Ȼ���ڲ����˵�����Ϊ�޸������ű�����ʾ������
            if (kickTime < intensity &&m_light) 
                m_light.intensity = kickTime * kickLightMultiplier;
            if(kickTime >=intensity)
            {
                if(m_light) m_light.intensity = 0;//�еƹص�
                if (doDrop)
                {
                    GameObject go = GameObject.Instantiate(item, dropPosition, dropRotation) as GameObject;/*ʵ����һ��perfab*/
                }
                Destroy(self);
            }
        }

    }
}
