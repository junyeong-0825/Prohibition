
public static class GameEvents
{
    public delegate void EventDelegate();
    public static event EventDelegate OnDayEnd;
    public static event EventDelegate OnSuccessTrade;
    public static event EventDelegate OnFailTrade;
    public static event EventDelegate OnTimeOverTrade;

    public static void NotifyDayEnd() { if (OnDayEnd != null) OnDayEnd(); }
    public static void NotifySuccesTrade() { if (OnSuccessTrade != null) OnSuccessTrade(); }
    public static void NotifyFailTrade() { if (OnFailTrade != null) OnFailTrade(); }
    public static void NotifyTimeOverTrade() { if (OnTimeOverTrade != null) OnTimeOverTrade(); }

}
