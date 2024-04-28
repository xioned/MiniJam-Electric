using System.Collections.Generic;
using UnityEngine;

public class ObjectPlaceManager : MonoBehaviour
{
    public List<GameObject> placeableObjectPrefabs;
    public List<GameObject> placeableObjectGhostPrefabs;
    public EnemyWaveManager enemyWaveManager;
    int quantity;
    GameObject currentGhostPrefab;
    Camera currentCamera;
    Grid grid;
    public List<string> notPlaceableTileList = new();
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
                Instantiate(placeableObjectPrefabs[selectedReward.id], new Vector2(placePos.x + .5f, placePos.y + .5f),Quaternion.identity);
                selectedReward.quantity -= 1;
                notPlaceableTileList.Add(tilePos);
                if (selectedReward.quantity > 0) { return; }
                selectedReward.id = -1;
                Destroy(currentGhostPrefab);
                enemyWaveManager.GenerateWave();
            }
        }
    }
}
