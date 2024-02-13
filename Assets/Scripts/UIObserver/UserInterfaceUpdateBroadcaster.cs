
using System.Collections.Generic;

public class UserInterfaceUpdateBroadcaster 
{
    private static List<IUpdateObserver> observers = new List<IUpdateObserver>();

    public static void RegisterObserver(IUpdateObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public static void UnregisterObserver(IUpdateObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    public static void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.OnUiUpdated();
        }
    }
}
