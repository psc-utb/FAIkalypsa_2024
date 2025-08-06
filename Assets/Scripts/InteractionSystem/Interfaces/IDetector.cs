
using System;

namespace InteractionSystem.Interfaces
{
    public interface IDetector<T1, T>
    {
        void Detect(T1 obj);
        void AttachDetected(Action<T> callback);
    }
}
