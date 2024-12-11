using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour 
{
    public List<Enemy> targetSelecting;
    public GameObject mouseObject;  
    void Update()
    {
        DebugInput();
    }

    public void DebugInput(){
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 newPosition = new Vector2(worldPosition.x, worldPosition.y);
        mouseObject.transform.position = newPosition;
    }
    
    public void AddEnemy(){

    }

    public void ClearTarget(Enemy enemy){

    }
    public void ClearAllTarget(){
        
    }

}
