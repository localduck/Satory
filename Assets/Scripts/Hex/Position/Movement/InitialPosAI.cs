using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class InitialPosAI : MonoBehaviour, IInitialHexes
    {
        private List<HexBattleground> initialHexes = new List<HexBattleground>();
        public List<HexBattleground> GetNewInitialHexes()
        {
            initialHexes.Clear();
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                if (hex.IsNeighbourHex & !hex.IsIncluded
                    && ifThereIsPlayersRegiment(hex))
                {
                    initialHexes.Add(hex);
                }
            }
            return initialHexes;
        }

        private bool ifThereIsPlayersRegiment(HexBattleground evaluatedHex)
        {
            bool AIPosfalse = true;
            if (evaluatedHex.GetComponentInChildren<Hero>() != null &&
                evaluatedHex.GetComponentInChildren<Enemy>() == null)
            {
                evaluatedHex.PotentialTergetDefined();
                AIPosfalse = false;
            }
            return AIPosfalse;
        }
    }
}