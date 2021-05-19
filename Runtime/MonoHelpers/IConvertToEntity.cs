using System.Collections;
using Leopotam.EcsLite;

namespace Voody.UniLeo.Lite
{
    public interface IConvertToEntity
    {
        void Convert(int entity, EcsWorld world);
    }
}
