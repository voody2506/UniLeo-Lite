using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace Voody.UniLeo.Lite
{
    public static class ConversionSystemExtension
    {
        public static EcsSystems ConvertScene(this EcsSystems ecsSystems)
        {
            ecsSystems.Add(new WorldInitSystem());
            return ecsSystems;
        }
    }
}
