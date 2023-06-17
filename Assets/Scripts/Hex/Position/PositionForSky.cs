using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class PositionForSky : MonoBehaviour, INeighborFinder
    {
        string caseMethod = "EvaluateFlying";
        public void GetAdjacentHexesExtended(HexBattleground initHex)
        {
            List<HexBattleground> neighboursToCheck = HexCalculation.GetHexNeighbours(initHex, caseMethod);

            foreach (HexBattleground hex in neighboursToCheck)
            {
                hex.IsNeighbourHex = true;
                hex.DistanceText.SetDistanceFromFlyingUnits(initHex);
            }
        }
    }
}
