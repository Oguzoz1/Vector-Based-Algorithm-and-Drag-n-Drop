using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertLocalToWorldPos : MonoBehaviour
{
    public Vector2 localCoordinates;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(LocalToWorld(localCoordinates), .1f);
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
