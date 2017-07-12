using System;
using System.Collections;
using System.Collections.Generic;

public class BTCompositeNode : BTNode
{
    public List<BTNode> children = new List<BTNode>();
    public int currIndex = 0;


    public override void addChild(BTNode node)
    {
        children.Add(node);
    }
    public override void removeChild(BTNode node)
    {
        if (children.Contains(node))
        {
            children.Remove(node);
        }
    }

    public override void clear()
    {
        currIndex = 0;
    }

}