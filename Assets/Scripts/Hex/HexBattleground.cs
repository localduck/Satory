using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public enum HexState { inactive, active };
    public class HexBattleground : MonoBehaviour
    {
        [SerializeField] private int m_HorizontalCoordinate;
        [SerializeField] private int m_VerticalCoordinate;
        [SerializeField] private HexState m_HexBattleState;
        [SerializeField] private bool m_IsUpLvl = false;
        //NeighbourHex values group:
        [SerializeField] private bool m_IsCurrentHex = false;
        [SerializeField] private bool m_IsNeighbourHex = false;
        [SerializeField] private bool m_IsIncluded = false;
        //NeighbourHex && Movement scripts group:
        [SerializeField] private Image m_VisualModel;
        [SerializeField] protected Image m_CurrentStateImg;
        protected HexClicked m_ClickThis;
        protected Distance m_DistanceText;
        protected DeploymentPosition m_DeploymentPos;

        public bool potentialTarget;
        public bool lookingForTarget;

        public Distance DistanceText 
        {
            get { return m_DistanceText; }
            set { m_DistanceText = value; }
        }
        public DeploymentPosition DeploymentPos
        {
            get { return m_DeploymentPos; }
            set { m_DeploymentPos = value; }
        }

        public HexClicked ClickThis
        {
            get { return m_ClickThis; }
            set { m_ClickThis = value; }
        }

        public virtual void SetTargetToMove()
        {
            /*if (m_IsCurrentHex == false)
            {
                m_ClickThis.ClearPreviousSelectionOfTargetHex();
                m_ClickThis.IsTargetToMove = true;
                m_CurrentStateImg.color = new Color32(70, 255, 70, 255);
            }*/
            
            m_ClickThis.IsTargetToMove = true;
            BattleController.targetToMove = this;
            m_CurrentStateImg.color = new Color32(70, 255, 70, 255);
        }

        public bool IsNeighbourHex
        {
            set { m_IsNeighbourHex = value; }
            get { return m_IsNeighbourHex; }
        }
        public bool IsCurrentHex
        {
            set { m_IsCurrentHex = value; }
            get { return m_IsCurrentHex; }
        }
        public bool IsIncluded
        {
            set { m_IsIncluded = value; }
            get { return m_IsIncluded; }
        }
        public Image VisualModel
        {
            set { m_VisualModel = value; }
            get 
            {
                if (m_VisualModel == null) SetVisualModel();
                return m_VisualModel; 
            }
        }

        /*public int HorizontalCoordinate => m_HorizontalCoordinate;
        public int VerticalCoordinate => m_VerticalCoordinate;*/
        public bool IsUpLvl
        {
            set { m_IsUpLvl = value; }
            get { return m_IsUpLvl; }
        }
        public HexState HexBattleState
        {
            set { m_HexBattleState = value; }
            get { return m_HexBattleState;  }
        }
        public int HorizontalCoordinate
        {
            set { m_HorizontalCoordinate = value; }
            get { return m_HorizontalCoordinate; }
        }
        public int VerticalCoordinate
        {
            set { m_VerticalCoordinate = value; }
            get { return m_VerticalCoordinate; }
        }
        // Start is called before the first frame update

        public void SetActivate()
        {
            m_HexBattleState = HexState.active;
        }

        public void SetVisualModel()
        {
            /*m_VisualModel = transform.GetChild(0).GetComponent<Image>();*/
            foreach(Image img in GetComponentsInChildren<Image>())
            {
                if (img.name == "VisualModel")
                {
                    m_VisualModel = img;
                }
            }
        }
        protected void SetCurrentStateImg()
        {
            /*m_CurrentStateImg = transform.GetChild(0).GetChild(0).GetComponent<Image>();*/
            foreach (Image img in GetComponentsInChildren<Image>())
            {
                if (img.name == "VisualModel")
                {
                    m_CurrentStateImg = img.GetComponentInChildren<Image>();
                }
            }
        }
        public void SetInactivate()
        {

            m_HexBattleState = HexState.inactive;
            if(VisualModel != null) VisualModel.color = new Color32(70, 70, 70, 170);
        }

        public virtual void SetAvalible()
        {
            if (VisualModel == null)
            {
                SetVisualModel();
            }
            if(m_CurrentStateImg == null)
            {
                SetCurrentStateImg();
            }
            m_CurrentStateImg.color = new Color32(255, 255, 255, 255);
        }

        public void SetAsCurrentHex()
        {
            m_DistanceText.DistanceFromCurrentPoint = 0;
            m_IsCurrentHex = true;
            DistanceText.StepsToGo = 1;
        }

        public virtual bool AvailableToGround()
        {
            return true;
        }

        public void SetDeploymentPosition()
        {
            m_DeploymentPos.GetComponent<PolygonCollider2D>().enabled = true;
            m_DeploymentPos.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        public void ClearDeploymentPosition()
        {
            m_DeploymentPos.GetComponent<PolygonCollider2D>().enabled = false;
            m_DeploymentPos.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        }

        public void PotentialTergetDefined()
        {
            m_CurrentStateImg.color = new Color(255, 0, 0, 225);
            potentialTarget = true;
        }

        public void SetDefaultValue()
        {
            m_IsCurrentHex = false;
            m_IsNeighbourHex = false;
            m_IsIncluded = false;
            DistanceText.GetComponent<Text>().color = new Color32(255, 255, 255, 0);
            if (m_CurrentStateImg == null) SetCurrentStateImg();
            m_CurrentStateImg.color = new Color32(255, 255, 255, 0);
            m_VisualModel.color = new Color32(255, 255, 255, 255);
            lookingForTarget = false;
            m_DistanceText.DistanceFromCurrentPoint = m_DistanceText.defaultDistance;
            m_DistanceText.StepsToGo = m_DistanceText.defaultstepsToGo;
        }

        private void Awake()
        {
            m_ClickThis = GetComponent<HexClicked>();
            m_DistanceText = GetComponentInChildren<Distance>();
            m_DeploymentPos = GetComponentInChildren<DeploymentPosition>();
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}