using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DragPiece))]
public class Product : MonoBehaviour
{
    [Header("Product Settings")]
    [SerializeField] private string _name;
    [SerializeField] private float _price;

    //Instead of methods, I'd use properties or encapsulations like
    // get { return _name; } set { _name = value;}

    public string GetName()
    {
        return _name;
    }
    public float GetPrice()
    {
        return _price;
    }
}
