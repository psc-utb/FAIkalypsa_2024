using System;

namespace SOLID_Object_Pool
{
    public interface IDespawnable<T>
    {
        event Action<T, bool> Despawn;
    }
}
