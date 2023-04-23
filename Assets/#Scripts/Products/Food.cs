using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Product
{
    [SerializeField] private FoodType _type;
    public enum FoodType
    {
        FRUIT,
        VEGETABLE,
        GRAIN,
        MEAT,
        DAIRY,
        SWEET
    }
    public FoodType GetTypeOf()
    {
        return _type;
    }

}