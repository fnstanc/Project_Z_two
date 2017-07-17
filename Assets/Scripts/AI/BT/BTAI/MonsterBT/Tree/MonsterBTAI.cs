using UnityEngine;
using System.Collections;
using System;

public class MonsterBTAI : BTTree
{

    public MonsterBTAI()
    {
        onBuildTree();
    }

    public override void onBuildTree()
    {
        this.root = new BTSequenceNode();
        BTSelectorNode select_1 = new BTSelectorNode();


        MonsterPartolCond parCond = new MonsterPartolCond();
        MonsterPartolAct patrolAct = new MonsterPartolAct();
//        select_1.addChild(parCond);
        select_1.addChild(patrolAct);


        //MonsterTagetCond targetCond = new MonsterTagetCond();
        //MonsterSerchAct serchAct = new MonsterSerchAct();
        //select_1.addChild(targetCond);
        //select_1.addChild(serchAct);

        //MonsterDistanceCond distanceCond = new MonsterDistanceCond();
        //MonsterMoveToAct moveToAct = new MonsterMoveToAct();
        //select_2.addChild(distanceCond);
        //select_2.addChild(moveToAct);

        //MonsterAttackAct attackAct = new MonsterAttackAct();
        //MonsterSkillCond skillCond = new MonsterSkillCond();
        //select_3.addChild(skillCond);
        //select_3.addChild(attackAct);

        this.root.addChild(select_1);
    }
}
