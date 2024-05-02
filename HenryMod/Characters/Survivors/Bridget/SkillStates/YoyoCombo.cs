using HenryMod.Modules.BaseStates;
using RoR2;
using UnityEngine;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class YoyoCombo : BaseMeleeAttack
    {
        public override void OnEnter()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageType.Generic;
            damageCoefficient = BridgetStaticValues.swordDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 300f;
            bonusForce = Vector3.zero;
            baseDuration = 1f;

            //0-1 multiplier of baseduration, used to time when the hitbox is out (usually based on the run time of the animation)
            //for example, if attackStartPercentTime is 0.5, the attack will start hitting halfway through the ability. if baseduration is 3 seconds, the attack will start happening at 1.5 seconds
            attackStartPercentTime = 0.2f;
            attackEndPercentTime = 0.4f;

            //this is the point at which the attack can be interrupted by itself, continuing a combo
            earlyExitPercentTime = 0.6f;

            hitStopDuration = 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            swingSoundString = "HenrySwordSwing";
            hitSoundString = "";

            if (swingIndex % 3 == 0) muzzleString = "SwingLeft";
            if (swingIndex % 3 == 1) muzzleString = "SwingRight";
            if (swingIndex % 3 == 2) muzzleString = "SwingRight";


            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = BridgetAssets.swordSwingEffect;
            hitEffectPrefab = BridgetAssets.swordHitImpactEffect;

            impactSound = BridgetAssets.swordHitSoundEvent.index;

            base.OnEnter();
        }

        protected override void PlayAttackAnimation()
        {
            PlayCrossfade("Gesture, Override", "Slash" + (1 + swingIndex), playbackRateParam, duration, 0.1f * duration);
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}