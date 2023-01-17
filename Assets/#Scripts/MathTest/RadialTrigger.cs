using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    public Transform Enemy;
    public float DistanceTrigger;

    private void OnDrawGizmos()
    {
        Vector2 playerPos = transform.position;
        Vector2 enemyPos = Enemy.position;
        Vector2 distance = enemyPos - playerPos;
        float magn = Mathf.Sqrt(distance.x * distance.x + distance.y * distance.y);
        Color col = magn < DistanceTrigger ? Color.green : Color.red;
        DrawCircle(col);

        Gizmos.DrawLine(playerPos, enemyPos);
    }
    void DrawCircle(Color colour)
    {
        float corners = 32; // How many corners the circle should have
        float radius = DistanceTrigger; // How wide the circle should be

        Vector3 origin = transform.position; // Where the circle will be drawn around
        Vector3 startRotation = transform.right * radius; // Where the first point of the circle starts
        Vector3 lastPosition = origin + startRotation;

        Gizmos.color = colour;
        float angle = 0;
        while (angle <= 360)
        {
            angle += 360 / corners;
            Vector3 nextPosition = origin + (Quaternion.Euler(0, 0, angle) * startRotation);
            Gizmos.DrawLine(lastPosition, nextPosition);
            Gizmos.DrawSphere(nextPosition, .01f);

            lastPosition = nextPosition;
        }
    }
}