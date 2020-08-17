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
    private RedeDeItem _itemAtualmenteAtivo;
    // Start is called before the first frame update
    void Start()
    {
        GetComida();
        fillGrid();
        RedeDeItem.OnMouseOverItemEventHandler += OnMouseOverItem;

    }
   void OnDisable (){

        RedeDeItem.OnMouseOverItemEventHandler -= OnMouseOverItem;
    }
    void fillGrid()
    {
        _items = new RedeDeItem[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                _items[x, y] =(InstantieteComida(x, y));
            }
        }
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
   
    void OnMouseOverItem(RedeDeItem item)
    {
        if (_itemAtualmenteAtivo == item)
        {
            _itemAtualmenteAtivo = null;
            return;
        }
        if (_itemAtualmenteAtivo == null)
        {
            _itemAtualmenteAtivo = item;
        }
        else
        {
            float xDiff = Mathf.Abs(item.x - _itemAtualmenteAtivo.x);
            float yDiff = Mathf.Abs(item.y - _itemAtualmenteAtivo.y);
            if (xDiff + yDiff == 1)
            {
                StartCoroutine(Swap(_itemAtualmenteAtivo, item));
                
                //Permetir Swap
            }
           
            else
            {
                Debug.LogError("Eles estão a mais de uma unidade longe um do outro");
                // Negar Swap
            }

            _itemAtualmenteAtivo.
        }
    }

    IEnumerator Swap(RedeDeItem a, RedeDeItem b)
    {
        TrocarRigiboodyStatus(false);// Desativar todos os Corpos Rigidos
        float movDuration = 0.3f;
        Vector3 aPosition = a.transform.position;
        StartCoroutine(a.transform.Move(b.transform.position, movDuration));
        StartCoroutine(b.transform.Move(aPosition, movDuration));
        yield return new WaitForSeconds(movDuration);
        TrocarRigiboodyStatus(true);// Desativar todos os Corpos Rigidos
        _itemAtualmenteAtivo = null;
    }
    void TrocarRigiboodyStatus(bool status)
    {

        foreach (RedeDeItem g in _items)
        {
            g.GetComponent<Rigidbody2D>().isKinematic = !status;
            //if (status)
            //{
            //    g.GetComponent<Rigidbody2D>().isKinematic = true;
            //}
            //else
            //{



            //}


        }






    }




}
