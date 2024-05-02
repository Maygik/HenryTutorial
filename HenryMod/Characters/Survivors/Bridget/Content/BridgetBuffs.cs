using RoR2;
using UnityEngine;

namespace HenryMod.Survivors.Henry
{
    public static class BridgetBuffs
    {
        // armor buff gained during roll
        public static BuffDef armorBuff;

        public static void Init(AssetBundle assetBundle)
        {
            armorBuff = Modules.Content.CreateAndAddBuff("BridgetArmorBuff",
                LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HiddenInvincibility").iconSprite,
                Color.white,
                false,
                false);

        }
    }
}
