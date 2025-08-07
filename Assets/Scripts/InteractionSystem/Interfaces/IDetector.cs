
using System;

namespace InteractionSystem.Interfaces
{
    public interface IDetector<T>
    {
        void AttachDetected(Action<T> callback);
    }
}
