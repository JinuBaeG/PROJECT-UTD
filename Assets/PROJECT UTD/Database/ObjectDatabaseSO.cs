using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    [CreateAssetMenu]
    public class ObjectDatabaseSO : ScriptableObject
    {
        public List<ObjectData> objectsData;
    }

    [Serializable]
    public class ObjectData
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public int ID { get; private set; }

        [field: SerializeField]
        public Vector2Int Size { get; private set; } = Vector2Int.one;

        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public int Damage { get; private set; }

        [field: SerializeField]
        public string Attribute { get; private set; }

        [field: SerializeField]
        public float AttackSpeed { get; private set; }

        [field: SerializeField]
        public bool isSplash { get; private set; } = false;

        [field: SerializeField]
        public int sellPrice { get; private set; }

        [field: SerializeField]
        public int weight { get; private set; }

    }
}
