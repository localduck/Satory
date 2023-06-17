using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class StackTxt : MonoBehaviour
    {
        private Hero parentHero;
        private Text stackText;
        private int m_stack;
        [SerializeField] 
        private float iterationCntrl;
        private int iterationVal;
        public int IterationVal
        {
            get { return iterationVal; }
            set
            {
                if(value < 1) { iterationVal = 1; }
                else { iterationVal = value; }
            }
        }
        Turn turn;

        // Start is called before the first frame update
        private void Start()
        {
            parentHero = GetComponentInParent<Hero>();
            stackText = GetComponent<Text>();
            DisplayCurrentStack(parentHero.HeroData.StackCurrent);
            turn = FindObjectOfType<Turn>();
        }

        public void DisplayCurrentStack(int currentStack)
        {
            parentHero.HeroData.StackCurrent = currentStack;
            stackText.text = currentStack.ToString();
        }

        public IEnumerator CountDownToTargetStack(int currentValue, int targetValue)
        {
            int delta = currentValue - targetValue;

            IterationVal = Mathf.FloorToInt(delta * Time.deltaTime / iterationCntrl);
            while (currentValue >= targetValue + IterationVal)
            {
                currentValue -= IterationVal;
                DisplayCurrentStack(currentValue);
                yield return null;
            }
            DisplayCurrentStack(targetValue);
            CheckIfHeroIsKilled();
        }

        private void CheckIfHeroIsKilled()
        {

            if (parentHero.HeroData.StackCurrent == 0)
            {
                parentHero.GetComponentInChildren<Animator>().SetTrigger("IsDead");
            }
            else
            {
                turn.TurnIsCompleted();
            }
        }
    }
}
