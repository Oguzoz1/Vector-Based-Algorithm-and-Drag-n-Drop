using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DragPiece : MonoBehaviour
{
    PlayerDistanceToAction playerActionDistance;
    Camera cam;

    private bool _isDragged;
    private Vector2 _mouseOffset;
    private Vector2 _originalPosition;
    private DropArea[] dropAreas;

    #region Unity Main
    private void Start() => InitializeScript();
    private void Update() => MainLogic();
    private void OnMouseDown()
    {
        _isDragged = true;
        _mouseOffset = GetMousePos() - (Vector2)transform.position;
    }
    private void OnMouseUp()
    {
        Drop();
    }
    #endregion
    #region Initializers
    void InitializeScript()
    {
        InitializeReferences();
        InitializeValues();
    }
    void InitializeReferences()
    {
        cam = Camera.main;

        //Two below references should use a global manager to hold references to not to use same method again.
        playerActionDistance = FindObjectOfType<PlayerDistanceToAction>(); //=> Lacks optimization
        dropAreas = FindObjectsOfType<DropArea>(); //=> Lacks optimization
    }
    void InitializeValues()
    {
        _originalPosition = transform.position;
    }
    #endregion
    #region Main Logic
    void MainLogic()
    {
        //Restricting drag by checking if player close to shelf.
        if(playerActionDistance.CanDrag()) Drag();
    }
    void Drag()
    {
        if (!_isDragged) return;

        //OnMouseButtonDown, object's transform will tranfer to mousePos. However, mousePos won't be centralised with the offset abstraction.
        transform.position = GetMousePos() - _mouseOffset;
    }
    void Drop()
    {
        //Between all dropareas, it can be dynamically dropped and taken.
        _isDragged = false;
        if (GetDropArea() == null) ResetPosInAllAreas();
        else if (Vector2.Distance(transform.position, GetDropArea().transform.position) <= GetDropArea().PlaceDistance) SetPosForArea(GetDropArea());
        else transform.position = GetDropArea().ResetPos(gameObject, _originalPosition);
    }
    #endregion
    #region Methods
    void ResetPosInAllAreas()
    {
        //To Reset lists in all areas.
        foreach (DropArea dropArea in dropAreas) transform.position = dropArea.ResetPos(gameObject, _originalPosition);
    }
    void SetPosForArea(DropArea area)
    {
        //To Reset Lists in all areas except designated one.
        transform.position = area.ListPos(gameObject);
        foreach (DropArea dropArea in dropAreas) if (dropArea != area) dropArea.ResetPos(gameObject, _originalPosition);
    }
    DropArea GetDropArea()
    {
        DropArea x = null;
        foreach (DropArea dropArea in dropAreas)
            if (Vector2.Distance(dropArea.transform.position, transform.position) <= 2f) x = dropArea;
        return x;
    }
    Vector2 GetMousePos()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
}