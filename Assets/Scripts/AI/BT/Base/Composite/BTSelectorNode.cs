using System;
using System.Collections.Generic;

public class BTSelectorNode : BTCompositeNode
{

    /// <summary>
    /// 选择节点
    /// 当节点返回success 则返回success
    /// 当节点返回failure 则执行下一个节点 返回running
    /// 否则返回running
    /// </summary>
    /// <returns></returns>
    public override BTResult onTick(WorkingData wd)
    {
        BTResult status = this.children[this.currIndex].onTick(wd);
        if (status == BTResult.success)
        {
            clear();
            return BTResult.success;
        }
        if (status == BTResult.failure)
        {
            this.currIndex++;
            if (this.currIndex >= this.children.Count)
            {
                clear();
                return BTResult.success;
            }
            status = BTResult.running;
        }
        return status;
    }
}
