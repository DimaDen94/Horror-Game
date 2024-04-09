using System;

public interface IMainThreadDispatcher
{
    void Enqueue(Action action);
}