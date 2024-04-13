using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Building : MonoBehaviour
{
    public bool Placed;
    public BoundsInt Area;
    public int SortingOrderNum = 0;

    public GameObject GameManagerObject;
    //public GameObject GBSystemObject;

    private float LowestTileXPos = -2;
    private float LowestTileYPos = -3;

    private static List<int> UsedSortingLayers = new List<int>();

    void Start()
    {
        GameManagerObject = GameObject.Find("GameManager");
        //GBSystemObject = GameObject.Find("Grid");
    }

    #region Re-placeability
    void Update()
    {
        //Allows the player to re-place an object.
        /*if(Input.GetMouseButton(0) && Placed)
        {
            float LocationX = gameObject.transform.position.x;
            float LocationY = gameObject.transform.position.y;
            float MouseLocationX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float MouseLocationY = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

            if(MouseLocationX < LocationX + 0.25f && MouseLocationY < LocationY + 1.5f)
            {
                if(MouseLocationX > LocationX - 0.25f && MouseLocationY > LocationY - 1.5f)
                {
                    GridBuildingSystem GBSystem = GBSystemObject.GetComponent<GridBuildingSystem>();
                    GBSystem.Temp = gameObject.GetComponent<Building>();
                    Placed = false;
                }
            }
        }*/

        //Determines depth of the placed item.
        if(!Placed)
        {
            SpriteRenderer Renderer = GetComponent<SpriteRenderer>();

            //int Multiplier = 4;
            float XPos = gameObject.transform.position.x;
            float YPos = gameObject.transform.position.y;
            XPos += Mathf.Abs(LowestTileXPos);
            YPos += Mathf.Abs(LowestTileYPos);

            SortingOrderNum = RoundInt(YPos) - RoundInt(XPos);

            /*if(UsedSortingLayers.Contains(SortingOrderNum))
            {
                SortingOrderNum++;
            }*/

            Renderer.sortingOrder = (100 - SortingOrderNum);
        }
    }

    int RoundInt(float tempFloat)
    {
        int _int = (int)tempFloat;
        if(tempFloat >= _int + 0.5f)
        {
            _int++;
        }

        return _int;
    }
    #endregion

    #region Build Methods
    public bool CanBePlaced()
    {
        Vector3Int PositionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt AreaTemp = Area;
        AreaTemp.position = PositionInt;

        if (GridBuildingSystem.current.CanTakeArea(AreaTemp))
        {
            return true;
        }

        return false;
    }

    public void Place()
    {
        //Places this object.
        Vector3Int PositionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt AreaTemp = Area;
        AreaTemp.position = PositionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(AreaTemp, GridBuildingSystem.TileType.Red);

        //Calculates the current score.
        GameManager gameManager = GameManagerObject.GetComponent<GameManager>();
        gameManager.ChangeScore(gameObject.tag);

        /*foreach(int layer in UsedSortingLayers)
        {
            Debug.Log(layer);
        }

        if(!UsedSortingLayers.Contains(SortingOrderNum))
        {
            UsedSortingLayers.Add(SortingOrderNum);
        }*/
    }
    #endregion
}