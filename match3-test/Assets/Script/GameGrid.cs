using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    public int xSize, ySize;
    public GameObject[] _comida;
    public float C_Widy;
    private RedeDeItem[,] _items;
    // Start is called before the first frame update
    void Start()
    {
        GetComida();
        fillGrid();

    }
    void fillGrid()
    {
        _items = new RedeDeItem[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                InstantieteComida(x, y);


            }



        }


        //}
    }
    void GetComida()
    {
        _comida = Resources.LoadAll<GameObject>("Prefabs");

        for (int i = 0; i < _comida.Length; i++)
        {
            _comida[i].GetComponent<RedeDeItem>().id = i;
        }


    }
    RedeDeItem InstantieteComida(int x, int y)
    {
        GameObject randomcomidas = _comida[Random.Range(0, _comida.Length)];
        RedeDeItem newComida = ((GameObject)Instantiate(randomcomidas, new Vector3(x * C_Widy, y), Quaternion.identity)).GetComponent<RedeDeItem>();
        newComida.PosicaoDoItemAlterada(x, y);
        return newComida;
    }

    void TrocarRigiboodyStatus(bool status)
    {

        foreach (RedeDeItem g in _items)
        {

            x''
        }






    }
}
