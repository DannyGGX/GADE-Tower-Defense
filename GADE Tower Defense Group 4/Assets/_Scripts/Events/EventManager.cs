using System.Collections;
using System.Collections.Generic;
using DannyG;
using UnityEngine;

/// <summary>
/// Used for storing and accessing events all in one place.
/// Source: https://youtu.be/RPhTEJw6KbI?si=-_n_Dyt1jjGXp6BY
/// Pro: This event system is less resource intensive than one that uses UnityEvents
/// Con: not as good for team collaboration, because of potential merge conflicts from creating events in this class.
///
/// Tip: use { get; } to expose where the event is used
/// </summary>
public static class EventManager
{
    public static Event<bool> onGamePaused { get; } = new (); // bool: true if game is paused
    
    
}
