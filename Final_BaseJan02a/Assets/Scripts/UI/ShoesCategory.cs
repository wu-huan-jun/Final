using System.Collections;
using System.Reflection;  
using System.Collections.Generic;
using UnityEngine;

public class ShoesCategory : MonoBehaviour
{
    [Header("KeyCodes")]
    public KeyCode shoeCategoryKeyLeft = KeyCode.Q;
    public KeyCode shoeCategoryKeyRight = KeyCode.E;
    public KeyCode categoryOneKey = KeyCode.Alpha1;
    public KeyCode categoryTwoKey = KeyCode.Alpha2;
    public KeyCode categoryThreeKey = KeyCode.Alpha3;
    public KeyCode shoeCategoryKeyEscape = KeyCode.Escape;

    [Header("Categories")]
    public string Left1 = "None";
    public string Left2 = "None";
    public string Left3 = "None";
    public string Right1 = "None";
    public string Right2 = "None";
    public string Right3 = "None";

    [Header("CanvasGroups")]
    [SerializeField] private CanvasGroup Main;
    [SerializeField] private CanvasGroup Left;
    [SerializeField] private CanvasGroup Right;
    [SerializeField] private bool b_Main = false;
    [SerializeField] private bool b_Left = false;
    [SerializeField] private bool b_Right =false;
    public GameObject Self ;
    public GameObject Player;
    // Start is called before the first frame update
    
    void Start()
    {
        Main.alpha = 0f;
        Left.alpha = 0f;
        Right.alpha = 0f;
        PlayerPrefs.SetString("Left1", "None"); 
        PlayerPrefs.SetString("Left2", "None"); 
        PlayerPrefs.SetString("Left3", "None"); 
        PlayerPrefs.SetString("Right1", "None"); 
        PlayerPrefs.SetString("Right2", "None"); 
        PlayerPrefs.SetString("Right3", "None"); 
    }
    
    private void KeyInput()
    {
        if(!b_Main && (Input.GetKeyDown(shoeCategoryKeyLeft) || Input.GetKeyDown(shoeCategoryKeyRight)))
        {
            b_Main = true;
        }
        if (b_Main)
        {
            if ((Input.GetKeyDown(shoeCategoryKeyEscape)))
            {
                b_Main = false;
            }
            if ((Input.GetKeyDown(shoeCategoryKeyLeft)))
            {
                b_Left = true;
                b_Right = false;
            }
            if ((Input.GetKeyDown(shoeCategoryKeyRight)))
            {
                b_Left = false;
                b_Right = true;
            }
        }
        if (b_Left)
        {
            if (Input.GetKeyDown(categoryOneKey))
            {
                PlayerPrefs.SetString("LeftShoe",PlayerPrefs.GetString("Left1"));
                Player.GetComponent<PlayerMovement>().refreshShoes = true;
            }
            if (Input.GetKeyDown(categoryTwoKey))
            {
                PlayerPrefs.SetString("LeftShoe", PlayerPrefs.GetString("Left2"));
                Player.GetComponent<PlayerMovement>().refreshShoes = true;
            }
            if (Input.GetKeyDown(categoryThreeKey))
            {
                PlayerPrefs.SetString("LeftShoe", PlayerPrefs.GetString("Left3"));
                Player.GetComponent<PlayerMovement>().refreshShoes = true;
            }
        }
        if (b_Right)
        {
            if (Input.GetKeyDown(categoryOneKey))
            {
                PlayerPrefs.SetString("RightShoe", PlayerPrefs.GetString("Right1"));
                Player.GetComponent<PlayerMovement>().refreshShoes = true;
            }
            if (Input.GetKeyDown(categoryTwoKey))
            {
                PlayerPrefs.SetString("RightShoe", PlayerPrefs.GetString("Right2"));
                Player.GetComponent<PlayerMovement>().refreshShoes = true;
            }
            if (Input.GetKeyDown(categoryThreeKey))
            {
                PlayerPrefs.SetString("RightShoe", PlayerPrefs.GetString("Right3"));
                Player.GetComponent<PlayerMovement>().refreshShoes = true;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        KeyInput();
        if(b_Main && Main.alpha < 1)
        {
            UIManager.FadeIn(Main, 5f, true);
        }
        if(!b_Main && Main.alpha > 0)
        {
            UIManager.FadeOut(Main, 5f, true);
            b_Left = false ;
            b_Right = false ;
        }

        if (b_Left && Left.alpha < 1)
        {
            UIManager.FadeIn(Left, 5f, true);
        }
        if(!b_Left && Left.alpha > 0)
        {
            UIManager.FadeOut(Left, 5f, true);
        }

        if (b_Right && Right.alpha < 1)
        {
            UIManager.FadeIn(Right, 5f, true);
        }
        if(!b_Right && Right.alpha > 0)
        {
            UIManager.FadeOut(Right, 5f, true);
        }
    }
}
