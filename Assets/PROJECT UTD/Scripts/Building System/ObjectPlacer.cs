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
        private Scanner scanner;
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
            scanner = newObject.GetComponentInChildren<Scanner>();

            turret.damage = database.objectsData[selectedObjectIndex].Damage;
            turret.damageRange = database.objectsData[selectedObjectIndex].DamageRange;
            turret.isSplash = database.objectsData[selectedObjectIndex].isSplash;
            turret.attackSpeed = database.objectsData[selectedObjectIndex].AttackSpeed;
            turret.sellPrice = database.objectsData[selectedObjectIndex].sellPrice;
            turret.projector.orthographicSize = database.objectsData[selectedObjectIndex].range + 2.0f;
            scanner.scanRange = database.objectsData[selectedObjectIndex].range;
            turret.isPreview = false;
            turret.projector.enabled = false;
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
