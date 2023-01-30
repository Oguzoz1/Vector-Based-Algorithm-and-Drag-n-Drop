using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationMatrix : MonoBehaviour
{
    public Transform obj;
    public Vector2 localTransform;

    private void OnDrawGizmos()
    {
        //Origin Point.
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(Vector2.zero, .05f);

        //cartesian basis vectors for origin
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Vector2.zero, Vector2.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector2.zero, Vector2.right);

        //Obj Point
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(obj.position, .05f);

        //cartesian basis vectors for obj
        Gizmos.color = Color.green;
        Gizmos.DrawLine(obj.position, obj.position + obj.up);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(obj.position, obj.position + obj.right);

        //Origin to Obj
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(Vector2.zero, obj.position);

        //Local To World Point
        Vector2 localToWorld = LocalToWorld();
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(localToWorld, .1f);

        //cartesian basis vectors for localPoint
        Gizmos.color = Color.green;
        Gizmos.DrawLine(localToWorld, localToWorld + new Vector2(0, localToWorld.normalized.y));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(localToWorld, localToWorld + new Vector2(localToWorld.normalized.x, 0));
    }

    Vector2 LocalToWorld()
    {
        Matrix4x4 localMatrix = Matrix4x4.identity;
        localMatrix.SetRow(0, new Vector3(obj.right.x, obj.up.x, obj.position.x));
        localMatrix.SetRow(1, new Vector3(obj.right.y, obj.up.y, obj.position.y));

        return localMatrix.MultiplyVector(new Vector3(localTransform.x, localTransform.y, 1));
    }
}
