using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class AvalibleHexPos : MonoBehaviour
    {
        private int step;
        private List<HexBattleground> initialHexes = new List<HexBattleground>();

        public void GetAvailablePositions(int stepsLimit, INeighborFinder AdjFinder, IInitialHexes getHexsToCheck)
        {

            HexBattleground startingHex = BattleController.currentAtacker.GetComponentInParent<HexBattleground>();
            AdjFinder.GetAdjacentHexesExtended(startingHex);

            for (step = 2; step <= stepsLimit; step++)
            {
                initialHexes = getHexsToCheck.GetNewInitialHexes();
                foreach (HexBattleground hex in initialHexes)
                {
                    AdjFinder.GetAdjacentHexesExtended(hex);
                    hex.IsIncluded = true;
                }
            }
        }
        /*internal List<HexBattleground> GetNewInitialHexes()
        {//old
            initialHexes.Clear();
            foreach (HexBattleground hex in FieldManager.m_HexesArray)
            {
                if (hex.IsNeighbourHex & !hex.IsIncluded)
                {
                    initialHexes.Add(hex);
                }
            }
            return initialHexes;
        }*/
    }
}
