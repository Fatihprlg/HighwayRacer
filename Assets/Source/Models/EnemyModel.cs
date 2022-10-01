using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : ObjectModel
{
    public int level;
    
    [SerializeField] private SplineFollower follower;

    public void OnCollidedWithPlayer()
    {

    }
    public void OnReachFinish()
    {
        follower.Restart();
    }
}
