using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class Turn : MonoBehaviour
    {
        private BattleController battleController;
        /*IInitialHexes getInitialHexes = new InitialPos();*/
        public delegate void StartNewRound();
        public static event StartNewRound OnNewRound;
        [SerializeField] GameOver ResultPanel; //Panel prefab

        private void Start()
        {
            battleController = GetComponent<BattleController>();

            //a new turn is initialized by pressing the start button
            StartButton.OnStartingBattle += InitializeNewTurn;
        }
        public void InitializeNewTurn()
        {
            battleController.CleanField();
            battleController.DefineNewAtacker();//finds an attacking hero
            Hero currentAtacker = BattleController.currentAtacker;//gets local atacker (for parameters)
            GetStartingHex();
            if (currentAtacker.GetComponent<Enemy>() == null)//checks if it is a player’s turn
            {
                IInitialHexes getInitialHexes = new InitialPos();
                currentAtacker.PlayerTurn(getInitialHexes);

            }
            else
            {
                IInitialHexes getInitialHexes = new InitialPosAI();
                currentAtacker.GetComponent<Enemy>().AITurnStart(getInitialHexes);
            }
        }

        private void GetStartingHex()
        {
            HexBattleground startingHex = BattleController.currentAtacker.GetComponentInParent<HexBattleground>();
            startingHex.SetAsCurrentHex();
        }

        public void TurnIsCompleted()
        {
            StartCoroutine(NextTurnOrGameOver());
        }
        public IEnumerator NextTurnOrGameOver()
        {
            WaitForSeconds wait = new WaitForSeconds(1f);
            yield return wait;
            battleController.events.gameObject.SetActive(true);//enables click response
            List<Hero> allFighters = battleController.DefineAllFighters();
            if (IfThereIsAIRegiment(allFighters) && IfThereIsPlayerRegiment(allFighters))
            {
                NextTurnOrNextRound(allFighters);
            }
            else
            {
                battleController.CleanField();//clearing the battlefield from frames and numbers
                ResultPanel.gameObject.SetActive(true);
                ResultPanel.DefeatOrVictory(IfThereIsPlayerRegiment(allFighters));
                RemoveAllHeroes(allFighters);
            }
        }
        private void RemoveAllHeroes(List<Hero> allFighters)
        {
            foreach (Hero hero in allFighters)
            {
                Destroy(hero.gameObject);
            }
        }
        bool IfThereIsAIRegiment(List<Hero> allFighters)
        {
            return allFighters.Exists(x => x.gameObject.GetComponent<Enemy>());
        }
        bool IfThereIsPlayerRegiment(List<Hero> allFighters)
        {
            return allFighters.Exists(x => !x.gameObject.GetComponent<Enemy>());
        }
        private void NextTurnOrNextRound(List<Hero> allFighters)
        {
            if (allFighters.Exists(x => x.HeroData.InitiativeCurrent > 0))
            {
                InitializeNewTurn();
            }
            else
            {
                OnNewRound();
                InitializeNewTurn();
            }
        }
    }
}
