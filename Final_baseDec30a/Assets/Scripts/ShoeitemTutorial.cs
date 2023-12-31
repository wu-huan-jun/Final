using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoeitemTutorial : MonoBehaviour
{
    [SerializeField] private GameObject categoryUI;
    [SerializeField] private CanvasGroup categoryitem;
    [SerializeField] private GameObject trigger;
    [SerializeField] private CanvasGroup shoes1;
    private bool trigged = false;
    private bool UI_done = false;
    [SerializeField] private GameObject self;
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            trigged = true;
            categoryUI.GetComponent<ShoesCategory>().Left1 = "Basic";
            categoryitem.alpha = 1;
        }
    }

    private void Start()
    {
        trigger.SetActive(false);
        shoes1.alpha = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (trigged && !UI_done)
        {
            UIManager.FadeIn(shoes1, 3f, true);
            if (shoes1.alpha == 1)
            {
                UI_done = true;
            }
        }
        if (UI_done)
        {
            UIManager.FadeOut(shoes1, .5f, true);
            trigger.SetActive(true);
            if (shoes1.alpha == 0)
                Destroy(self);
        }
    }
}
