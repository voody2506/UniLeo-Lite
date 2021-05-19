using System;
using System.Collections;
using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace Voody.UniLeo.Light
{
    public static class WorldHandler
    {
        private static EcsWorld world;

        public static void Init(EcsWorld ecsWorld)
        {
            world = ecsWorld;
        }

        public static EcsWorld GetMainWorld()
        {
            return world;
        }

        public static void Destroy()
        {
            world = null;
        }
    }
}
