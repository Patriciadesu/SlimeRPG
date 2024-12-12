using System;
using System.Collections;
using UnityEngine;

public class BossEvent : Event
{
    public Enemy BossAsEnemy;
    public BossNPC BossAsNPC;
    public Vector2 BossSpawnPoint;
    private void Awake()
    {
        
    }
    public override IEnumerator StartEvent()
    {
        throw new System.NotImplementedException();
    }
    public override void EndEvent()
    {
        throw new System.NotImplementedException();
    }

}
