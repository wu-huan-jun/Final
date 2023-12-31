using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    public GameObject Timeline;

    // Start is called before the first frame update
    void Start()
    {
        Timeline.SetActive(false);
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.tag == "Player" )
        {
            Timeline.SetActive(true);
            
        }
    }
}
