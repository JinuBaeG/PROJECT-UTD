using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class RemovingState : IBuildingState
    {
        private int gameObjectIndex = -1;
        Grid grid;
        PreviewSystem previewSystem;
        GridData buildingData;
        ObjectPlacer objectPlacer;

        public RemovingState(Grid grid,
                             PreviewSystem previewSystem,
                             GridData buildingData,
                             ObjectPlacer objectPlacer)
        {
            this.grid = grid;
            this.previewSystem = previewSystem;
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
            if (buildingData.CanPlacedObjectAt(gridPosition, Vector2Int.one) == false)
            {
                selectedData = buildingData;
            }

            if(selectedData == null)
            {
                // Sound
            }
            else
            {
                gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
                if(gameObjectIndex == -1)
                {
                    return;
                }

                selectedData.RemoveObjectAt(gridPosition);
                objectPlacer.RemoveObjectAt(gameObjectIndex);
            }

            Vector3 cellPosition = grid.CellToWorld(gridPosition);
            previewSystem.UpdatePosition(cellPosition, CheckIfSelectionISValid(gridPosition));
        }

        private bool CheckIfSelectionISValid(Vector3Int gridPosition)
        {
            return !(buildingData.CanPlacedObjectAt(gridPosition, Vector2Int.one));
        }

        public void UpdateState(Vector3Int gridPosition)
        {
            bool validity = CheckIfSelectionISValid(gridPosition);
            previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity);
        }
    }
}
