using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class StunAtack : IAtacking
    {
        private DamageCounter damageController = new DamageCounter();
        private int targetStack;
        public void HeroIsDealingDamage(Hero atacker, Hero Target)
        {
            targetStack = damageController.CountTargetStack(atacker, Target);
            int currentInt = Target.HeroData.StackCurrent;
            Stun(Target);
            Target.HeroData.StackCurrent = targetStack;
            Target.stack.StartCoroutine(Target.stack.CountDownToTargetStack(currentInt, targetStack));
        }

        private void Stun(Hero Target)
        {
            Target.HeroData.InitiativeCurrent = 0;
        }
    }
}
