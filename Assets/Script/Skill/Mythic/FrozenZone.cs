using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class FrozenZone : ActiveSkill
{
    public GameObject freezingObjPrefab;
    public float freezeTime;
    public float freezeArea;
    public override IEnumerator OnUse()
    {
        //yield return base.OnUse();
        if (!isActive) yield break;

        isActive = false;

        if (Player.Instance == null)
        {
            isActive = true;
            yield break;
        }

        Debug.Log($"Using FrozenZone.");
        initFreeze();

        yield return new WaitForSeconds(coolDown);

        isActive = true;
        
    }

    void initFreeze(){
        GameObject freezeObj = Instantiate(freezingObjPrefab , Player.Instance.transform.position , Quaternion.identity);
        Freezer freezer = freezeObj.GetComponent<Freezer>();
        freezer.aliveTime = freezeTime;
        freezer.range = freezeArea;
        freezer.SetScale();
    }
}
