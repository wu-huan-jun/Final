using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//控制角色根据跟随相机旋转
public class T3rdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    public Transform playerObj;
    public Rigidbody rb;

    public float rotationSpeed;
    // Start is called before the first frame update
    [Header("CameraStyle")]
    public Transform combatLookAt;
    public CameraStyle currentStyle;
    public enum CameraStyle { Basic, Combat, TopDown, FPV }//第一人称相机和第三人称不太一样，这里先留好接口
    public GameObject T3rdCam;
    public GameObject CombatCam;
    public GameObject T3rdTopDownCam;
    public GameObject FPVCam;
    public GameObject FPVholder;
    public GameObject PlayerMesh;
    public float switchCoolDown = 300f;
    [Header("Debug")]
    public float switchCoolDown_;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//隐藏鼠标
        switchCoolDown_ = switchCoolDown;
        
        //开场禁用相机
        CombatCam.SetActive(false);
        T3rdCam.SetActive(true);
        T3rdTopDownCam.SetActive(false);
        FPVCam.SetActive(false);
        FPVholder.SetActive(false);
        PlayerMesh.SetActive(true);
    }


    void SwitchCameraStyle(CameraStyle newStyle)//
    {
        CombatCam.SetActive(false);//这样启用或禁用某个cam
        T3rdCam.SetActive(false);
        T3rdTopDownCam.SetActive(false);
        FPVCam.SetActive(false);
        FPVholder.SetActive(false);
        PlayerMesh.SetActive(true);

        if (newStyle == CameraStyle.Basic)
            T3rdCam.SetActive(true);
        if (newStyle == CameraStyle.Combat)
            CombatCam.SetActive(true);
        if (newStyle == CameraStyle.TopDown)
            T3rdTopDownCam.SetActive(true);
        if (newStyle == CameraStyle.FPV)
        {
            FPVCam.SetActive(true);
            FPVholder.SetActive(true);
            PlayerMesh.SetActive(false);
        }

        currentStyle = newStyle;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        //获取镜头朝向
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

        if (currentStyle == CameraStyle.Basic || currentStyle == CameraStyle.TopDown)
        {//普通第三人称
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");//Input.GetAxis里的函数要大写
            Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

            if (inputDir != Vector3.zero)
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);//插值旋转
        }
        else if(currentStyle == CameraStyle.Combat)
        {
            Vector3 dirToCombatLookAt = combatLookAt.position - new Vector3(transform.position.x, combatLookAt.position.y, transform.position.z);
            orientation.forward = dirToCombatLookAt.normalized;

            player.forward = dirToCombatLookAt.normalized;
        }
        switchCoolDown_ -= 1f;
        //按下v切换视角
        if (Input.GetKey(KeyCode.V) && switchCoolDown_ < 0)
        {
            switchCoolDown_ = switchCoolDown;
            if (currentStyle == CameraStyle.Basic)
                SwitchCameraStyle(CameraStyle.TopDown);
            else if (currentStyle == CameraStyle.TopDown)
                SwitchCameraStyle(CameraStyle.Basic);
            else if (currentStyle == CameraStyle.FPV)
                SwitchCameraStyle(CameraStyle.Basic);
        }
    }
}
