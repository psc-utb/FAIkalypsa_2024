
using System;

namespace InteractionSystem.Interfaces
{
    public interface ISensor
    {
        void Sense();
    }

    public interface ISensor<T>
    {
        void AttachSensed(Action<T> callback);
    }
}
