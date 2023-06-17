using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class MeleeAtack : IAtacking
    {
        DamageCounter damageController = new DamageCounter();
        int targetStack;
        public void HeroIsDealingDamage(Hero atacker, Hero Target)
        {
            targetStack = damageController.CountTargetStack(atacker, Target);
            int currentInt = Target.HeroData.StackCurrent;
            Target.HeroData.StackCurrent = targetStack;
            Target.stack.StartCoroutine(Target.stack.CountDownToTargetStack(currentInt, targetStack));
        }
    }
}
