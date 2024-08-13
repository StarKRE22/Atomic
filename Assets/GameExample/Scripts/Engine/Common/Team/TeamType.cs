using System;
using UnityEngine;

namespace GameExample.Engine
{
    public enum TeamType
    {
        BLUE = 0,
        RED = 1,
        NONE = 2
    }

    public static class TeamTypeExtensions
    {
        public static Color GetColor(this TeamType teamType)
        {
            return teamType switch
            {
                TeamType.BLUE => Color.blue,
                TeamType.RED => Color.red,
                TeamType.NONE => Color.black,
                _ => throw new ArgumentOutOfRangeException(nameof(teamType), teamType, null)
            };
        }
    }
}