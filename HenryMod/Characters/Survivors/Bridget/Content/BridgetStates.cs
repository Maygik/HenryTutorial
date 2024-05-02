﻿using HenryMod.Survivors.Henry.SkillStates;

namespace HenryMod.Survivors.Henry
{
    public static class BridgetStates
    {
        public static void Init()
        {
            Modules.Content.AddEntityState(typeof(YoyoCombo));

            Modules.Content.AddEntityState(typeof(Shoot));

            Modules.Content.AddEntityState(typeof(Roll));

            Modules.Content.AddEntityState(typeof(ThrowBomb));
        }
    }
}