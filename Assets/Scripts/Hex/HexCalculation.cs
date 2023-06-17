using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

namespace Satory23
{
    static public class HexCalculation
    {
        static public List<HexBattleground> GetHexNeighbours(HexBattleground currentHex, IEvaluateHex checkHex)
        {
            List<HexBattleground> m_Neighbours = new List<HexBattleground>();

            int initialX = currentHex.HorizontalCoordinate - 1;
            int initialY = currentHex.VerticalCoordinate - 1; 

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (Mathf.Abs(x + y) != 2 //exclude two hexes that are not adjacent hexes
                         && checkHex.EvaluateHex(FieldManager.m_HexesArray[initialX + x, initialY + y])
                         && FieldManager.m_HexesArray[initialX + x, initialY + y] != currentHex) //exclude inactive hexes
                    {
                        /*print($"{initialX + x}, {initialY + y}");*/
                        m_Neighbours.Add(FieldManager.m_HexesArray[initialX + x, initialY + y]);
                        FieldManager.m_HexesArray[initialX + x, initialY + y].SetAvalible();
                    }
                }
            }

            return m_Neighbours;
        }

        static public List<HexBattleground> GetHexNeighbours(HexBattleground currentHex, string caseMethod)
        {
            List<HexBattleground> m_Neighbours = new List<HexBattleground>();

            int initialX = currentHex.HorizontalCoordinate - 1;
            int initialY = currentHex.VerticalCoordinate - 1;

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (Mathf.Abs(x + y) != 2 //exclude two hexes that are not adjacent hexes
                         && EvaluateHex(FieldManager.m_HexesArray[initialX + x, initialY + y], caseMethod)
                         && FieldManager.m_HexesArray[initialX + x, initialY + y] != currentHex) //exclude inactive hexes
                    {
                        /*print($"{initialX + x}, {initialY + y}");*/
                        m_Neighbours.Add(FieldManager.m_HexesArray[initialX + x, initialY + y]);
                        FieldManager.m_HexesArray[initialX + x, initialY + y].SetAvalible();
                    }
                }
            }

            return m_Neighbours;
        }
        #region Evalueate Calc

        static public bool EvaluateIfIsNewHex(HexBattleground evaluetedHex)
        {
            return evaluetedHex.HexBattleState == HexState.active
                                && !evaluetedHex.IsCurrentHex
                                && !evaluetedHex.IsNeighbourHex;
        }

        static public bool EvaluateHex(HexBattleground evaluatedHex, string caseMethod)
        {//BoolTargetRange
            switch (caseMethod)
            {
                case "BoolTargetRange":
                    return evaluatedHex.HexBattleState == HexState.active
                        && !evaluatedHex.IsCurrentHex
                        && !evaluatedHex.lookingForTarget;
                case "EvaluateFlying":
                    return evaluatedHex.HexBattleState == HexState.active
                        && !evaluatedHex.IsCurrentHex
                        && !evaluatedHex.IsNeighbourHex
                        && evaluatedHex.GetComponentInChildren<Enemy>() == null;
                case "EvaluateGroundAI":
                    return evaluatedHex.HexBattleState == HexState.active
                        && !evaluatedHex.IsCurrentHex
                        && !evaluatedHex.IsIncluded
                        && evaluatedHex.AvailableToGround()
                        && boolAIStay(evaluatedHex);
                case "EvaluateGround":
                    return evaluatedHex.HexBattleState == HexState.active
                        && !evaluatedHex.IsCurrentHex
                        && !evaluatedHex.IsNeighbourHex
                        && evaluatedHex.AvailableToGround()
                        && evaluatedHex.GetComponentInChildren<Enemy>() == null;
                case "BoolAIObserveTargets":
                    return evaluatedHex.HexBattleState == HexState.active
                        && !evaluatedHex.IsCurrentHex
                        && evaluatedHex.AvailableToGround();
                case "BoolTarget":
                    return BoolTarget(evaluatedHex);
                case "PathCheck":
                    return evaluatedHex.IsNeighbourHex;
                default:
                    return false;
            }
        }

        public static bool boolAIStay(HexBattleground evaluatedHex)
        {
            bool AIHexPosition = true;
            if (evaluatedHex.GetComponentInChildren<Hero>() != null &&
                evaluatedHex.GetComponentInChildren<Enemy>() == null)
            {
                AIHexPosition = false;
            }
            return AIHexPosition;
        }

        public static bool BoolTarget(HexBattleground evaluatedHex)
        {
            //check whether the hero is on the hex or not and
            //whether this object contains the Enemy component
            if (BattleController.currentAtacker.GetComponent<Enemy>() == null)
            {
                return evaluatedHex.GetComponentInChildren<Enemy>() != null;
            }
            else
            {
                return evaluatedHex.GetComponentInChildren<Hero>() != null &&
                evaluatedHex.GetComponentInChildren<Enemy>() == null;
            }
        }
        #endregion
        static public void EditePrefabsDelete(string prefabAssetPath)
        {
            //CALL: EditePrefabsDelete("Assets/Prefabs/HexagonPrefabs/Autumn/Grass/Mistake/");
            Debug.Log(prefabAssetPath);
            DirectoryInfo d = new DirectoryInfo(prefabAssetPath); //Assuming Test is your Folder

            FileInfo[] Files = d.GetFiles("*.prefab"); //Getting Text files

            foreach (FileInfo file in Files)
            {
                string str = "";
                str = prefabAssetPath + file.Name;
                Debug.Log(str);
                GameObject activePrefab = PrefabUtility.LoadPrefabContents(str); // Load the prefab in order to edit it.
                GameObject childToDelete = activePrefab.transform.GetChild(1).gameObject; // Get some child that you want to delete.
                if(childToDelete.name == "DistanceText" || childToDelete.name == "ArmyPosition")
                {
                    GameObject.DestroyImmediate(childToDelete, true); // Make sure you pass true to "allowDestroyingAssets"
                    PrefabUtility.SaveAsPrefabAsset(activePrefab, str); // Save the prefab.
                }
                PrefabUtility.UnloadPrefabContents(activePrefab); // Unload the prefab to stop editing it.
            }
        }
    }
}
