using UnityEngine;
using System.Collections;
using UnityEngine.Assertions;
using System;

public class PlayerAI : Player
{
    protected override void PostSpawnSpotLight()
    {
        mLightComponent.color = Color.blue;
    }

    public override void Initialize2(
        int inId,
        GameObject inSceneManager)
    {
        base.Initialize2(inId, inSceneManager);
        isController = ePlayerType._playerTypeAI;

        Assert.IsTrue(mSceneManager != null);

        PostSpawnSpotLight();
    }

    public override void Update2()
    {
        base.Update2();
    }

    public override void SyncUpdate2()
    {
        base.SyncUpdate2();
    }

    void Start()
    {
        
	}
	
	void Update()
    {
	
	}
}
