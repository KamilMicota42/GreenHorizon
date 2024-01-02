using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;
    
    void Update()
    {
        if(aiPath.desiredVelocity.x >= 0.1f)
        {
            transform.localScale = new Vector3(-5f, 5f, 1f);
        } else if(aiPath.desiredVelocity.x <= -0.1f)
        {
            transform.localScale = new Vector3(5f, 5f, 1f);
        }
    }
}
