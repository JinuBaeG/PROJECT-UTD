using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class ReplacementState : IBuildingState
    {
        private int gameObjectIndex = -1;
        private int replaceSelectObjectID = -1;
        Grid grid;
        PreviewSystem previewSystem;
        ObjectDatabaseSO database;
        GridData buildingData;
        ObjectPlacer objectPlacer;

        private bool isReplace;
        private Vector3Int oldGridPosition;

        public ReplacementState(Grid grid,
                                PreviewSystem previewSystem,
                                ObjectDatabaseSO database,
                                GridData buildingData,
                                ObjectPlacer objectPlacer)
        {
            this.grid = grid;
            this.previewSystem = previewSystem;
            this.database = database;
            this.buildingData = buildingData;
            this.objectPlacer = objectPlacer;

            previewSystem.StartShowingCursorPreview();
        }

        public void EndState()
        {
            previewSystem.StopShowingPreivew();
        }

        public void OnAction(Vector3Int gridPosition)
        {
            GridData selectedData = null;
            
            if(!isReplace)
            {
                isReplace = true;

                if (buildingData.CanPlacedObjectAt(gridPosition, Vector2Int.one) == false)
                {
                    selectedData = buildingData;
                }

                if (selectedData == null)
                {
                    // Sound
                }
                else
                {
                    gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
                    replaceSelectObjectID = selectedData.GetPlacedObjectID(gridPosition);
                    oldGridPosition = gridPosition;

                    if (gameObjectIndex == -1)
                    {
                        return;
                    }

                    if (gameObjectIndex > -1)
                    {
                        selectedData.RemoveObjectAt(gridPosition);
                        objectPlacer.RemoveObjectAt(gameObjectIndex);
                        
                        previewSystem.StartShowingReplacementPreview(
                            database.objectsData[replaceSelectObjectID].Prefab
                            , database.objectsData[replaceSelectObjectID].Size, gridPosition);

                    }
                }
            }
            else
            {
                isReplace = false;

                gameObjectIndex = database.objectsData.FindIndex(data => data.ID == replaceSelectObjectID);
                int index = objectPlacer.PlaceObject(database.objectsData[replaceSelectObjectID].Prefab, grid.CellToWorld(gridPosition));

                selectedData = buildingData;
                selectedData.AddOjectAt(gridPosition
                        , database.objectsData[gameObjectIndex].Size
                        , database.objectsData[gameObjectIndex].ID
                        , index);
                previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), false);

            }

        }

        public void CancelReplacement()
        {
            gameObjectIndex = database.objectsData.FindIndex(data => data.ID == replaceSelectObjectID);
            int index = objectPlacer.PlaceObject(database.objectsData[replaceSelectObjectID].Prefab, grid.CellToWorld(oldGridPosition));

            buildingData.AddOjectAt(oldGridPosition
                        , database.objectsData[gameObjectIndex].Size
                        , database.objectsData[gameObjectIndex].ID
                        , index); 
        }

        private bool CheckPlacementValidity(Vector3Int gridPosition, int selectObjectIndex)
        {
            GridData selectedData = buildingData;

            return selectedData.CanPlacedObjectAt(gridPosition, database.objectsData[selectObjectIndex].Size);
        }

        private bool CheckIfSelectionISValid(Vector3Int gridPosition)
        {
            return !(buildingData.CanPlacedObjectAt(gridPosition, Vector2Int.one));
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            if (!isReplace)
            {
                bool validity = CheckIfSelectionISValid(gridPosition);
                previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
            } else
            {
                bool placementValidity = CheckPlacementValidity(gridPosition, replaceSelectObjectID);
                previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), placementValidity);
            }
        }
    }
}
