using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class VelocityDecrease : IAtacking
    {
        private int targetStack;
        public void HeroIsDealingDamage(Hero atacker, Hero Target)
        {
            Decrease(Target);
            Target.stack.StartCoroutine(Target.stack.CountDownToTargetStack(Target.HeroData.StackCurrent, Target.HeroData.StackCurrent-1));
        }

        private void Decrease(Hero Target)
        {
            Target.HeroData.VelocityCurrent = (int) (Target.HeroData.VelocityCurrent - 0.5 * Target.HeroData.VelocityCurrent);
        }
    }
}
