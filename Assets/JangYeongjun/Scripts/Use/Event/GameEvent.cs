
public static class GameEvents
{
    public delegate void EventDelegate();
    public static event EventDelegate OnDayEnd;

    public static void NotifyDayEnd() { if (OnDayEnd != null) OnDayEnd(); }
}
