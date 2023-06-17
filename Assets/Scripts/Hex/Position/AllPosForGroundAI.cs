using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class AllPosForGroundAI : MonoBehaviour
    {
        private int step;
        private List<HexBattleground> initialHexes = new List<HexBattleground>();
        string caseMethod = "BoolAIObserveTargets";

        //looks for all positions available
        public void GetAvailablePositions(int stepsLimit, IInitialHexes getHexesToCheck, HexBattleground startingHex)
        {
            GetAdjacentHexesExtended(stepsLimit, startingHex);
            for (step = 2; step <= stepsLimit; step++)
            {
                initialHexes = getHexesToCheck.GetNewInitialHexes();
                foreach (HexBattleground hex in initialHexes)
                {
                    GetAdjacentHexesExtended(stepsLimit, hex);
                }
            }
        }
        public void GetAdjacentHexesExtended(int stepsLimit, HexBattleground initialHex)
        {
            List<HexBattleground> neighboursToCheck = HexCalculation.GetHexNeighbours(initialHex, caseMethod);
            foreach (HexBattleground hex in neighboursToCheck)
            {
                if (hex.DistanceText.EvaluateDistanceForGroundAI(initialHex, stepsLimit))
                {
                    hex.IsNeighbourHex = true;
                    hex.DistanceText.SetDistanceFromGroundUnits(initialHex);
                }
            }
        }
    }
}