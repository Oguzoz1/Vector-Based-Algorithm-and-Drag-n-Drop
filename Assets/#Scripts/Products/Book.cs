using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : Product
{
    [SerializeField] private BookType _type;
    public enum BookType
    {
        COOK,
        EDUCATION,
        NOVEL
    }
    public BookType GetTypeOf()
    {
        return _type;
    }
}
