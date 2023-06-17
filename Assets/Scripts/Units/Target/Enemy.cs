using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Satory23
{
    public class Enemy : MonoBehaviour
    {
        private BattleController battleController; //class's reference
        private AllPosForGroundAI tocheckTheField; 
        public List<HexBattleground> PosToOccupy = new List<HexBattleground>();
        private List<HexBattleground> allTargets = new List<HexBattleground>();
        private List<HexBattleground> closeTargets = new List<HexBattleground>();
        private HexBattleground hexToOccupy;
        private AvalibleHexPos availablePos;
        private UniteMove move;
        private Hero hero;

        private void Start()
        {
            battleController = FindObjectOfType<BattleController>();
            tocheckTheField = GetComponent<AllPosForGroundAI>();
            hero = GetComponent<Hero>();
            availablePos = GetComponent<AvalibleHexPos>();
            move = GetComponent<UniteMove>();
            move.LookingRightWay = false;
        }

        public void AITurnStart(IInitialHexes getInitialHexes)
        {
            int stepsLimit = battleController.LengthOfWholeField;
            HexBattleground startintHex = GetComponentInParent<HexBattleground>();

            tocheckTheField.GetAvailablePositions(stepsLimit, getInitialHexes, startintHex);
            CollectAllPosToOccupy();
            AIMakesDecision();
        }

        List<HexBattleground> CollectAllPosToOccupy()
        {
            PosToOccupy.Clear();
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                if (hex.DistanceText.DistanceFromCurrentPoint <= hero.HeroData.VelocityCurrent)
                {
                    PosToOccupy.Add(hex);
                }
            }
            return PosToOccupy;
        }


        private List<HexBattleground> CheckIfAttackIsAvailable()
        {
            int currentVelocity = BattleController.currentAtacker.HeroData.VelocityCurrent;
            closeTargets.Clear();
            List<HexBattleground> allTargets = battleController.IsLookingForPotentialTargets();
            foreach (HexBattleground hex in allTargets)
            {
                if (hex.DistanceText.DistanceFromCurrentPoint <= currentVelocity + 1)
                {
                    closeTargets.Add(hex);
                }
            }
            return closeTargets;
        }
        public HexBattleground AISelectsTargetToAttack()
        {
            allTargets.Clear();
            if (CheckIfAttackIsAvailable().Count > 0)
            {
                allTargets = CheckIfAttackIsAvailable().
                             OrderBy(hero => hero.GetComponentInChildren<Hero>().HeroData.HPCurrent).ToList();
            }
            else
            {
                //sort all player's regiments first by the distance to the target, then by HP property
                allTargets = battleController.IsLookingForPotentialTargets().OrderBy(hero => hero.DistanceText.DistanceFromCurrentPoint).
                            ThenBy(hero => hero.GetComponentInChildren<Hero>().HeroData.HPCurrent).ToList();
            }
            BattleController.currentTarget = allTargets[0].GetComponentInChildren<Hero>();
            return allTargets[0];
        }
        void AIIStartsMoving(HexBattleground targetToAttack)//determines the distance from the attack target to each hex
        {
            battleController.CleanField();
            targetToAttack.SetAsCurrentHex();
            int stepsLimit = battleController.LengthOfWholeField;
            IInitialHexes getInitialHexes = new InitialPos();

            //determines the distance from the attack target to each hex
            tocheckTheField.GetAvailablePositions(stepsLimit, getInitialHexes, targetToAttack);
            INeighborFinder adjFinder = BattleController.currentAtacker.GetTypeOfHero();
            AIDefinesPath(adjFinder);
        }
        private HexBattleground AISelectsPosToOcuppy()
        {
            List<HexBattleground> OrderedPos = PosToOccupy.OrderBy(s => s.DistanceText.DistanceFromCurrentPoint).ToList();
            for (int i = 0; i < OrderedPos.Count; i++)
            {

                if (OrderedPos[i].GetComponentInChildren<Hero>() == null)
                {
                    hexToOccupy = OrderedPos[i];
                    break;
                }
            }

            return hexToOccupy;
        }
        void AIMakesDecision()
        {
            HexBattleground targetToAttack = AISelectsTargetToAttack();
            if (targetToAttack.DistanceText.DistanceFromCurrentPoint > 1)
            {
                AIIStartsMoving(targetToAttack);
            }
            else
            {
                hero.HeroIsAtacking();
            }
        }
        void AIDefinesPath(INeighborFinder adjFinder)
        {
            BattleController.targetToMove = AISelectsPosToOcuppy();
            battleController.CleanField();
            IInitialHexes getInitialHexes = new InitialPos();
            int stepsLimit = hero.HeroData.VelocityCurrent;
            HexBattleground startingHex = BattleController.currentAtacker.GetComponentInParent<HexBattleground>();
            startingHex.SetAsCurrentHex();

            availablePos.GetAvailablePositions(stepsLimit, adjFinder, getInitialHexes);
            GetComponent<OptimalPath>().MatchPath();
            move.StartsMoving();
        }
    }
}
