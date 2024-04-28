using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceManager : MonoBehaviour
{
    public List<GameObject> placeableObjectPrefabs;
    public List<GameObject> placeableObjectGhostPrefabs;
    public EnemyWaveManager enemyWaveManager;
    GameObject currentGhostPrefab;
    Camera currentCamera;
    Grid grid;
    public List<string> notPlaceableTileList = new();
    public List<GameObject> placedObjectList = new();
    public Reward selectedReward;
    private void Awake()
    {
        currentCamera = Camera.main;
        grid = FindObjectOfType<Grid>();
    }
    public void SetPlaceableObject(Reward reward)
    {
        if(currentGhostPrefab != null) { Destroy(currentGhostPrefab); }
        selectedReward = reward;
        Vector2 placePos = (Vector2Int)grid.WorldToCell(currentCamera.ScreenToWorldPoint(Input.mousePosition));
        currentGhostPrefab = Instantiate(placeableObjectGhostPrefabs[selectedReward.id], placePos,Quaternion.identity);
    }

    private void Update()
    {
        if(currentGhostPrefab == null) { return; }
        Vector2 placePos = (Vector2Int)grid.WorldToCell(currentCamera.ScreenToWorldPoint(Input.mousePosition));
        currentGhostPrefab.transform.position = new Vector2(placePos.x+.5f, placePos.y+.5f);
        if(Input.GetMouseButtonDown(0))
        {
            string tilePos = placePos.ToString();
            if (notPlaceableTileList.Contains(tilePos))
            {
                //CantPlace
            }
            else
            {
                GameObject spawnedObject = Instantiate(placeableObjectPrefabs[selectedReward.id], new Vector2(placePos.x + .5f, placePos.y + .5f),Quaternion.identity);
                //CheckNeighbourBuilds(spawnedObject, placePos, selectedReward.id);
                selectedReward.quantity -= 1;
                notPlaceableTileList.Add(tilePos);
                placedObjectList.Add(spawnedObject);
                if (selectedReward.quantity > 0) { return; }
                selectedReward.id = -1;
                Destroy(currentGhostPrefab);
                enemyWaveManager.GenerateWave();
            }
        }
    }

    public void CheckNeighbourBuilds(GameObject spawnedObject,Vector2 placePos,int id)
    {
        if (notPlaceableTileList.Contains(new Vector2(placePos.x+1, placePos.y).ToString()))
        {
            
        }
        if (notPlaceableTileList.Contains(new Vector2(placePos.x -1, placePos.y).ToString()))
        {

        }
        if (notPlaceableTileList.Contains(new Vector2(placePos.x, placePos.y+1).ToString()))
        {

        }
        if (notPlaceableTileList.Contains(new Vector2(placePos.x + 1, placePos.y-1).ToString()))
        {

        }
    }
}
