using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingLaser : MonoBehaviour
{
    public int bounceAmount = 3;
    RaycastHit hit;
    private void OnDrawGizmos()
    {
        //Reflect();
        Ref();
    }
    void Reflect()
    {
        //Calculations were made without a formula. Offset was directed wrong in this method. 
        Vector2 startPos = transform.position;
        Vector2 startDir = transform.up;

        for (int i = 0; i < bounceAmount; i++)
        {
            if (Physics.Raycast(startPos, startDir, out hit, Mathf.Infinity))
            {
                Gizmos.color = Color.white;
                Gizmos.DrawRay(startPos, startDir * hit.distance);

           
                Vector2 rayDir = hit.point;
                Vector2 VectorProjection = (Vector2.Dot(rayDir, hit.normal.normalized) * hit.normal.normalized);
                Vector2 reflectionToRight = rayDir - (2 * VectorProjection);//if reflected vector is not multiplied with 2, new vector is going to be flatened towards projected direction.
                Vector2 reflectionToLeft = rayDir - (2 * VectorProjection) * -1f;

                float scalarProj = Vector2.Dot(rayDir, hit.normal.normalized);

                Gizmos.color = Color.green;
                Gizmos.DrawRay(hit.point, hit.normal.normalized);

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(rayDir, 0.05f);

                startPos = rayDir;
                startDir = scalarProj < 0 ? reflectionToRight.normalized : reflectionToLeft;

                if (i == bounceAmount - 1)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawRay(startPos, startDir * reflectionToRight.magnitude);

                }
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawRay(startPos, startDir * 100);
            }
        }
    }
    void Ref()
    {
        Vector2 startPos = transform.position;
        Vector2 startDir = transform.up;
        Ray ray = new Ray(startPos, startDir);

        for (int i = 0; i < bounceAmount; i++)
        {
            if (Physics.Raycast(ray, out hit))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawLine(ray.origin, hit.point);
                Gizmos.DrawSphere(hit.point, 0.1f);
                Vector2 reflected = Reflect(ray.direction, hit.normal);
                Gizmos.color = Color.white;
                Gizmos.DrawLine(hit.point, (Vector2)hit.point + reflected);
                ray.direction = reflected;
                ray.origin = hit.point;
            }
            else break;
        }
    }
    Vector2 Reflect(Vector2 inDir, Vector2 n)
    {
        float proj = Vector2.Dot(inDir, n);
        //Vector Reflection Formula d-2(d.n)n;
        //Basically, offsetting Vector towards the reflected pos.
        //Scalar is Dot Product to scale the normalised vector n(surface normal).
        //Scaling twice since once would give the vector on basis vector x. Twice would raise vector head towards the reflected pos.
        return inDir - 2 * proj * n;
    }
}
