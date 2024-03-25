using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //The following variable(s) SHOULD be modified in the inspector.
    private GameObject GameManagerObject;

    [Space(20)]
    //The following variable(s) is/are NOT to be touched in the inspector.
    public bool Placed;
    public BoundsInt Area;

    void Awake()
    {
        GameManagerObject = GameObject.Find("GameManager");
    }

    void Update()
    {
        //Destroys any unplaced furniture when grading time begins.
        if (!GameManager.GridIsActive)
        {
            if (!Placed)
            {
                Destroy(this.gameObject);
            }
        }
    }

    #region Build Methods

    public bool CanBePlaced()
    {
        if (GameManager.GridIsActive)
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
        else
        {
            return false;
        }
    }

    public void Place()
    {
        if (GameManager.GridIsActive)
        {
            //Actual Placement
            Vector3Int PositionInt = GridBuildingSystem.current.gridLayout.LocalToCell(transform.position);
            BoundsInt AreaTemp = Area;
            AreaTemp.position = PositionInt;
            Placed = true;
            GridBuildingSystem.current.TakeArea(AreaTemp);

            //Score Calculation
            string ThisTag = gameObject.tag;
            GameManager gameManager = GameManagerObject.GetComponent<GameManager>();
            gameManager.ChangeScore(ThisTag);
        }
        
    }

    #endregion
}
