using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollision : MonoBehaviour
{
    public bool inWater;

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" )inWater = true;
        other.gameObject.GetComponent<PlayerMovement>().inWater = true;//这样可以直接修改玩家的状态！！！
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") inWater = false;
        other.gameObject.GetComponent<PlayerMovement>().inWater = false;
    }
}