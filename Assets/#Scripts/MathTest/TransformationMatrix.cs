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

    void TransformationMatrixUsage()
    {
        Matrix4x4 localToWorldMtx = transform.localToWorldMatrix;

        // Functions to do space transformations between local and world.
        // transform.TransformPoint() // M*(v.x, v.y, v.z, 1) -> local to world func: localtoworldmatrix * our vector, 1/0
        // transform.InverseTransformPoint () // M^1*(v.x, v.y, v.z, 1) -> world to local func:


        //Freya Example: Assuming we need an offset for a weapon to spawn a bullet. Offset location will be relative to position of the weapon.
        //Thus, offset must be transformed from local to world to be able to spawn.
        //transform.TransformPoint(localOffset); is used to determine this.

        //What direction an object moving in relative to another object:
        //From Object B to object A, direction vector is in a world position. We might want that in local space.
        //Thus, we would know exacty x and y relative to Object B.
        //Such as crew wants to know the location of enemy relative to their ship.
        // transform.TransformVector(direction vector world); // M*(v.x, v.y, v.z, 0)
    }
}
