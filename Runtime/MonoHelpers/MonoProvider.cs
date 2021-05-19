using UnityEngine;
using Leopotam.EcsLite;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;

namespace Voody.UniLeo.Light
{
    public abstract class MonoProvider<T> : BaseMonoProvider, IConvertToEntity where T : struct
    {

        [SerializeField] private T value;

        void IConvertToEntity.Convert(int entity, EcsWorld world)
        {
            var pool = world.GetPool<T>();
            if (pool.Has(entity))
            {
                pool.Del(entity);
            }

            pool.Add(entity) = value;
        }
    }
}
