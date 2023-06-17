using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class UniteMove : MonoBehaviour
    {
        public bool isMoving = false;//enables and disables motion
        Hero hero;
        public List<Image> path;
        private int totalSteps;//number of hexes included in Optimal path
        private int currentStep;//list index defining the current target for movement
        Vector3 targetPos;
        float speedOfAnim = 8f;//determines the speed of movement
        private bool lookingRightWay = true;
        public bool LookingRightWay
        {
            get { return lookingRightWay; }
            set { lookingRightWay = value; }
        }

        private SpriteRenderer heroSprite;
        private BattleController battleController;
        void Start()
        {
            hero = GetComponent<Hero>();
            heroSprite = GetComponentInChildren<SpriteRenderer>();
            battleController = FindObjectOfType<BattleController>();
        }

        void Update()
        {
            if (isMoving) HeroIsMoving();
        }
        public void StartsMoving()
        {
            battleController.events.gameObject.SetActive(false);
            battleController.CleanField();
            currentStep = 0;
            totalSteps = path.Count - 1;
            isMoving = true;
            hero.GetComponentInChildren<Animator>().SetBool("IsMoving", true);
            ResetTargetPos();
        }
        private void ResetTargetPos()
        {
            targetPos = new Vector3(path[currentStep].transform.position.x,
          path[currentStep].transform.position.y,
          transform.position.z);//defines next step changing the value of currentStep variable
            ControlDirection(targetPos);
        }
        private void HeroIsMoving()
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos,
                            speedOfAnim * Time.deltaTime);
            ManageSteps();
        }
        private void ManageSteps()
        {
            if (Vector3.Distance(transform.position, targetPos) < 0.1f
          && currentStep < totalSteps)
            {
                currentStep++;
                ResetTargetPos();
            }
            else if (Vector3.Distance(transform.position, targetPos) < 0.1f)
            {
                StopsMoving();
            }
        }
        private void StopsMoving()
        {
            isMoving = !isMoving;
            transform.parent = path[currentStep].transform;
            hero.GetComponentInChildren<Animator>().SetBool("IsMoving", false);
            hero.HeroData.VelocityCurrent = 0;
            hero.DefineTargets();
            battleController.events.gameObject.SetActive(true);
        }

        public void ControlDirection(Vector3 targetPos)
        {
            if(transform.position.x > targetPos.x && lookingRightWay ||
                transform.position.x < targetPos.x && !lookingRightWay)
            {
                heroSprite.flipX = !heroSprite.flipX;
                lookingRightWay = !lookingRightWay;
            }
        }
    }
}
