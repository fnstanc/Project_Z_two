using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PlayerFSM : FSM
{
    public PlayerFSM(BaseEntity agent) : base(agent)
    {
    }

    public override void init()
    {
        this.dictStates.Add(StateType.idle, new PlayerIdleState(this.agent, StateType.idle));
        this.dictStates.Add(StateType.skill, new PlayerSkillState(this.agent, StateType.skill));
        this.dictStates.Add(StateType.run, new PlayerRunState(this.agent, StateType.run));
        this.dictStates.Add(StateType.spawn, new PlayerSpawnState(this.agent, StateType.spawn));
        this.dictStates.Add(StateType.onHit, new PlayerOnHitState(this.agent, StateType.onHit));
        this.dictStates.Add(StateType.yasuoRSkill, new PlayerYaSuoRState(this.agent, StateType.yasuoRSkill));
        this.dictStates.Add(StateType.dodge, new PlayerDodgeState(this.agent, StateType.dodge));
        this.dictStates.Add(StateType.die, new PlayerDieState(this.agent, StateType.die));
        this.dictStates.Add(StateType.baseCombo, new PlayerBaseComboState(this.agent, StateType.baseCombo));
    }
}
