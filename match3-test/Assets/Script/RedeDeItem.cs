using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedeDeItem : MonoBehaviour
{
    public int x
    {
        get;
        private set;

    }
    public int y
    {
        get;
        private set;


    }
    [HideInInspector]
    public int id;
    internal object rigidbody2d;

    public void PosicaoDoItemAlterada(int newX, int newY)
    {

        x = newX;
        y = newY;
        gameObject.name = string.Format("Sprite[{0}] [{1}]", x, y);
    }
    void OnMouseDown()
    {
        if (OnMouseOverItemEventHandler != null) {

            OnMouseOverItemEventHandler(this);
        
        }
    }
    public delegate void OnMouseOverItem(RedeDeItem item);
    public static event OnMouseOverItem OnMouseOverItemEventHandler;

}