using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class OptimalPath : MonoBehaviour
    {
        public static List<HexBattleground> optimalPath = new List<HexBattleground>();
        public static HexBattleground nextStep;
        public List<Image> visualModels = new List<Image>();
        private HexBattleground targetHex;
        private INeighborFinder naighbornOption = new PositionForPath();
        private UniteMove move;

        public UniteMove Move
        {
            get { return move; }
            set { move = value; }
        }

        private void Start()
        {
            move = GetComponent<UniteMove>();
        }

        public void MatchPath()
        {
            optimalPath.Clear();
            targetHex = BattleController.targetToMove;
            optimalPath.Add(targetHex);

            int steps = targetHex.DistanceText.DistanceFromCurrentPoint;
            for (int i = steps; i > 1;)
            {
                naighbornOption.GetAdjacentHexesExtended(targetHex);
                targetHex = nextStep;
                i -= nextStep.DistanceText.AddToPartOfOptimalPath();
            }
            ManagePath();
        }

        public void ManagePath()
        {
            visualModels.Clear();
            optimalPath.Reverse();
            foreach (HexBattleground hex in optimalPath)
            {
                visualModels.Add(hex.VisualModel);
            }

            move.path = visualModels;
        }
    }
}