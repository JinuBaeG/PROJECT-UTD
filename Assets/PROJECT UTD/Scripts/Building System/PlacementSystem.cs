using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class PlacementSystem : MonoBehaviour
    {
        [SerializeField]
        private InputManager inputManager;
        [SerializeField]
        private Grid grid;

        [SerializeField]
        private ObjectDatabaseSO database;

        [SerializeField]
        private GameObject gridVisualization;

        private GridData buildingData;

        [SerializeField]
        private PreviewSystem preview;

        private Vector3Int lastDetectedPosition = Vector3Int.zero;

        [SerializeField]
        public ObjectPlacer objectPlacer;

        IBuildingState buildingState;

        private bool isReplace;

        private void Start()
        {
            StopPlacement();
            buildingData = new();
        }

        // Turret Place
        public void StartPlacement(int ID)
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new PlacementState(ID, grid, preview, database, buildingData, objectPlacer);
            inputManager.OnClicked += PlaceStructure;
            inputManager.OnExit += StopPlacement;
        }
        // Turret Replace
        public void StartReplacement()
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new ReplacementState(grid, preview, database, buildingData, objectPlacer);
            inputManager.OnClicked += PlaceStructure;
            //inputManager.OnExit += StopPlacement;
            isReplace = true;
        }
        // Turret Remove
        public void StartRemoving()
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new RemovingState(grid, preview, buildingData, objectPlacer);
            inputManager.OnClicked += PlaceStructure;
            inputManager.OnExit += StopPlacement;
        }

        private void PlaceStructure()
        {
            if(inputManager.IsPointerOverUI())
            {
                return;
            }
            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            buildingState.OnAction(gridPosition);

            if(!isReplace)
            {
                StopPlacement();
            } else
            {
                isReplace = false;
            }
        }

        //private bool CheckPlacementValidity(Vector3Int gridPosition, int selectObjectIndex)
        //{
        //    GridData selectedData = database.objectsData[selectObjectIndex].ID == 0 ? floorData : buildingData;

        //    return selectedData.CanPlacedObjectAt(gridPosition, database.objectsData[selectObjectIndex].Size);
        //}

        private void StopPlacement()
        {
            gridVisualization.SetActive(false);

            if (buildingState == null)
            {
                return;
            }

            buildingState.EndState();
            inputManager.OnClicked -= PlaceStructure;
            inputManager.OnExit -= StopPlacement;
            lastDetectedPosition = Vector3Int.zero;
            buildingState = null;
        }

        private void Update()
        {
            if(buildingState == null)
            {
                return;
            }

            Vector3 mousePosition = inputManager.GetSelectedMapPosition();
            Vector3Int gridPosition = grid.WorldToCell(mousePosition);

            if(lastDetectedPosition != gridPosition)
            {
                buildingState.UpdateState(gridPosition);
                lastDetectedPosition = gridPosition;
            }

        }
    }
}
