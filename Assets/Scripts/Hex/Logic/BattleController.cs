using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Satory23
{
    public class BattleController : MonoBehaviour
    {
        public static HexBattleground targetToMove;
        public static Hero currentAtacker;
        public static Hero currentTarget;
        private List<Hero> allFighters = new List<Hero>();
        public int LengthOfWholeField; //number of iteration to check the entire battlefield;
        public List<HexBattleground> potentialTargets = new List<HexBattleground>();//collects all player's regiments
        private Turn turn;
        public EventSystem events;//to disable click response

        private void Start()
        {
            turn = GetComponent<Turn>();
            events = FindObjectOfType<EventSystem>();
        }

        public List<Hero> DefineAllFighters()
        {
            allFighters = FindObjectsOfType<Hero>().ToList();
            return allFighters;
        }
        public void DefineNewAtacker()
        {
            //   sorts fighters by initiative value, in descending order
            List<Hero> allFighters = DefineAllFighters().
                                     OrderByDescending(hero => hero.HeroData.InitiativeCurrent).ToList();
            //  the first element of the list has the biggest initiative value
            currentAtacker = allFighters[0];
            currentAtacker.HeroData.InitiativeCurrent = 0;
        }
        public void CleanField()
        {
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                hex.SetDefaultValue();
            }
        }

        public void RemoveHeroWhenItIsKilled(Hero hero)
        {
            hero.GetComponentInParent<HexBattleground>().potentialTarget = false;
            Destroy(hero.gameObject);
            IsLookingForPotentialTargets();
            turn.TurnIsCompleted();
        }
        public List<HexBattleground> IsLookingForPotentialTargets()//collects all player's regiments into a list
        {
            potentialTargets.Clear();
            foreach (HexBattleground hex in FieldManager.activeHexList)
            {
                //checks if the hex is marked as occupied by a player’s regiment
                if (hex.potentialTarget && hex.GetComponentInChildren<Hero>() != null)
                {
                    potentialTargets.Add(hex);
                }
            }
            return potentialTargets;
        }
    }
}