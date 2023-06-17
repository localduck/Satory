using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class Projectile : MonoBehaviour
    {
        private Vector3 targetPosition;
        [SerializeField] private Vector3 adjustingTargetPosition;
        public bool ArrowFlies = false;
        [SerializeField] private float velocity;
        IAtacking dealsDamage = new MeleeAtack();

        private void Update()
        {
            if(ArrowFlies)
            {
                transform.position =  Vector2.MoveTowards(transform.position, targetPosition, velocity * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, transform.position.y, targetPosition.z);
                if(Vector2.Distance(transform.position, targetPosition) < 0.1f)
                {
                    ArrowFlies = false;
                    Hero currentTarget = BattleController.currentTarget;
                    dealsDamage.HeroIsDealingDamage(BattleController.currentAtacker, currentTarget);
                    currentTarget.GetComponentInChildren<Animator>().SetTrigger("IsHited");
                    DestroySelf();
                }
            }
        }

        public void Fire()
        {
            Vector3 currentTargetPosition = BattleController.currentTarget.transform.position;
            targetPosition = currentTargetPosition + adjustingTargetPosition;
            ArrowFlies = true;
        }

        private void DestroySelf()
        {
            Destroy(gameObject);
        }
    }
}
