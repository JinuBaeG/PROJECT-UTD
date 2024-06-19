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

        private GridData floorData, buildingData;

        [SerializeField]
        private PreviewSystem preview;

        private Vector3Int lastDetectedPosition = Vector3Int.zero;

        [SerializeField]
        public ObjectPlacer objectPlacer;

        IBuildingState buildingState;

        private void Start()
        {
            StopPlacement();
            floorData = new();
            buildingData = new();
        }

        public void StartPlacement(int ID)
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new PlacementState(ID, grid, preview, database, floorData, buildingData, objectPlacer);
            inputManager.OnClicked += PlaceStructure;
            inputManager.OnExit += StopPlacement;
        }

        public void StartRemoving()
        {
            StopPlacement();
            gridVisualization.SetActive(true);
            buildingState = new RemovingState(grid, preview, floorData, buildingData, objectPlacer);
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
