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
        ConvertAndDestroy,
        ConvertAndSave
    }

    public class ConvertToEntity : MonoBehaviour
    {
        [SerializeField] private ConvertMode convertMode; // Conversion Mode of ECS
        [SerializeField] private String customWorld; // World Type of ECS
        private EcsPackedEntity packedEntity;
        private EcsWorld spawnWorld;
        private bool isProccessed = false;
        private void Start()
        {
            var world = WorldHandler.GetMainWorld(); // Getting Main World from ECS
            if (world != null && isProccessed)
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

        // After World Init system convert all components tp 
        public void setProccessed()
        {
            this.isProccessed = true;
        }
        
        public int? TryGetEntity()
        {
            if (spawnWorld != null)
            {
                int entity;
                if (packedEntity.Unpack(spawnWorld, out entity))
                {
                    return entity;
                }
            }
            return null;
        }

        public void Set(int entity, EcsWorld world)
        {
            spawnWorld = world;
            packedEntity = EcsEntityExtensions.PackEntity(world, entity);
        }
    }
}