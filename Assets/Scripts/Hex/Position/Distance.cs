using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class Distance : MonoBehaviour
    {
        [SerializeField] private int distanceFromCurrentPoint;
        [SerializeField] private int stepsToGo;
        public int defaultDistance;//Default value of the distanceFromStartingPoint variable
        public int defaultstepsToGo;
        private HexBattleground hex;
        private Text distanceText;

        public int StepsToGo
        {
            get { return stepsToGo; }
            set { stepsToGo = value; }
        }

        public int DistanceFromCurrentPoint
        {
            get { return distanceFromCurrentPoint; }
            set { distanceFromCurrentPoint = value; }
        }
        // Start is called before the first frame update
        private void Start()
        {
            hex = GetComponentInParent<HexBattleground>();
            distanceText = GetComponent<Text>();
        }

        //add to HexCalculation
        public void SetDistanceFromGroundUnits(HexBattleground initHex)
        {
            distanceFromCurrentPoint = initHex.DistanceText.distanceFromCurrentPoint + initHex.DistanceText.stepsToGo;
            DisplayDistanceText();
        }

        public void SetDistanceFromFlyingUnits(HexBattleground initHex)
        {
            stepsToGo = 1;
            distanceFromCurrentPoint = initHex.DistanceText.distanceFromCurrentPoint + stepsToGo;
            DisplayDistanceText();
        }

        public void SetDistanceForGroundUnit(HexBattleground initialHex)
        {
            //add a step to the previous step to get diastance from starting point
            distanceFromCurrentPoint = initialHex.DistanceText.distanceFromCurrentPoint
                        + initialHex.DistanceText.stepsToGo;
        }

        private void DisplayDistanceText()
        {
            distanceText.text = distanceFromCurrentPoint.ToString();
            Color tmpColor = distanceText.color;
            tmpColor.a = 255;
            distanceText.color = tmpColor;
        }

        public bool EvaluateDistance(HexBattleground initHex)
        {
            return distanceFromCurrentPoint + stepsToGo == initHex.DistanceText.distanceFromCurrentPoint;
        }

        public int AddToPartOfOptimalPath()
        {
            OptimalPath.optimalPath.Add(hex);
            hex.VisualModel.color = new Color32(150, 150, 150, 255);
            return stepsToGo;
        }

        public bool EvaluateDistanceForGround(HexBattleground initHex)
        {
            int currentDistance = initHex.DistanceText.distanceFromCurrentPoint + initHex.DistanceText.stepsToGo;
            int stepsLimit = BattleController.currentAtacker.HeroData.VelocityCurrent;
            return distanceFromCurrentPoint > currentDistance && stepsLimit >= currentDistance;
        }
        public bool EvaluateDistanceForGroundAI(HexBattleground initHex, int stepsLimit)
        {
            //distance to reach initial hex and get out of it
            int currentDistance = initHex.DistanceText.DistanceFromCurrentPoint
                                  + initHex.DistanceText.stepsToGo;
            return DistanceFromCurrentPoint > currentDistance &&
                    stepsLimit >= currentDistance;
        }
    }
}
