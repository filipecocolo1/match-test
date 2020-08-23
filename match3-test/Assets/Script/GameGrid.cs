
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;


public class GameGrid : MonoBehaviour
{
    public int xSize, ySize;
    public GameObject[] _comida;
    public float C_Widy;
    private RedeDeItem[,] _items;
    private RedeDeItem _itemAtualmenteAtivo;
    public static int minitensForMACH = 3;
    // Start is called before the first frame update
    void Start()
    {
        GetComida();
        fillGrid();
        RedeDeItem.OnMouseOverItemEventHandler += OnMouseOverItem;
        List<RedeDeItem> machForItemns = SearchVertically(_items[3, 3]);
        if (machForItemns.Count >= 3)
        {

            Debug.Log("Há um match Valido para um indice 3x3");


        }
        else
        {
            Debug.Log("Não Ha um match valido para indice 3x3");
        }
    }
    void OnDisable()
    {

        RedeDeItem.OnMouseOverItemEventHandler -= OnMouseOverItem;
    }
    void fillGrid()
    {
        _items = new RedeDeItem[xSize, ySize];
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                _items[x, y] = (InstantieteComida(x, y));
            }
        }
    }

    //MachInfo GetMachInformation(RedeDeItem intem)
    //{
    //    MachInfo m = new MachInfo();
    //    m.mach = null;
    //    List<RedeDeItem> hMach = SearchHorizontally(intem);
    //    List<RedeDeItem> vMach = SearchVertically(intem);
    //    if (hMach.Count >= minitensForMACH && hMach.Count > vMach.Count)
    //    {
    //        m.machStartX = GetMinimumX(hMach);
    //        m.machEndingX = GetMaximumX(hMach);
    //        m.machStartY = m.machEndingY = hMach[0].y;

    //        m.mach = hMach;
    //        //Definir informação  para mach horizontal 

    //    }
    //    else if (vMach.Count >= minitensForMACH)
    //    {
    //        m.machStartY = GetMinimumY(vMach);
    //        m.machEndingY = GetMaximumY(vMach);
    //        m.machStartX= m.machEndingX = hMach[0].x;

    //        m.mach = vMach;

    //    }
    //    return m;
    //}
    //int GetMinimumX(List<RedeDeItem> items)
    //{
    //    float[] indices = new float[items.Count];
    //    for (int i = 0; i < indices.Length; i++)
    //    {
    //        indices[i] = items[i].x;
    //    }
    //    return (int)Mathf.Min(indices);
    //}

    //int GetMaximumX(List<RedeDeItem> items)
    //{
    //    float[] indices = new float[items.Count];
    //    for (int i = 0; i < indices.Length; i++)
    //    {
    //        indices[i] = items[i].x;
    //    }
    //    return (int)Mathf.Max(indices);
    //}
    //int GetMinimumY(List<RedeDeItem> items)
    //{
    //    float[] indices = new float[items.Count];
    //    for (int i = 0; i < indices.Length; i++)
    //    {
    //        indices[i] = items[i].y;
    //    }
    //    return (int)Mathf.Min(indices);
    //}
    //int GetMaximumY(List<RedeDeItem> items)
    //{
    //    float[] indices = new float[items.Count];
    //    for (int i = 0; i < indices.Length; i++)
    //    {
    //        indices[i] = items[i].y;
    //    }
    //    return (int)Mathf.Max(indices);
    //}

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

            _itemAtualmenteAtivo = null;
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

    List<RedeDeItem> SearchHorizontally(RedeDeItem item)
    {
        List<RedeDeItem> hItemns = new List<RedeDeItem> { item };
        int left = item.x - 1;
        int right = item.x + 1;
        while (left >= 0 && _items[left, item.y].id == item.id)
        {
            hItemns.Add(_items[left, item.y]);
            left--;
        }
        while (right < xSize && _items[right, item.y].id == item.id)
        {
            hItemns.Add(_items[right, item.y]);
            right++;

        }

        return hItemns;

    }
    List<RedeDeItem> SearchVertically(RedeDeItem item)
    {
        List<RedeDeItem> vItemns = new List<RedeDeItem> { item };
        int lower = item.y - 1;
        int upper = item.y + 1;
        while (lower >= 0 && _items[item.x, lower].id == item.id)
        {
            vItemns.Add(_items[item.x, lower]);
            lower--;

        }
        while (upper < ySize && _items[item.x, upper].id == item.id)
        {
            vItemns.Add(_items[item.x, upper]);
            upper++;

        }
        return vItemns;
    }


}
