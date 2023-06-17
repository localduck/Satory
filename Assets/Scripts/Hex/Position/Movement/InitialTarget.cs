using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class InitialTarget : IInitialHexes
    {
        List<HexBattleground> initialHexes = new List<HexBattleground>();
        public List<HexBattleground> GetNewInitialHexes()
        {
            initialHexes.Clear();
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                if (hex.lookingForTarget)
                {
                    initialHexes.Add(hex);
                }
            }
            return initialHexes;
        }
    }
}
