using System.Collections;
using UnityEngine;

public class FrozenZone : ActiveSkill
{
    public GameObject freezingObjPrefab;
    public float freezeTime;
    public float freezeArea;
    public override IEnumerator OnUse()
    {
        yield return base.OnUse();
        Debug.Log($"Using FrozenZone.");
        initFreeze();
    }

    void initFreeze(){
        GameObject freezeObj = Instantiate(freezingObjPrefab , Player.Instance.transform.position , Quaternion.identity);
        Freezer freezer = freezeObj.GetComponent<Freezer>();
        freezer.aliveTime = freezeTime;
        freezer.range = freezeArea;
        freezer.SetScale();
    }
}
