﻿using UnityEngine;
using System.Collections;

public class PlayerAI : Player
{
    public override void Initialize2(int myId, GameObject sceneManager)
    {
        isController = ePlayerType._playerTypeAI;
        base.Initialize2(myId, sceneManager);
    }

    void Start()
    {
        
	}
	
	void Update()
    {
	
	}
}