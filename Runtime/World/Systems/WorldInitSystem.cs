using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Unity.Mathematics;
using UnityEngine;

namespace Voody.UniLeo.Lite
{
    /// This class handle global init to ECS World

#if ENABLE_IL2CPP
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.NullChecks, false)]
    [Unity.IL2CPP.CompilerServices.Il2CppSetOption (Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false)]
#endif

    class WorldInitSystem : IEcsPreInitSystem, IEcsRunSystem, IEcsDestroySystem
    {
        private EcsPool<InstantiateComponent> _instantiatePool;
        private EcsFilter _filter;
        private EcsWorld _baseWorld;

        public void PreInit(EcsSystems systems)
        {
            var convertableGameObjects =
                GameObject.FindObjectsOfType<ConvertToEntity>();
            // Iterate throught all gameobjects, that has ECS Components
            foreach (var convertable in convertableGameObjects)
            {
                AddEntity(convertable.gameObject, systems, convertable.GetWorldName());
            }

            _baseWorld = systems.GetWorld();
            _filter = _baseWorld.Filter<InstantiateComponent>().End();
            _instantiatePool = _baseWorld.GetPool<InstantiateComponent>();

            // After adding all entitites from the begining of the scene, we need to handle global World value
            WorldHandler.Init(_baseWorld);
        }

        public void Run(EcsSystems systems)
        {
            foreach (var i in _filter)
            {
                ref InstantiateComponent instantiate = ref _instantiatePool.Get(i);
                if (instantiate.gameObject)
                {
                    AddEntity(instantiate.gameObject, systems, instantiate.worldName);
                }

                _baseWorld.DelEntity(i);
            }
        }

        public void Destroy(EcsSystems systems)
        {
            WorldHandler.Destroy();
        }

        // Creating New Entity with components function
        private void AddEntity(GameObject gameObject, EcsSystems systems, String worldName)
        {
            var nameValue = worldName == "" ? null : worldName;
            var spawnWorld = systems.GetWorld(nameValue);
            int entity = spawnWorld.NewEntity();
            ConvertToEntity convertComponent = gameObject.GetComponent<ConvertToEntity>();
            if (convertComponent)
            {
                foreach (var component in gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        // Adding Component to entity
                        entityComponent.Convert(entity, spawnWorld);
                        GameObject.Destroy(component);
                    }
                }

                switch (convertComponent.GetConvertMode())
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.Destroy(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.Destroy(convertComponent);
                        break;
                }
            }
        }
    }
}

#if ENABLE_IL2CPP
// Unity IL2CPP performance optimization attribute.
namespace Unity.IL2CPP.CompilerServices {
    enum Option {
        NullChecks = 1,
        ArrayBoundsChecks = 2
    }

    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class Il2CppSetOptionAttribute : Attribute {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute (Option option, object value) { Option = option; Value = value; }
    }
}
#endif