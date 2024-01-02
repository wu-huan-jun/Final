using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private bool isCrystal = false;
    [SerializeField] private bool isMoney = false;
    [SerializeField] private GameObject m_UIManager;
    [SerializeField] private TMP_Text crystalTMP;
    [SerializeField] private TMP_Text moneyTMP;
    [SerializeField] private int number = 1;
    [SerializeField] private GameObject self;
    private Quaternion rot;
    private float a = 0;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (isCrystal)
            {
                UIManager.AddCrystal(m_UIManager,crystalTMP,number);
                Destroy(self);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isCrystal) 
        self.GetComponent<Transform>().rotation = Quaternion.Euler(0, a++,0);//Ë®¾§Ðý×ª£¨£¿£©·ÖÖÓ£¨£¿
    }
}
