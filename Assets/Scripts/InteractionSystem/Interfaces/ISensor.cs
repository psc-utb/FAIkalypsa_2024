
using System;

namespace InteractionSystem.Interfaces
{
    public interface ISensor<T>
    {
        void Sense();
        void AttachSensed(Action<T> callback);
    }
}
