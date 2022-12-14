using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine : Product
{
    [SerializeField] private MagazineType _type;
    public enum MagazineType
    {
        BUSINESS,
        SPORTS
    }
    public MagazineType GetTypeOf()
    {
        return _type;
    }

}
