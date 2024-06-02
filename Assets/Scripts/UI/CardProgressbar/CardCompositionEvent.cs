using StackLandsLike.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CardCompositionEvent : EventArgs
{
    public float progress;
}
public class ProgressBarTickEvent : EventArgs
{
    public CardCraftInfo info;
}
public class StopCompositionEvent : EventArgs
{
    public CardGroup cardGroup;
}

public static class EventManager
{
    public static event EventHandler<CardCompositionEvent> CardCompositionStarted;
    public static event EventHandler<ProgressBarTickEvent> ProgressBarTick;
    public static event Action<StopCompositionEvent> StopComposition;
    public static void TriggerCardCompositionStarted(CardGroup cardGroup, float progress)
    {
        CardCompositionEvent e = new CardCompositionEvent();
        e.progress = progress;
        CardCompositionStarted?.Invoke(cardGroup, e);
    }

    public static void TriggerProgressBarTick(CardGroup cardGroup,CardCraftInfo info)
    {
        ProgressBarTickEvent e = new ProgressBarTickEvent();
        e.info = info;
        ProgressBarTick?.Invoke(cardGroup,e);
    }
    public static void TriggerStopComposition(CardGroup cardGroup)
    {
        StopCompositionEvent e = new StopCompositionEvent();
        e.cardGroup = cardGroup;
        StopComposition?.Invoke(e);
    }
    
}

