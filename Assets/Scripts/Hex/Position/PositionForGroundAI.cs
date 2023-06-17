using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class PositionForGroundAI : MonoBehaviour, INeighborFinder
    {
        string caseMethod = "EvaluateGroundAI";
        public void GetAdjacentHexesExtended(HexBattleground initHex)
        {
            List<HexBattleground> neighboursToCheck = HexCalculation.GetHexNeighbours(initHex, caseMethod);

            foreach (HexBattleground hex in neighboursToCheck)
            {
                if (hex.DistanceText.EvaluateDistanceForGround(initHex))
                {
                    hex.IsNeighbourHex = true;
                    hex.DistanceText.SetDistanceFromGroundUnits(initHex);
                }
            }
        }
    }
}
