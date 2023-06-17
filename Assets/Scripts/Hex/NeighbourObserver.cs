using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class NeighbourObserver : MonoBehaviour
    {
        static public List<HexBattleground> m_Neighbours = new List<HexBattleground>();
        private HexBattleground m_currentHex;
        /*private FieldManager m_sceneAPI;*/
        /*private HexBattleground[,] m_HexesArray;*/

        private void Start()
        {
            /*m_sceneAPI = FindObjectOfType<FieldManager>();
            m_HexesArray = m_sceneAPI.HexesArray;*/

            /*HexCalculation.GetHexNeighbours(GetComponentInParent<HexBattleground>(), out m_Neighbours);*/

            /*GetHexNeighbours();*/
        }

        private void GetHexNeighbours()
        {
            m_currentHex = GetComponentInParent<HexBattleground>();
            //subtract 1 since array's index starts from 1. 
            int initialX = m_currentHex.HorizontalCoordinate - 1; //first index
            int initialY = m_currentHex.VerticalCoordinate - 1; //second index
                                                                //iterates x and y from -1 to 1 to get adjacent hexes referring to the coordinates of starting hex
            /*print($"{initialX}, {initialY}");*/

            /*m_Neighbours.Add(m_HexesArray[initialX + 1, initialY]); //right diagonal
            m_Neighbours.Add(m_HexesArray[initialX + 1, initialY - 1]); //left diagonal
            m_Neighbours.Add(m_HexesArray[initialX - 1, initialY]); //right diagonal
            m_Neighbours.Add(m_HexesArray[initialX - 1, initialY + 1]); //left diagonal
            m_Neighbours.Add(m_HexesArray[initialX, initialY + 1]); //top
            m_Neighbours.Add(m_HexesArray[initialX, initialY - 1]); //botton*/
            /*m_Neighbours.Add(m_HexesArray[initialX + 1, initialY + 1]);*/
            /*m_Neighbours.Add(m_HexesArray[initialX - 1, initialY - 1]);*/

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (Mathf.Abs(x + y) != 2 //exclude two hexes that are not adjacent hexes
                         && FieldManager.m_HexesArray[initialX + x, initialY + y].HexBattleState
                           == HexState.active && FieldManager.m_HexesArray[initialX + x, initialY + y] != m_currentHex
                           && FieldManager.m_HexesArray[initialX + x, initialY + y].IsIncluded
                           && FieldManager.m_HexesArray[initialX + x, initialY + y].IsNeighbourHex) //exclude inactive hexes
                    {
                        /*print($"{initialX + x}, {initialY + y}");*/
                        m_Neighbours.Add(FieldManager.m_HexesArray[initialX + x, initialY + y]);
                        FieldManager.m_HexesArray[initialX + x, initialY + y].SetAvalible();
                    }
                }
            }
            /*print(m_Neighbours.Count);*/

            /*foreach (HexBattleground hex in m_Neighbours)
            {
                if (hex.VisualModel == null)
                {
                    hex.SetVisualModel();
                }
                hex.VisualModel.color = new Color32(120, 180, 200, 255);
            }*/
        }
    }
}
