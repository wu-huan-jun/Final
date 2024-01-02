using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterTheGame : MonoBehaviour
{
    public GameObject BeginPannel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            BeginPannel.SetActive(false);
        }
    }
}
