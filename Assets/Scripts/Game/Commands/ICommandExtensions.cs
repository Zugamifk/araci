using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ICommandExtensions
{
    public static void Do(this ICommand command)
    {
        Game.Do(command);
    }
}
