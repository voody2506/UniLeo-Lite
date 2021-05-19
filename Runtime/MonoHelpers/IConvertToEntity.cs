using System.Collections;
using Leopotam.EcsLite;

namespace Voody.UniLeo.Light
{
    public interface IConvertToEntity
    {
        void Convert(int entity, EcsWorld world);
    }
}
