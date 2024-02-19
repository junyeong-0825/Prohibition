
public static class GameEvents
{
    public delegate void EventDelegate();
    public static event EventDelegate OnDayEnd;
    public static event EventDelegate OnSave;
    public static event EventDelegate OnSaveDay;

    public static void NotifyDayEnd() { if (OnDayEnd != null) OnDayEnd(); }

    public static void NotifySave() { if (OnSave != null) OnSave(); }
}
