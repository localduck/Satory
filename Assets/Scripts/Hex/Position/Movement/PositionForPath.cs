using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class PositionForPath : INeighborFinder
    {
        string caseMethod = "PathCheck";
        public void GetAdjacentHexesExtended(HexBattleground initHex)
        {
            List<HexBattleground> neighborToCheck = HexCalculation.GetHexNeighbours(initHex, caseMethod);
            foreach (HexBattleground hex in neighborToCheck)
            {
                if(hex.DistanceText.EvaluateDistance(initHex))
                {
                    OptimalPath.nextStep = hex;
                    break;
                }
            }
        }
    }
}
