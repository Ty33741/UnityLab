using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public class GoapPlanner
    {
        public class Node
        {
            public Node Parent;
            public GoapAction Action;
            public HashSet<KeyValuePair<string, object>> States;
            public int TotalCost;

            public Node(Node parent, GoapAction action,
                HashSet<KeyValuePair<string, object>> states, int totalCost)
            {
                Parent = parent;
                Action = action;
                States = states;
                TotalCost = totalCost;
            }
        }


        // generate best plan of actions in stack
        public Stack<GoapAction> Plan(GoapAgent agent, GoapAction[] actions,
            HashSet<KeyValuePair<string, object>> worldStates,
            HashSet<KeyValuePair<string, object>> goals)
        {
            // 1. reset all actions
            foreach (var action in actions)
            {
                action.DoReset();
            }

            // 2. single out all valid actions
            GoapAction[] availableActions =
                actions.Where(action => action.CheckProceduralPrecondition(agent)).ToArray();
            //HashSet<GoapAction> availableActions = new HashSet<GoapAction>();
            //foreach (var action in actions)
            //{
            //    if (action.CheckProceduralPrecondition(agent))
            //    {
            //        availableActions.Add(action);
            //    }
            //}

            // 3. build a plan tree
            List<Node> plans = new List<Node>();
            Node head = new Node(null, null, worldStates, 0);

            bool success = CheckPreconditions(worldStates, goals);
            if (success)// if goals reached, return empty stack
            {
                return new Stack<GoapAction>();
            }
            success = BuildTree(head, plans, availableActions, goals);
            if (!success)
            {
                return null;
            }

            // 4. find plan of minimal cost
            Node bestPlan = null;
            foreach (var plan in plans)
            {
                if (bestPlan == null || bestPlan.TotalCost > plan.TotalCost)
                {
                    bestPlan = plan;
                }
            }

            // 5. build result
            Stack<GoapAction> result = new Stack<GoapAction>();
            while (bestPlan != null && bestPlan.Action != null)
            {
                result.Push(bestPlan.Action);
                bestPlan = bestPlan.Parent;
            }

            return result;
        }


        // build planning tree
        private bool BuildTree(Node node, List<Node> plans, GoapAction[] actions,
            HashSet<KeyValuePair<string, object>> goal)
        {
            bool success = false;
            foreach (var action in actions)
            {
                // check preconditions
                if (!CheckPreconditions(node.States, action.GetPreconditions()))
                {
                    continue;
                }

                // apply effect and set child node
                HashSet<KeyValuePair<string, object>> states = ApplyEffects(node.States, action.GetEffects());
                Node child = new Node(node, action, states, node.TotalCost + action.Cost);

                // check goal
                if (CheckPreconditions(child.States, goal))
                {
                    plans.Add(child);
                    success = true;
                }
                else // not yet, build sub tree
                {
                    GoapAction[] actionSubSet = ActionSubSet(actions, action);
                    if (BuildTree(child, plans, actionSubSet, goal))
                    {
                        success = true;
                    }
                }
            }
            return success;
        }


        // check if current states contains all preconditions
        private bool CheckPreconditions(
            HashSet<KeyValuePair<string, object>> states,
            HashSet<KeyValuePair<string, object>> preconditions)
        {
            foreach (var precondition in preconditions)
            {
                if (!states.Contains(precondition))
                {
                    return false;
                }
            }
            return true;
        }


        // apply effects to a clone of current states, and then return the clone
        private HashSet<KeyValuePair<string, object>> ApplyEffects(
            HashSet<KeyValuePair<string, object>> states,
            HashSet<KeyValuePair<string, object>> effects)
        {
            // clone
            HashSet<KeyValuePair<string, object>> result = new HashSet<KeyValuePair<string, object>>();
            foreach (var state in states)
            {
                result.Add(state);
            }

            // apply effects
            foreach (var effect in effects)
            {
                //seach clone for the same key...
                bool success = false;
                foreach (var r in result)
                {
                    if (r.Key.Equals(effect.Key))
                    {
                        success = true;
                        break;
                    }
                }

                // if key matched, overwrite the value...
                if (success)
                {
                    result.RemoveWhere(r => r.Key.Equals(effect.Key));
                    result.Add(new KeyValuePair<string, object>(effect.Key, effect.Value));
                }
                else// else add a new key-value pair
                {
                    result.Add(new KeyValuePair<string, object>(effect.Key, effect.Value));
                }
            }

            return result;
        }


        // remove one and return a subset of actions
        private GoapAction[] ActionSubSet(GoapAction[] actions, GoapAction actionToRemove)
        {
            return actions.Where(action => !action.Equals(actionToRemove)).ToArray();
            //HashSet<GoapAction> result = new HashSet<GoapAction>();
            //foreach (var action in actions)
            //{
            //    if (!action.Equals(actionToRemove))
            //    {
            //        result.Add(action);
            //    }
            //}
            //return result;
        }


    }
}