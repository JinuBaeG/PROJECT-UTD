using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class PlacementState : IBuildingState
    {
        private int selectedObjectIndex = -1;
        int ID;
        Grid grid;
        PreviewSystem previewSystem;
        ObjectDatabaseSO database;
        GridData buildingData;
        ObjectPlacer objectPlacer;

        public PlacementState(int iD,
                              Grid grid,
                              PreviewSystem previewSystem,
                              ObjectDatabaseSO database,
                              GridData buildingData,
                              ObjectPlacer objectPlacer)
        {
            ID = iD;
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.database = database;
            this.buildingData = buildingData;
            this.objectPlacer = objectPlacer;

            selectedObjectIndex = database.objectsData.FindIndex(data => data.ID == ID);
            
            if (selectedObjectIndex > -1)
            {
                previewSystem.StartShowingPlacementPreview(
                    database.objectsData[selectedObjectIndex].Prefab
                    , database.objectsData[selectedObjectIndex].Size);
            }
            else
            {
                throw new System.Exception($"No Object with ID {iD}");
            }
        }

        public void EndState()
        {
            previewSystem.StopShowingPreivew();
        }

        public void OnAction(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);
            if (placementValidity == false)
            {
                return;
            }

            int index = objectPlacer.PlaceObject(database.objectsData[selectedObjectIndex].Prefab, grid.CellToWorld(gridPosition));

            GridData selectedData = buildingData;

            selectedData.AddOjectAt(gridPosition
                , database.objectsData[selectedObjectIndex].Size
                , database.objectsData[selectedObjectIndex].ID
                , index);

            // Construct Position Preview
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);
        }

        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectObjectIndex)
        {
            GridData selectedData = buildingData;

            return selectedData.CanPlacedObjectAt(gridPosition, database.objectsData[selectObjectIndex].Size);
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool placementValidity = CheckPlacementValidity(gridPosition, selectedObjectIndex);

            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
        }
    }
}
