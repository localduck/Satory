using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public interface INeighborFinder
    {
        public void GetAdjacentHexesExtended(HexBattleground initHex);
    }
}
