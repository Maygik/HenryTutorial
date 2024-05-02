using EntityStates;
using HenryMod.Survivors.Henry;
using IL.RoR2.Achievements;
using RoR2;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace HenryMod.Survivors.Henry.SkillStates
{
    public class Roller : BaseSkillState
    {

        public static float initialSpeedCoefficient = 1.5f;
        public static float maxSpeedCoefficient = 5f;

        public static float windUpThreshold = 0.2f;
        public static float windDownThreshold = 0.5f;
        public static float duration = 1.25f;


        public static string dodgeSoundString = "HenryRoll";
        public static float dodgeFOV = global::EntityStates.Commando.DodgeState.dodgeFOV;

        private float rollSpeed;
        private Vector3 forwardDirection;
        private Animator animator;
        private Vector3 previousPosition;


        enum ERollingState
        {
            NotRolling,
            WindingUp,
            Rolling,
            WindingDown,
        }
        ERollingState rollingState = ERollingState.NotRolling;




        public override void OnEnter()
        {
            base.OnEnter();
            animator = GetModelAnimator();

            if (isAuthority && inputBank && characterDirection)
            {
                forwardDirection = (inputBank.moveVector == Vector3.zero ? characterDirection.forward : inputBank.moveVector).normalized;
            }

            Vector3 rhs = characterDirection ? characterDirection.forward : forwardDirection;
            Vector3 rhs2 = Vector3.Cross(Vector3.up, rhs);

            float num = Vector3.Dot(forwardDirection, rhs);
            float num2 = Vector3.Dot(forwardDirection, rhs2);

            rollingState = ERollingState.WindingUp;

            RecalculateRollSpeed();

            if (characterMotor && characterDirection)
            {
                //characterMotor.velocity.y = 0f;
                characterMotor.velocity = forwardDirection * rollSpeed;
            }

            Vector3 b = characterMotor ? characterMotor.velocity : Vector3.zero;
            previousPosition = transform.position - b;

            PlayAnimation("FullBody, Override", "Roll", "Roll.playbackRate", duration);
            Util.PlaySound(dodgeSoundString, gameObject);

            if (NetworkServer.active)
            {
                characterBody.AddTimedBuff(BridgetBuffs.armorBuff, 3f * duration);
                characterBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.5f * duration);
            }
        }

        private void RecalculateRollSpeed()
        {
            switch(rollingState)
            {
                case ERollingState.WindingUp:
                    rollSpeed = moveSpeedStat * Mathf.Lerp(initialSpeedCoefficient, maxSpeedCoefficient, fixedAge / (duration * windUpThreshold));
                    break;
                case ERollingState.Rolling:
                    rollSpeed = moveSpeedStat * maxSpeedCoefficient;
                    break;
                case ERollingState.WindingDown:
                    rollSpeed = moveSpeedStat * Mathf.Lerp(initialSpeedCoefficient, maxSpeedCoefficient, (fixedAge - (duration * windDownThreshold)) / (duration - (duration * windDownThreshold)));
                    break;
            }                        
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            RecalculateRollSpeed();

            if (characterDirection) characterDirection.forward = forwardDirection;
            if (cameraTargetParams) cameraTargetParams.fovOverride = Mathf.Lerp(dodgeFOV, 60f, fixedAge / duration);

            Vector3 normalized = (transform.position - previousPosition).normalized;
            if (characterMotor && characterDirection && normalized != Vector3.zero)
            {
                Vector3 vector = normalized * rollSpeed;
                float d = Mathf.Max(Vector3.Dot(vector, forwardDirection), 0f);
                vector = forwardDirection * d;
                vector.y = characterMotor.velocity.y;

                characterMotor.velocity = vector;
            }
            previousPosition = transform.position;

            if (isAuthority && fixedAge >= duration)
            {
                outer.SetNextStateToMain();
                return;
            }
        }

        public override void OnExit()
        {
            if (cameraTargetParams) cameraTargetParams.fovOverride = -1f;
            base.OnExit();

            characterMotor.disableAirControlUntilCollision = false;
        }

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(forwardDirection);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            forwardDirection = reader.ReadVector3();
        }
    }
}