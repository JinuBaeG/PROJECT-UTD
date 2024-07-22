using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class GridData
    {
        Dictionary<Vector3Int, PlacementData> placedObjects = new();

        public int GetPlacedObjectID(Vector3Int gridPosition)
        {
            return placedObjects[gridPosition].ID;
        }

        public int GetPlacedObjectPrice(Vector3Int gridPosition)
        {
            return placedObjects[gridPosition].SellPrice;
        }

        public void AddOjectAt(Vector3Int gridPosition, Vector2Int objectSize, int ID, int placedObjectIndex, int sellPrice)
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);
            PlacementData data = new PlacementData(positionToOccupy, ID, placedObjectIndex, sellPrice);
            foreach(var pos in positionToOccupy)
            {
                if(placedObjects.ContainsKey(pos))
                {
                    throw new Exception("Dictionary already contains this cell position in {pos}");
                }

                placedObjects[pos] = data;
            }
        }

        private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> returnVal = new();

            for(int x = 0; x < objectSize.x; x++)
            {
                for(int y = 0; y < objectSize.y; y++)
                {
                    returnVal.Add(gridPosition + new Vector3Int(x, 0, y));
                }
            }

            return returnVal;
        }

        public bool CanPlacedObjectAt(Vector3Int gridPosition, Vector2Int objectSize)
        {
            List<Vector3Int> positionToOccupy = CalculatePositions(gridPosition, objectSize);

            foreach(var pos in positionToOccupy)
            {
                if(placedObjects.ContainsKey(pos))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetRepresentationIndex(Vector3Int gridPosition)
        {
            if(placedObjects.ContainsKey(gridPosition) == false)
            {
                return -1;
            }

            return placedObjects[gridPosition].PlacedObjectIndex;
        }

        public void RemoveObjectAt(Vector3Int gridPosition)
        {
            foreach(var pos in placedObjects[gridPosition].occupiedPositions)
            {
                placedObjects.Remove(pos);
            }
        }

    }

    public class PlacementData
    {
        public List<Vector3Int> occupiedPositions;

        public int ID { get; private set; }

        public int PlacedObjectIndex { get; private set; }

        public int SellPrice { get; private set; }

        public PlacementData(List<Vector3Int> occupiedPositions, int iD, int placedObjectIndex, int sellPrice)
        {
            this.occupiedPositions = occupiedPositions;
            ID = iD;
            PlacedObjectIndex = placedObjectIndex;
            SellPrice = sellPrice;
        }
    }
}
