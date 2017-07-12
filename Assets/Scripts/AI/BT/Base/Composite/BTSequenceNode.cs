using System;
using System.Collections;

public class BTSequenceNode : BTCompositeNode
{

    /// <summary>
    /// 顺序节点
    /// 当节点返回success 则执行下一个节点 返回running
    /// 当节点返回failure 则直接返回success
    /// 否则返回running
    /// </summary>
    /// <returns></returns>
    public override BTResult onTick(WorkingData wd)
    {
        BTResult status = this.children[currIndex].onTick(wd);
        if (status == BTResult.success)
        {
            this.currIndex++;
            if (this.currIndex >= this.children.Count)
            {
                clear();
                return BTResult.success;
            }
            status = BTResult.running;
        }
        if (status == BTResult.failure)
        {
            clear();
            return BTResult.success;
        }
        return status;
    }

}
