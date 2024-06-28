using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTD
{
    [CreateAssetMenu]
    public class EnemyDatabaseSO : ScriptableObject
    {
        public List<EnemyData> objectsData;
    }

    [Serializable]
    public class EnemyData
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public int ID { get; private set; }

        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField]
        public float HealthPoint { get; private set; }

        [field: SerializeField]
        public string Attribute { get; private set; }

        [field: SerializeField]
        public float MoveSpeed { get; private set; }

    }
}
