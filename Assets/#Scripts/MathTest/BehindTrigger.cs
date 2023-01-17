using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehindTrigger : MonoBehaviour
{
    public float LookTrigLimit = 0.8f;
    public Transform Enemy;
    private void OnDrawGizmos()
    {
        Vector2 lookDir = transform.up;

        //Transform is at world origin. Therefore, we do not need to make the transform, origin for the enemy obj.
        //Vector2 EnemyPos = Enemy.position - transform.position;
        Vector2 EnemyPos = Enemy.position;

        //Enemy Normalisation Manually
        float eLen = Mathf.Sqrt(EnemyPos.x * EnemyPos.x + EnemyPos.y * EnemyPos.y);
        Vector2 enemyNormal = EnemyPos / eLen;

        //Two normalised vector will give value between -1 and 1.
        float lookTrig = Vector2.Dot(lookDir, enemyNormal);
        //If its negative, then it means object behind. Relative to look direction.
        bool lookTriggered = lookTrig < LookTrigLimit;

        Gizmos.color = lookTriggered ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, enemyNormal);

        Gizmos.color = Color.white;
        Gizmos.DrawLine(transform.position, lookDir);
        Gizmos.DrawLine(transform.position, transform.right);
        Gizmos.DrawLine(transform.position, -transform.right);
    }
}
