using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class AnimatorEventRedirect : MonoBehaviour
    {
        [SerializeField]
        private Hero m_HeroLink;

        private void Start()
        {
            if(m_HeroLink == null) m_HeroLink = GetComponentInParent<Hero>();
        }

        public void AnimationEventCallDealsDamage(HexBattleground target)
        {
            m_HeroLink.DealsDamage(target);
        }

        public void AnimationEventCallHeroIsKilled()
        {
            m_HeroLink.HeroIsKilled();
        }
    }
}
