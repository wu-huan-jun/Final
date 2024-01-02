using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsBehavior : MonoBehaviour
{
    [SerializeField] private bool kicked = false;
    [SerializeField] private float i = 0;
    [SerializeField] private float rotSpeed = 1f;
    [SerializeField] private float rotTarget = 90f;
    [SerializeField] private float originalRot;
    Transform m_transform;
    // Start is called before the first frame update
    void Start()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (kicked)
        {
            if (i < rotTarget)
            {
                i++;

                m_transform.rotation = Quaternion.Euler(transform.rotation.x, originalRot+i, transform.rotation.z);
            }
        }
    }
    public void OnTriggerEnter(Collider feet)
    {
        if (feet.gameObject.tag == "Feet" && !kicked)
        {
            originalRot = (m_transform.rotation.y)*360/(3.14f*2);
            kicked=true;
        }
    }
}
