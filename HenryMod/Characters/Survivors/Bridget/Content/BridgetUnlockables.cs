using HenryMod.Survivors.Henry.Achievements;
using RoR2;
using UnityEngine;

namespace HenryMod.Survivors.Henry
{
    public static class BridgetUnlockables
    {
        public static UnlockableDef characterUnlockableDef = null;
        public static UnlockableDef masterySkinUnlockableDef = null;

        public static void Init()
        {
            masterySkinUnlockableDef = Modules.Content.CreateAndAddUnlockbleDef(
                BridgetMasteryAchievement.unlockableIdentifier,
                Modules.Tokens.GetAchievementNameToken(BridgetMasteryAchievement.identifier),
                BridgetSurvivor.instance.assetBundle.LoadAsset<Sprite>("texMasteryAchievement"));
        }
    }
}
