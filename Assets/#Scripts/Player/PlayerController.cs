using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerDistanceToAction))]
public class PlayerController : MonoBehaviour
{
    [Header("Controller Settings")]
    [SerializeField] private float xBorder;
    [SerializeField] private float _speed = 10;

    #region Unity Main
    void Update() => MainLogic();
    #endregion
    #region Main Logic
    void MainLogic()
    {
        TransformMovement();
    }
    void TransformMovement()
    {
        //Unorthodox but visually appealing way of restricting movement
        bool canMoveRight = transform.position.x <= xBorder;
        bool canMoveLeft = transform.position.x >= -xBorder;

        if (Input.GetKey(KeyCode.A) && canMoveLeft)
        {
            float speed = _speed * Time.deltaTime;
            Vector3 desired = Vector2.left * speed;
            transform.position += desired;
        }
        if (Input.GetKey(KeyCode.D) && canMoveRight)
        {
            float speed = _speed * Time.deltaTime;
            Vector3 desired = Vector2.right * speed;
            transform.position += desired;
        }
    }
    #endregion
}
