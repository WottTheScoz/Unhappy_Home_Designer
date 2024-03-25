using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class Building : MonoBehaviour
{
    public bool Placed;
    public BoundsInt Area;
    //public int Cost;

    //public GameObject GameManagerObject;
    //public GameObject MoneyManagerObject;

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

        Vector3Int PositionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
        BoundsInt AreaTemp = Area;
        AreaTemp.position = PositionInt;
        Placed = true;
        GridBuildingSystem.current.TakeArea(AreaTemp);
    }
    #endregion
}