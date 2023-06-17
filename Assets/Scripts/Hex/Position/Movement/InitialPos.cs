using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class InitialPos : IInitialHexes
    {
        private List<HexBattleground> initialHexes = new List<HexBattleground>();

        public List<HexBattleground> GetNewInitialHexes()//collects objects whose neighbours need to be found
        {
            initialHexes.Clear();// empty the array before filling it again
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                if (hex.IsNeighbourHex & !hex.IsIncluded)//eliminates unnecessary hexes
                {
                    initialHexes.Add(hex);
                }
            }
            return initialHexes;
        }
    }
}
