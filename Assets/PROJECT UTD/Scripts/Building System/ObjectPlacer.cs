using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    public class ObjectPlacer : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> placedGameObject = new();
        private TurretController turret;
        private GameObject newObject;

        public int PlaceObject(GameObject prefab, Vector3 position)
        {
            newObject = Instantiate(prefab);
            newObject.transform.position = position;
            placedGameObject.Add(newObject);

            return placedGameObject.Count - 1;
        }

        public void InitTurret(ObjectDatabaseSO database, int selectedObjectIndex)
        {
            turret = newObject.GetComponentInChildren<TurretController>();
            Debug.Log(database.objectsData[selectedObjectIndex].Damage);
            turret.damage = database.objectsData[selectedObjectIndex].Damage;
            turret.attackSpeed = database.objectsData[selectedObjectIndex].AttackSpeed;
            turret.sellPrice = database.objectsData[selectedObjectIndex].sellPrice;
        }

        internal void RemoveObjectAt(int gameObjectIndex)
        {
            if(placedGameObject.Count <= gameObjectIndex || placedGameObject[gameObjectIndex] == null)
            {
                return;
            }
            Destroy(placedGameObject[gameObjectIndex]);
            placedGameObject[gameObjectIndex] = null;
        }
    }
}
