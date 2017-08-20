using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.AI.FSM;
using UnityEngine;

namespace Assets.Scripts.AI.GOAP
{
    public class GoapAgent : MonoBehaviour
    {
        private Fsm _stateMachine;
        private Fsm.State _idleState;
        //private Fsm.State _moveState;
        private Fsm.State _performState;
        private GoapAction[] _actions;
        private Stack<GoapAction> _plan;
        private GoapPlanner _planner;
        private IGoap _dataProvider;
        private HashSet<KeyValuePair<string, object>> _actionData;

        private void Start()
        {
            _actionData = new HashSet<KeyValuePair<string, object>>();
            _planner = new GoapPlanner();

            _stateMachine = new Fsm();
            CreateIdleState();
            //CreateMoveState();
            CreatePerformState();

            FindDataProvider();
            LoadActions();

            _stateMachine.PushState(_idleState);
        }

        private void Update()
        {
            _stateMachine.Run(gameObject);
        }


        private void CreateIdleState()
        {
            _idleState = (fsm, fsmGameObject) =>
            {
                // reset data content
                ResetActionData();

                HashSet<KeyValuePair<string, object>> worldStates = _dataProvider.GetWorldStates();
                HashSet<KeyValuePair<string, object>> goals = _dataProvider.GetGoals();

                // planning
                _plan = _planner.Plan(this, _actions, worldStates, goals);

                // if has no plan...
                if (_plan == null)
                {
                    _dataProvider.PlanFailed(goals);
                }
                else
                {
                    _dataProvider.PlanFound(goals, _plan);
                    fsm.PopState();
                    fsm.PushState(_performState);
                }
            };
        }


        private void CreatePerformState()
        {
            _performState = (fsm, fsmGameObject) =>
            {
                // all actions done, plan over...
                if (_plan.Count == 0)
                {
                    _dataProvider.PlanFinished();
                    fsm.PopState();
                    fsm.PushState(_idleState);
                    return;
                }

                // if action done (in other words, has exited)
                var action = _plan.Peek();
                if (action.IsDone())
                {
                    _plan.Pop();
                    return;
                }

                // action go!
                if (!action.Go(this))
                {
                    _dataProvider.PlanAborted(action);
                    fsm.PopState();
                    fsm.PushState(_idleState);
                }

            };
        }


        private void ResetActionData()
        {
            _actionData.Clear();
        }



        // find derivation of IGoap
        private void FindDataProvider()
        {
            _dataProvider = GetComponent<IGoap>();
        }


        // load all derivation of GoapAction
        private void LoadActions()
        {
            _actions = GetComponents<GoapAction>();
        }


        // get pretty plans string
        public static string PrettyPlan(Stack<GoapAction> plan)
        {
            string result = "";
            foreach (var action in plan)
            {
                if (result != "")
                {
                    result += " -> ";
                }
                result += action.GetType().Name;
            }
            return result;
        }


        // get pretty goals string
        public static string PrettyGoals(HashSet<KeyValuePair<string, object>> goals)
        {
            string result = "";
            foreach (var goal in goals)
            {
                if (result != "")
                {
                    result += ", ";
                }
                result += "(" + goal.Key + ", " + goal.Value + ")";
            }
            return result;
        }


        // add item to data context
        public void AddActionData(string key, object obj)
        {
            _actionData.Add(new KeyValuePair<string, object>(key, obj));
        }


        // search data context
        public object SearchActionData(string key)
        {
            foreach (var data in _actionData)
            {
                if (data.Key.Equals(key))
                {
                    return data.Value;
                }
            }
            return null;
        }

    }
}