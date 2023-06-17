using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Satory23
{
    public class DeploymentManager : MonoBehaviour
    {
        public static CharIcon readForDeploymentIcon;
        private List<HexBattleground> enemiesPositions;
        private List<UnitAttributes> enemiesDeployment;
        public static StorageLogic storage;
        private int enemiesNum;
        // Start is called before the first frame update
        private void Start()
        {
            ActivatePositionForRegiment();
            enemiesPositions = new List<HexBattleground>();
            enemiesDeployment = new List<UnitAttributes>();
            storage = FindObjectOfType<StorageLogic>();
            enemiesDeployment = storage.CurrentProgress.UnitsOfEnemies;
            enemiesNum = enemiesDeployment.Count();
            PlaceEnemies();
        }

        private void PlaceEnemies()
        {
            List<HexBattleground> enemiesPositions = GetEnemiesPosition();
            for (int i = 0; i < enemiesNum; i++)
            {
                int positionNum = enemiesPositions.Count();
                int randomIndex = Random.Range(0, positionNum - 1);
                Image visualModel = enemiesPositions[randomIndex].VisualModel;
                InstantiateEnemy(enemiesDeployment[i], visualModel);
                enemiesPositions.RemoveAt(randomIndex);
            }
        }

        private void InstantiateEnemy(UnitAttributes unitAttributes, Image hexPosition)
        {
            Hero enemy = Instantiate(unitAttributes.heroSO, hexPosition.transform);
            enemy.HeroData = unitAttributes;
            enemy.SetAttributes();
            enemy.gameObject.AddComponent<Enemy>();
            enemy.gameObject.AddComponent<AllPosForGroundAI>();
        }

        public List<HexBattleground> GetEnemiesPosition()
        {
            enemiesPositions.Clear();
            foreach(HexBattleground hex in FieldManager.activeHexList)
            {
                if(hex.DeploymentPos.RegimentPosition == PositionForRegiment.enemy)
                {
                    enemiesPositions.Add(hex);
                }
            }
            return enemiesPositions;
        }

        public static void DeployRegiment(HexBattleground parentHex)
        {
            Hero regiment = readForDeploymentIcon.unitAttributes.heroSO;
            regiment.HeroData = readForDeploymentIcon.unitAttributes;
            Hero fighter = Instantiate(regiment, parentHex.VisualModel.transform);

            parentHex.ClearDeploymentPosition();
            readForDeploymentIcon.UnitIsDeploed();
            readForDeploymentIcon = null;
            storage.GetComponent<StartButton>().ControlStartButton();
        }

        public void ActivatePositionForRegiment()
        {
            foreach (HexBattleground hex in FieldManager.m_HexesArray)
            {
                if (hex.DeploymentPos.RegimentPosition == PositionForRegiment.player)
                {
                    hex.SetDeploymentPosition();
                }
            }
        }
    }
}
