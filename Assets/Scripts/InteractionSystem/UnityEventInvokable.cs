using InteractionSystem.Interfaces;
using UnityEngine.Events;

public class UnityEventInvokable : UnityEvent, IInvokable, IInvokableParams
{
    void IInvokableParams.Invoke(params object[] obj)
    {
        Invoke();
    }
}
public class UnityEventInvokable<T0> : UnityEvent<T0>, IInvokable<T0>, IInvokableParams
{
    void IInvokableParams.Invoke(params object[] obj)
    {
        Invoke((T0)obj[0]);
    }
}

public class UnityEventInvokable<T0, T1> : UnityEvent<T0, T1>, IInvokable<T0, T1>, IInvokableParams
{
    void IInvokableParams.Invoke(params object[] obj)
    {
        Invoke((T0)obj[0], (T1)obj[1]);
    }
}

public class UnityEventInvokable<T0, T1, T2> : UnityEvent<T0, T1, T2>, IInvokable<T0, T1, T2>, IInvokableParams
{
    void IInvokableParams.Invoke(params object[] obj)
    {
        Invoke((T0)obj[0], (T1)obj[1], (T2)obj[2]);
    }
}

public class UnityEventInvokable<T0, T1, T2, T3> : UnityEvent<T0, T1, T2, T3>, IInvokable<T0, T1, T2, T3>, IInvokableParams
{
    void IInvokableParams.Invoke(params object[] obj)
    {
        Invoke((T0)obj[0], (T1)obj[1], (T2)obj[2], (T3)obj[3]);
    }
}
