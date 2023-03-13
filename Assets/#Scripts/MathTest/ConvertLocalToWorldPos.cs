using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertLocalToWorldPos : MonoBehaviour
{
    public Vector2 localCoordinates;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector2.zero, Vector2.right);
        Gizmos.DrawLine(transform.position, transform.position + transform.right);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up);
        Gizmos.DrawLine(Vector2.zero, Vector2.up);




        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(LocalToWorld(localCoordinates), .05f);
    }
    Vector2 LocalToWorld(Vector2 local)
    {
        //Conversion of local coordinates to world.
        Vector2 position = transform.position;
        //Scaling Local Vector relative to object position vector basis x and basis y vectors.
        //Scaling is multplying scalar of local coordinates and basis X and Y of object transform.
        position += (local.x * (Vector2)transform.right);
        position += (local.y * (Vector2)transform.up);
        //Eventually adding the scaled/stretched vector to the transform.position, will give us the world pos.
        return position;
    }
}
