using System;
using System.Collections.Generic;

public class MonsterFSM : FSM
{
    public MonsterFSM(BaseEntity agent) : base(agent)
    {
    }

    public override void init()
    {
        //this.dictStates.Add(StateType.idle, new MonsterIdleState(this.agent));
        //this.dictStates.Add(StateType.run, new MonsterRunState(this.agent));
        //this.dictStates.Add(StateType.attack, new MonsterAttackState(this.agent));
        //this.dictStates.Add(StateType.die, new MonsterDieState(this.agent));
        //this.dictStates.Add(StateType.beHit, new MonsterBeHitState(this.agent));
        //this.dictStates.Add(StateType.spawn, new MonsterSpawnState(this.agent));

        this.dictStates.Add(StateType.idle, new MonsterIdleState(this.agent, StateType.idle));
        this.dictStates.Add(StateType.skill, new PlayerSkillState(this.agent, StateType.skill));
        this.dictStates.Add(StateType.run, new PlayerRunState(this.agent, StateType.run));
        this.dictStates.Add(StateType.spawn, new PlayerSpawnState(this.agent, StateType.spawn));
        this.dictStates.Add(StateType.onHit, new PlayerOnHitState(this.agent, StateType.onHit));
        this.dictStates.Add(StateType.die, new PlayerDieState(this.agent, StateType.die));
    }
}

