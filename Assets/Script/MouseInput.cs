using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public static MouseInput Instance;

    public List<Enemy> targetSelecting;
    public Vector2 MousePos
    {
        get
        {
            return _mousePos;
        }
    }
    private Vector2 _mousePos;
    public GameObject mousePrefab;
    GameObject mouse;

    void Awake()
    {
        if (Instance != null)
            Destroy(Instance.gameObject);

        targetSelecting = new List<Enemy>();

        mouse = Instantiate(mousePrefab , transform);

        Instance = this;
    }
    void Update()
    {
        DebugInput();
    }

    public void DebugInput(){
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        Vector2 newPosition = new Vector2(worldPosition.x, worldPosition.y);

        _mousePos = newPosition;

        mouse.transform.position = newPosition;

        if (Input.GetMouseButtonDown(0))
        {
            AddEnemy();
        }
    }
    
    public void AddEnemy()
    {
        var rays = Physics2D.CircleCastAll(_mousePos, 0.1f, Vector2.zero, 0, LayerMask.GetMask("Enemy"));

        foreach (var ray in rays)
        {
            if (ray.collider != null && ray.collider.TryGetComponent(out Enemy enemy))
            {
                if (!targetSelecting.Contains(enemy))
                {
                    targetSelecting.Add(enemy);
                }
            }
        }
    }

    public void ClearTarget(Enemy enemy){

    }
    public void ClearAllTarget(){
        
    }

}
