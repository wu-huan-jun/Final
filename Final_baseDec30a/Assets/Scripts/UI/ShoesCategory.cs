using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoesCategory : MonoBehaviour
{
    public KeyCode shoeCategoryKeyLeft = KeyCode.Q;
    public KeyCode shoeCategoryKeyRight = KeyCode.E;
    public KeyCode shoeCategoryKeyEscape = KeyCode.Escape;
    [Header("Groups")]
    [SerializeField] private CanvasGroup Main;
    [SerializeField] private CanvasGroup Left;
    [SerializeField] private CanvasGroup Right;
    [SerializeField] private bool b_Main = false;
    [SerializeField] private bool b_Left = false;
    [SerializeField] private bool b_Right =false;

    // Start is called before the first frame update
    void Start()
    {
        Main.alpha = 0f;
        Left.alpha = 0f;
        Right.alpha = 0f;
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
