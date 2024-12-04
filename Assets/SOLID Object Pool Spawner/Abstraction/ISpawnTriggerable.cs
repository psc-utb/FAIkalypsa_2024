using System;

namespace SOLID_Object_Pool
{
    public interface ISpawnTriggerable
    {
        event Action SpawnTrigger;
    }
}
