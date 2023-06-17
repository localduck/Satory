using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    [CreateAssetMenu(fileName = "UnitAttributes", menuName = "ScriptableObjects/Units/Attributes")]
    public class UnitAttributes : ScriptableObject
    {
        [Header("Default Attributes")]
        public int velocity;
        public float initiative;
        public int atack;
        public int hp;
        public int resistance;
        public int stack;

        [SerializeField] int atackdistance;

        [Header("General Attributes")]
        public bool isRanger;
        public bool isFlying;
        public Sprite unitSprite;
        public Hero heroSO;

        [Header("Current Attributes")]
        private float initiativeCurrent;
        public float InitiativeCurrent
        {
            get { return initiativeCurrent; }
            set { initiativeCurrent = value; }
        }
        int hpCurrent;
        public int HPCurrent
        {
            get { return hpCurrent; }
            set { hpCurrent = value; }
        }

        int atackCurrent;
        public int AtackCurrent
        {
            get { return atackCurrent; }
            set { atackCurrent = value; }
        }
        int velocityCurrent;
        public int VelocityCurrent
        {
            get { return velocityCurrent; }
            set { velocityCurrent = value; }
        }
        int resistanceCurrent;
        public int ResistanceCurrent
        {
            get { return resistanceCurrent; }
            set { resistanceCurrent = value; }
        }
        int stackCurrent;
        public int StackCurrent
        {
            get { return stackCurrent; }
            set
            {
                if (stackCurrent > 0) { stackCurrent = value; }
                else { stackCurrent = 0; }
            }
        }
        public int Atackdistanse
        {
            get
            {
                if (!isRanger) { return 1; }
                else { return atackdistance; }
            }
        }

        public void SetCurrentAttributes()
        {
            hpCurrent = hp;
            atackCurrent = atack;
            resistanceCurrent = resistance;
            initiativeCurrent = initiative;
            velocityCurrent = velocity;
            stackCurrent = stack;
        }
        public void SetDefaultVelocityAndInitiative()
        {
            velocityCurrent = velocity;
            initiativeCurrent = initiative;
        }
        public int Calculatelosses()
        {
            return stack - stackCurrent;
        }
    }
}
