using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropArea : MonoBehaviour
{
    [Header("Drop Area Settings")]
    public Transform startingPoint;
    public float productSpaceRange = 2;
    public int amountPerRow = 2;
    public int rowAmount = 1;

    public float PlaceDistance { get; private set; } = 5f;
    public List<GameObject> _products { get; set; } = new List<GameObject>();
    public Vector2 ListPos(GameObject product)
    {
        //Visually Listing Products
        if (!_products.Contains(product))
            _products.Add(product);
        float spacingMod = _products.IndexOf(product);
        float rowMod = 0;
        float columnMod = 0;

        //Checking item's location in the rows;
        if (spacingMod >= amountPerRow)
        {
            //Setting rowmod according to amount that will be nested in a row.
            rowMod = (int)Mathf.Floor(spacingMod / amountPerRow);
            //Setting spacing mod regarding if a row is full or not.
            if (spacingMod % amountPerRow == 0) spacingMod -= spacingMod;
            else spacingMod -= (spacingMod - (spacingMod % amountPerRow));
        }
        if(rowMod >= rowAmount)
        {
            columnMod = (int)Mathf.Floor(rowMod / rowAmount);
            if (rowMod % rowAmount == 0) rowMod -= rowMod;
            else rowMod -= (rowMod - (rowMod % rowAmount));
        }
        //Giving position of a starting pos.
        Vector2 desired = startingPoint.position;
        product.transform.parent = startingPoint;
        //Editing current vector2 desire, from startingpos by using algorithm above.
        desired += Vector2.right * spacingMod * productSpaceRange;
        if (rowMod != 0) desired += Vector2.right * rowMod * productSpaceRange;
        if (columnMod != 0) desired += Vector2.down * columnMod * productSpaceRange;
        return desired;
    }
    public void ResetList()
    {
        //Method to re-list everything.
        for (int i = 0; i < _products.Count; i++)
        {
            float spacingMod = i;
            float rowMod = 0;
            float columnMod = 0;

            //Checking item's location in the rows;
            if (spacingMod >= amountPerRow)
            {
                //Setting rowmod according to amount that will be nested in a row.
                rowMod = (int)Mathf.Floor(spacingMod / amountPerRow);
                //Setting spacing mod regarding if a row is full or not.
                if (spacingMod % amountPerRow == 0) spacingMod -= spacingMod;
                else spacingMod -= (spacingMod - (spacingMod % amountPerRow));
            }
            if (rowMod >= rowAmount)
            {
                columnMod = (int)Mathf.Floor(rowMod / rowAmount);
                if (rowMod % rowAmount == 0) rowMod -= rowMod;
                else rowMod -= (rowMod - (rowMod % rowAmount));
            }
            //Giving position of a starting pos.
            Vector2 desired = startingPoint.position;
            desired += Vector2.right * spacingMod * productSpaceRange;
            if (rowMod != 0) desired += Vector2.right * rowMod * productSpaceRange;
            if (columnMod != 0) desired += Vector2.down * columnMod * productSpaceRange;
            _products[i].transform.position = desired;
        }

    }
    public Vector2 ResetPos(GameObject product, Vector2 originalPos)
    {
        if (_products.Contains(product))
        {
            _products.Remove(product);
            ResetList();
            return originalPos;
        }
        else return originalPos;
    }
}