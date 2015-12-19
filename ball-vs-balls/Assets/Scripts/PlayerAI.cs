using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;

public class PlayerAI : Player
{
    public override void Initialize2(
        int inId,
        GameObject inSceneManager)
    {
        base.Initialize2(inId, inSceneManager);
        isController = ePlayerType._playerTypeAI;

        Assert.IsTrue(mSceneManager != null);
    }

    public override void Update2()
    {
        base.Update2();
    }

    void Start()
    {
        
	}
	
	void Update()
    {
	
	}
}
