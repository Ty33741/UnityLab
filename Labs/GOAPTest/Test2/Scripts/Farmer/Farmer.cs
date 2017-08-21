using System.Collections.Generic;
using Assets.Scripts.AI.GOAP;
using Assets.Scripts.Game.Character;
using UnityEngine;

namespace Assets.Labs.GOAPTest.Test2.Scripts
{
    public class Farmer : SimpleGoapCharacter
    {
        public GameObject Tool;
        
        public void EquipTool()
        {
            Tool.SetActive(true);
        }

        public void DestroyTool()
        {
            Tool.SetActive(false);
        }

        public override HashSet<KeyValuePair<string, object>> GetWorldStates()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("findFood", false),
                new KeyValuePair<string, object>("hasTool", Tool.activeInHierarchy)
            };
        }

        public override HashSet<KeyValuePair<string, object>> GetGoals()
        {
            return new HashSet<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("collectFood", true)
            };
        }
    }
}
