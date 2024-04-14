using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class GridBuildingSystem : MonoBehaviour
{
    public static GridBuildingSystem current;

    public GridLayout gridLayout;
    public Tilemap MainTilemap;
    public Tilemap TempTilemap;

    public GameObject FurnitureHolder;

    static Dictionary<TileType, TileBase> TileBases = new Dictionary<TileType, TileBase>();

    //Private variables
    Vector3 PrevPos;
    BoundsInt PrevArea;
    Building Temp;

    List<TileBase> PlaceableAreas = new List<TileBase>();

    //Contains standard Unity Methods such as Awake, Start, and Update.
    #region Unity Methods

    void Awake()
    {
        current = this;
    }

    void Start()
    {
        string TilePath = @"Tiles/";
        TileBases.Add(TileType.Empty, null);
        TileBases.Add(TileType.White, Resources.Load<TileBase>(TilePath + "white"));
        TileBases.Add(TileType.Green, Resources.Load<TileBase>(TilePath + "green"));
        TileBases.Add(TileType.Red, Resources.Load<TileBase>(TilePath + "red"));

        //Removes MainTilemap from the player's view.
        TilemapRenderer MainTilemapRenderer = MainTilemap.GetComponent<TilemapRenderer>();
        MainTilemapRenderer.enabled = false;

        //The length of PlaceableAreas saves the amount of white tiles in MainTilemap.
        foreach(TileBase tile in GetTilesBlock(MainTilemap.cellBounds, MainTilemap))
        {
            PlaceableAreas.Add(tile);
        }
    }

    void Update()
    {
        if (!Temp)
        {
            return;
        }

        if (GameManager.GridIsActive)
        {
            //Allows the player to drag furniture.
            if (Input.GetMouseButton(0))
            {
                if (EventSystem.current.IsPointerOverGameObject(0))
                {
                    return;
                }

                if (!Temp.Placed)
                {
                    Vector2 TouchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector3Int CellPos = gridLayout.LocalToCell(TouchPos);

                    if (PrevPos != CellPos)
                    {
                        Temp.transform.localPosition = gridLayout.CellToLocalInterpolated(CellPos + new Vector3(0.5f, 0.5f, 0f));
                        PrevPos = CellPos;
                        FollowBuilding();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Temp.CanBePlaced())
                {
                    Temp.Place();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClearArea();
                Destroy(Temp.gameObject);
            }
            else if(Input.GetKeyDown(KeyCode.C))
            {
                //Destroys all placed furniture.
                for(int i = FurnitureHolder.transform.childCount - 1; i >= 0; i--)
                {
                    Destroy(FurnitureHolder.transform.GetChild(i).gameObject);
                }

                //Resets placeability on the board.
                int Size = PlaceableAreas.Count;
                TileBase[] TileArray = new TileBase[Size];

                for (int i = 0; i < Size; i++)
                {
                    TileArray[i] = TileBases[TileType.White];
                }

                MainTilemap.SetTilesBlock(MainTilemap.cellBounds, TileArray);
            }
        }
    }
    #endregion

    //Gets and sets tiles on a tilemap.
    #region Tilemap Management

    //Gets tiles one by one and returns an array of them.
    static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] Array = new TileBase[area.size.x * area.size.y * area.size.z];
        int Counter = 0;

        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int Pos = new Vector3Int(v.x, v.y, v.z);
            Array[Counter] = tilemap.GetTile(Pos);
            Counter++;
        }

        return Array;
    }

    static void SetTilesBlock(BoundsInt area, TileType type, Tilemap tilemap)
    {
        int Size = area.size.x * area.size.y * area.size.z;
        TileBase[] TileArray = new TileBase[Size];
        FillTiles(TileArray, type);
        tilemap.SetTilesBlock(area, TileArray);
    }

    static void FillTiles(TileBase[] arr, TileType type)
    {
        for(int i = 0; i < arr.Length; i++)
        {
            arr[i] = TileBases[type];
        }
    }
    #endregion

    //Methods that move and place building.
    #region Building Placement

    public void InitializeWithBuilding(GameObject building)
    {
        if (GameManager.GridIsActive)
        {
            Temp = Instantiate(building, FurnitureHolder.transform).GetComponent<Building>();
            FollowBuilding();
        }
    }

    void ClearArea()
    {
        TileBase[] ToClear = new TileBase[PrevArea.size.x * PrevArea.size.y * PrevArea.size.z];
        FillTiles(ToClear, TileType.Empty);
        TempTilemap.SetTilesBlock(PrevArea, ToClear);
    }


    void FollowBuilding()
    {
        ClearArea();

        Temp.Area.position = gridLayout.WorldToCell(Temp.gameObject.transform.position);
        BoundsInt BuildingArea = Temp.Area;

        TileBase[] BaseArray = GetTilesBlock(BuildingArea, MainTilemap);

        int Size = BaseArray.Length;
        TileBase[] TileArray = new TileBase[Size];

        for (int i = 0; i < BaseArray.Length; i++)
        {
            if(BaseArray[i] == TileBases[TileType.White])
            {
                TileArray[i] = TileBases[TileType.Green];
            }
            else
            {
                FillTiles(TileArray, TileType.Red);
                break;
            }
        }

        TempTilemap.SetTilesBlock(BuildingArea, TileArray);
        PrevArea = BuildingArea;
    }

    public bool CanTakeArea(BoundsInt area)
    {
        TileBase[] BaseArray = GetTilesBlock(area, MainTilemap);
        foreach(var b in BaseArray)
        {
            if(b != TileBases[TileType.White])
            {
                Debug.Log("Cannot place here");
                return false;
            }
        }

        return true;
    }

    public void TakeArea(BoundsInt area, TileType mainColor)
    {
        SetTilesBlock(area, TileType.Empty, TempTilemap);
        SetTilesBlock(area, mainColor, MainTilemap);
    }

    #endregion

    public enum TileType
    {
        Empty,
        White,
        Green,
        Red,
    }
}