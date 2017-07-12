using UnityEngine;
using System.Collections;

public abstract class BTTree
{

    public BTNode root;

    public void onTick(WorkingData  wd)
    {
        if (root != null)
            root.onTick(wd);
    }

    public abstract void onBuildTree();

}
