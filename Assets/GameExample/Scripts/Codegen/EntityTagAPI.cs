/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;

namespace Atomic.Entities
{
    public static class EntityTagAPI
    {
        ///Keys
        public const int Coin = 1;
        public const int Character = 2;


        ///Extensions
        public static bool HasCoinTag(this IEntity obj) => obj.HasTag(Coin);
        public static bool AddCoinTag(this IEntity obj) => obj.AddTag(Coin);
        public static bool DelCoinTag(this IEntity obj) => obj.DelTag(Coin);

        public static bool HasCharacterTag(this IEntity obj) => obj.HasTag(Character);
        public static bool AddCharacterTag(this IEntity obj) => obj.AddTag(Character);
        public static bool DelCharacterTag(this IEntity obj) => obj.DelTag(Character);
    }
}
