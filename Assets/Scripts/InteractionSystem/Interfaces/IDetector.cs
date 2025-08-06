
using System;

namespace InteractionSystem.Interfaces
{
    public interface IDetector<T, T1>
    {
        void Detect(T1 obj);
        void AttachDetected(Action<T> callback);
    }
}
