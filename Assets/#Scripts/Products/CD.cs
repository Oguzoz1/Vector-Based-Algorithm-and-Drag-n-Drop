using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CD : Product
{
    [SerializeField] private CDType _type;
    public enum CDType
    {
        MUSIC,
        MOVIE
    }
    public CDType GetTypeOf()
    {
        return _type;
    }
}
