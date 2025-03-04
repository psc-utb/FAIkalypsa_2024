
using UnityEngine;

namespace InteractionSystem.Interfaces
{
    public interface IInvokableParams : ISerializationCallbackReceiver
    {
        void Invoke(params object[] obj);
    }

    public interface IInvokable
    {
        void Invoke();
    }

    public interface IInvokable<T0>
    {
        void Invoke(T0 arg0);
    }

    public interface IInvokable<T0, T1>
    {
        void Invoke(T0 arg0, T1 arg1);
    }

    public interface IInvokable<T0, T1, T2>
    {
        void Invoke(T0 arg0, T1 arg1, T2 arg2);
    }

    public interface IInvokable<T0, T1, T2, T3>
    {
        void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3);
    }
}
