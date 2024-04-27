using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceManager : MonoBehaviour
{
    public List<GameObject> placeableObjectPrefabs;
    public List<GameObject> placeableObjectGhostPrefabs;
    public int quantity;
    public GameObject currentGhostPrefab;
    Camera currentCamera;
    Grid grid;
    public List<string> notPlaceableTileList = new List<string>();
    private void Awake()
    {
        currentCamera = Camera.main;
        grid = FindObjectOfType<Grid>();
    }
    public void SetPlaceableObject(Reward reward)
    {
        if(currentGhostPrefab != null) { Destroy(currentGhostPrefab); }
        quantity = reward.quantity;
        Vector2 placePos = (Vector2Int)grid.WorldToCell(currentCamera.ScreenToWorldPoint(Input.mousePosition));
        currentGhostPrefab = Instantiate(placeableObjectGhostPrefabs[reward.id], placePos,Quaternion.identity);
    }

    private void Update()
    {
        if(currentGhostPrefab == null) { return; }
        Vector2 placePos = (Vector2Int)grid.WorldToCell(currentCamera.ScreenToWorldPoint(Input.mousePosition));
        currentGhostPrefab.transform.position = placePos;
        if(Input.GetMouseButtonDown(0))
        {
            string tilePos = placePos.ToString();
            if (notPlaceableTileList.Contains(tilePos))
            {
                //CantPlace
            }
            else
            {
                
            }
        }
    }
}
