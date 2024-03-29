using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistanceToAction : MonoBehaviour
{
    [Header("Points of Interest")]
    [SerializeField] private Transform shelf;
    [SerializeField] private Transform checkOut;

    //Rough and strict way of checking but non-harmful due to project size.
    public bool CanDrag()
    {
        if (Vector2.Distance(transform.position, shelf.position) <= 2f) return true;
        else return false;
    }
    public bool canCheckOut()
    {
        if (Vector2.Distance(transform.position, checkOut.position) <= 2f) return true;
        else return false;
    }
}
