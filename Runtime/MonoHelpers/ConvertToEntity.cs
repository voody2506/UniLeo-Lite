using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leopotam.EcsLite;

namespace Voody.UniLeo.Lite
{
    public enum ConvertMode
    {
        ConvertAndInject,
        ConvertAndDestroy
    }

    public class ConvertToEntity : MonoBehaviour
    {
        [SerializeField] private ConvertMode convertMode; // Conversion Mode of ECS
        [SerializeField] private String customWorld; // World Type of ECS

        private void Start()
        {
            var world = WorldHandler.GetMainWorld(); // Getting Main World from ECS
            if (world != null)
            {
                var entity = world.NewEntity();
                var instantiatePool = world.GetPool<InstantiateComponent>();
                ref var instantiateComponent = ref instantiatePool.Add(entity);
                instantiateComponent.gameObject = gameObject;
                instantiateComponent.worldName = customWorld;
            }
        }

        public String GetWorldName()
        {
            return customWorld;
        }

        public ConvertMode GetConvertMode()
        {
            return convertMode;
        }
    }
}