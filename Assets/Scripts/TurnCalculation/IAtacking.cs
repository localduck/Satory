using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public interface IAtacking
    {
        void HeroIsDealingDamage(Hero atacker, Hero Target);
    }
}