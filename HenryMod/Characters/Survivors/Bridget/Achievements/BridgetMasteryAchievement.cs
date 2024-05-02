﻿using RoR2;
using HenryMod.Modules.Achievements;

namespace HenryMod.Survivors.Henry.Achievements
{
    //automatically creates language tokens "ACHIEVMENT_{identifier.ToUpper()}_NAME" and "ACHIEVMENT_{identifier.ToUpper()}_DESCRIPTION" 
    [RegisterAchievement(identifier, unlockableIdentifier, null, null)]
    public class BridgetMasteryAchievement : BaseMasteryAchievement
    {
        public const string identifier = BridgetSurvivor.HENRY_PREFIX + "masteryAchievement";
        public const string unlockableIdentifier = BridgetSurvivor.HENRY_PREFIX + "masteryUnlockable";

        public override string RequiredCharacterBody => BridgetSurvivor.instance.bodyName;

        //difficulty coeff 3 is monsoon. 3.5 is typhoon for grandmastery skins
        public override float RequiredDifficultyCoefficient => 3;
    }
}