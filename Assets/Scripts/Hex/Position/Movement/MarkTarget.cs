using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class MarkTarget : INeighborFinder
    {
        string caseMethod = "BoolTargetRange";
        public void GetAdjacentHexesExtended(HexBattleground initHex)
        {
            List<HexBattleground> nborToCheck = HexCalculation.GetHexNeighbours(initHex, caseMethod);
            foreach(HexBattleground hex in nborToCheck)
            {
                hex.lookingForTarget = true;
                if(hex.GetComponentInChildren<Enemy>() != null)
                {
                    hex.PotentialTergetDefined();
                }
            }
        }
    }
}
