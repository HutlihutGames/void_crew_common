using System.Collections.Generic;

#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
#endif

namespace VC.Common.CoreData
{
public class ModStatID
    {
        public int Id;
        public string Name;
        public ModStatID() { }

        public ModStatID(int id, string name)
        {
            Id = id;
            Name = name;
        }

#if UNITY_EDITOR
        public static List<ModStatID> GetAllStatTypes()
        {
            return typeof(ModStatID)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(ModStatID))
                .Select(f => (ModStatID)f.GetValue(null)!)
                .ToList();
        }
#endif

        private const int INT_FLAG = 0x_1_00_00_000;

        private const int DEFENSE_CATEGORY_FLAG = 0x_0_01_00_000;
        private const int MOVE_CATEGORY_FLAG = 0x_0_02_00_000;
        private const int POWER_CATEGORY_FLAG = 0x_0_03_00_000;
        private const int UTILITY_CATEGORY_FLAG = 0x_0_04_00_000;
        private const int EVA_CATEGORY_FLAG = 0x_0_05_00_000;
        private const int CHARACTERSTATS_CATEGORY_FLAG = 0x_0_06_00_000;

        public static bool IsInt(int id)
        {
            return (id & INT_FLAG) > 0;
        }

        //ID GUIDE
        // Hex | Type  |  Category | Sub-category | Local index
        // 0x  |   0   |     00    |      00      |     000   

        //Types:
        // 0 - Float
        // 1 - Int

        // Categories:                         | Sub categories:
        // Weapons and projectiles - 00        | Primary (used on all weapons), Secondary (used on per weapon basis)
        // Defense - 01                        | Primary (used on all defensive mechanisms, Secondary (used on per object basis)
        // Movement - 02                       | Primary (used on all moving mechanisms, Secondary (used on per object basis)
        // Power - 03                          | 00 for basic power properties, 01 for battery properties
        // Utility - 04                        | 00 for mission progress, 01 for clonetube related things, 02 for action-modules (jammer/scanner/webifier), 03 for attractor/gravity-scoop, 04 for life-support related things
        // EVA - 05                            | 00 for general jetpack stats, 01 for jetpack dash stats
        // CharacterStats - 06                 | 00 for general stats, 01 for passive healing related stats

        // Weapon category
        public static readonly ModStatID Damage = new ModStatID(0x_0_00_00_001, nameof(Damage));
        public static readonly ModStatID FireRate = new ModStatID(0x_0_00_00_002, nameof(FireRate));
        public static readonly ModStatID Range = new ModStatID(0x_0_00_00_003, nameof(Range));
        public static readonly ModStatID ProjectileSpeed = new ModStatID(0x_0_00_00_004, nameof(ProjectileSpeed));
        public static readonly ModStatID Accuracy = new ModStatID(0x_0_00_00_005, nameof(Accuracy));
        public static readonly ModStatID RotationSpeed = new ModStatID(0x_0_00_00_006, nameof(RotationSpeed));

        public static readonly ModStatID DamageSecondary = new ModStatID(0x_0_00_00_101, nameof(DamageSecondary));

        public static readonly ModStatID MaxZoom = new ModStatID(0x_0_00_01_001, nameof(MaxZoom));
        public static readonly ModStatID ReloadTime = new ModStatID(0x_0_00_01_002, nameof(ReloadTime));
        public static readonly ModStatID MagazineReservoirTick = new ModStatID(0x_0_00_01_003, nameof(MagazineReservoirTick));
        public static readonly ModStatID ActiveReloadThreshold = new ModStatID(0x_0_00_01_004, nameof(ActiveReloadThreshold));
        public static readonly ModStatID HeatPerShot = new ModStatID(0x_0_00_01_005, nameof(HeatPerShot));
        public static readonly ModStatID HeatDissipationPerSec = new ModStatID(0x_0_00_01_006, nameof(HeatDissipationPerSec));
        public static readonly ModStatID AmmoConsumptionEfficiency = new ModStatID(0x_0_00_01_007, nameof(AmmoConsumptionEfficiency));

        // Weapon pips
        public static readonly ModStatID DamagePip = new ModStatID(0x_1_00_10_001, nameof(DamagePip));
        public static readonly ModStatID FireRatePip = new ModStatID(0x_1_00_10_002, nameof(FireRatePip));
        public static readonly ModStatID RangePip = new ModStatID(0x_1_00_10_003, nameof(RangePip));
        public static readonly ModStatID ProjectileSpeedPip = new ModStatID(0x_1_00_10_004, nameof(ProjectileSpeedPip));
        public static readonly ModStatID AccuracyPip = new ModStatID(0x_1_00_10_005, nameof(AccuracyPip));
        public static readonly ModStatID RotationSpeedPip = new ModStatID(0x_1_00_10_006, nameof(RotationSpeedPip));
        
        public static readonly ModStatID MaxZoomPip = new ModStatID(0x_1_00_11_001, nameof(MaxZoomPip));
        public static readonly ModStatID ReloadTimePip = new ModStatID(0x_1_00_11_002, nameof(ReloadTimePip));
        public static readonly ModStatID MagazineReservoirTickPip = new ModStatID(0x_1_00_11_003, nameof(MagazineReservoirTickPip));
        public static readonly ModStatID ActiveReloadThresholdPip = new ModStatID(0x_1_00_11_004, nameof(ActiveReloadThresholdPip));
        public static readonly ModStatID HeatPerShotPip = new ModStatID(0x_1_00_11_005, nameof(HeatPerShotPip));
        public static readonly ModStatID HeatDissipationPerSecPip = new ModStatID(0x_1_00_11_006, nameof(HeatDissipationPerSecPip));

        // Defense category
        public static readonly ModStatID KpdTrackingRange = new ModStatID(0x_0_01_01_001, nameof(KpdTrackingRange));
        public static readonly ModStatID KpdCooldownAfterBurst = new ModStatID(0x_0_01_01_002, nameof(KpdCooldownAfterBurst));
        public static readonly ModStatID ShieldMaxHitPoints = new ModStatID(0x_0_01_02_001, nameof(ShieldMaxHitPoints));
        public static readonly ModStatID ShieldRechargeSpeed = new ModStatID(0x_0_01_02_002, nameof(ShieldRechargeSpeed));
        public static readonly ModStatID ShieldRechargeDelay = new ModStatID(0x_0_01_02_003, nameof(ShieldRechargeDelay));
        public static readonly ModStatID ShieldAbsorption = new ModStatID(0x_0_01_02_004, nameof(ShieldAbsorption));
        public static readonly ModStatID ShieldGenerationEnabled = new ModStatID(0x_1_01_02_005, nameof(ShieldGenerationEnabled));

        public static readonly ModStatID Invulnerability = new ModStatID(0x_1_01_03_000, nameof(Invulnerability));

        public static readonly ModStatID AsphyxiationVulnerability = new ModStatID(0x_0_01_03_001, nameof(AsphyxiationVulnerability));
        public static readonly ModStatID KineticVulnerability = new ModStatID(0x_0_01_03_002, nameof(KineticVulnerability));
        public static readonly ModStatID ElectricVulnerability = new ModStatID(0x_0_01_03_003, nameof(ElectricVulnerability));
        public static readonly ModStatID EnergyVulnerability = new ModStatID(0x_0_01_03_004, nameof(EnergyVulnerability));
        public static readonly ModStatID FireVulnerability = new ModStatID(0x_0_01_03_005, nameof(FireVulnerability));
        public static readonly ModStatID FreezingVulnerability = new ModStatID(0x_0_01_03_006, nameof(FreezingVulnerability));
        public static readonly ModStatID PhysicalVulnerability = new ModStatID(0x_0_01_03_007, nameof(PhysicalVulnerability));
        public static readonly ModStatID RadiationVulnerability = new ModStatID(0x_0_01_03_008, nameof(RadiationVulnerability));
        public static readonly ModStatID VoidVulnerability = new ModStatID(0x_0_01_03_009, nameof(VoidVulnerability));
        public static readonly ModStatID Vulnerability = new ModStatID(0x_0_01_03_010, nameof(Vulnerability));
        public static readonly ModStatID MaxHitPoints = new ModStatID(0x_0_01_04_001, nameof(MaxHitPoints));

        // Movement category
        public static readonly ModStatID ForwardPower = new ModStatID(0x_0_02_00_001, nameof(ForwardPower));
        public static readonly ModStatID YawTorque = new ModStatID(0x_0_02_00_002, nameof(YawTorque));
        public static readonly ModStatID ElevationPower = new ModStatID(0x_0_02_00_003, nameof(ElevationPower));
        public static readonly ModStatID StrafePower = new ModStatID(0x_0_02_00_004, nameof(StrafePower));
        public static readonly ModStatID EnginePower = new ModStatID(0x_0_02_00_005, nameof(EnginePower));
        public static readonly ModStatID PilotAidLevel = new ModStatID(0x_0_02_00_006, nameof(PilotAidLevel));

        public static readonly ModStatID JumpChargeSpeed = new ModStatID(0x_0_02_01_001, nameof(JumpChargeSpeed));
        public static readonly ModStatID VoidJumpCapable = new ModStatID(0x_1_02_01_002, nameof(VoidJumpCapable));
        
        public static readonly ModStatID SignatureVelocity = new ModStatID(0x_0_02_02_001, nameof(SignatureVelocity));
        public static readonly ModStatID SignatureAngularVelocity = new ModStatID(0x_0_02_02_002, nameof(SignatureAngularVelocity));
        
        public static readonly ModStatID ThrusterBoosterDuration = new ModStatID(0x_0_02_03_001, nameof(ThrusterBoosterDuration));
        public static readonly ModStatID ThrusterBoosterCooldown = new ModStatID(0x_0_02_03_002, nameof(ThrusterBoosterCooldown));
        public static readonly ModStatID ThrusterBoosterRechargeSpeed = new ModStatID(0x_0_02_03_003, nameof(ThrusterBoosterRechargeSpeed));

        // Movement pips
        public static readonly ModStatID ForwardPowerPip = new ModStatID(0x_1_02_10_001, nameof(ForwardPowerPip));
        public static readonly ModStatID YawTorquePip = new ModStatID(0x_1_02_10_002, nameof(YawTorquePip));
        public static readonly ModStatID ElevationPowerPip = new ModStatID(0x_1_02_10_003, nameof(ElevationPowerPip));
        public static readonly ModStatID StrafePowerPip = new ModStatID(0x_1_02_10_004, nameof(StrafePowerPip));
        public static readonly ModStatID EnginePowerPip = new ModStatID(0x_1_02_10_005, nameof(EnginePowerPip));
        public static readonly ModStatID PilotAidLevelPip = new ModStatID(0x_1_02_10_006, nameof(PilotAidLevelPip));

        public static readonly ModStatID JumpChargeSpeedPip = new ModStatID(0x_1_02_11_001, nameof(JumpChargeSpeedPip));
        
        public static readonly ModStatID SignatureVelocityPip = new ModStatID(0x_1_02_12_001, nameof(SignatureVelocityPip));
        public static readonly ModStatID SignatureAngularVelocityPip = new ModStatID(0x_1_02_12_002, nameof(SignatureAngularVelocityPip));
        
        public static readonly ModStatID ThrusterBoosterDurationPip = new ModStatID(0x_1_02_13_001, nameof(ThrusterBoosterDurationPip));

        // Power category
        public static readonly ModStatID PowerWanted = new ModStatID(0x_1_03_00_001, nameof(PowerWanted));
        public static readonly ModStatID PowerProvided = new ModStatID(0x_1_03_00_002, nameof(PowerProvided));

        public static readonly ModStatID BatteryRechargeAmount = new ModStatID(0x_0_03_01_001, nameof(BatteryRechargeAmount));
        
        public static readonly ModStatID BreakerTemperatureShift = new ModStatID(0x_0_03_02_001, nameof(BreakerTemperatureShift));

        // Utility category
        public static readonly ModStatID ProcessingSpeed = new ModStatID(0x_0_04_00_001, nameof(ProcessingSpeed));

        public static readonly ModStatID HealingSpeed = new ModStatID(0x_0_04_01_001, nameof(HealingSpeed));

        public static readonly ModStatID ActionCooldown = new ModStatID(0x_0_04_02_001, nameof(ActionCooldown));
        public static readonly ModStatID EffectRadius = new ModStatID(0x_0_04_02_002, nameof(EffectRadius));

        public static readonly ModStatID AttractorMaxRange = new ModStatID(0x_0_04_03_001, nameof(AttractorMaxRange));
        public static readonly ModStatID AttractorPullVelocity = new ModStatID(0x_0_04_03_002, nameof(AttractorPullVelocity));

        public static readonly ModStatID LifeSupportEffectivity = new ModStatID(0x_0_04_04_001, nameof(LifeSupportEffectivity));
        public static readonly ModStatID PassiveTemperatureShift = new ModStatID(0x_0_04_04_002, nameof(PassiveTemperatureShift));

        // EVA category
        public static readonly ModStatID JetpackThrust = new ModStatID(0x_0_05_00_001, nameof(JetpackThrust));
        public static readonly ModStatID JetpackOxygenCapacity = new ModStatID(0x_0_05_00_002, nameof(JetpackOxygenCapacity));

        public static readonly ModStatID JetpackDashOxygen = new ModStatID(0x_0_05_01_001, nameof(JetpackDashOxygen));
        public static readonly ModStatID JetpackDashThrust = new ModStatID(0x_0_05_01_002, nameof(JetpackDashThrust));
        public static readonly ModStatID JetpackDashDuration = new ModStatID(0x_0_05_01_003, nameof(JetpackDashDuration));
        public static readonly ModStatID JetpackDashCooldown = new ModStatID(0x_0_05_01_004, nameof(JetpackDashCooldown));

        public static readonly ModStatID JetpackEnhanced = new ModStatID(0x_1_05_02_001, nameof(JetpackEnhanced));

        // Ectype stats category
        public static readonly ModStatID PassiveHealingAmount = new ModStatID(0x_0_06_00_002, nameof(PassiveHealingAmount));
        public static readonly ModStatID PassiveHealingInterval = new ModStatID(0x_0_06_00_003, nameof(PassiveHealingInterval));
        public static readonly ModStatID PassiveHealingCap = new ModStatID(0x_0_06_00_004, nameof(PassiveHealingCap));

        public static readonly ModStatID PushForceCounter = new ModStatID(0x_0_06_01_001, nameof(PushForceCounter));

        public static readonly ModStatID InstantCrypticWinChance = new ModStatID(0x_0_06_02_001, nameof(InstantCrypticWinChance));
        public static readonly ModStatID CrypticSkill = new ModStatID(0x_0_06_02_002, nameof(CrypticSkill));
        public static readonly ModStatID BreachRepairSkill = new ModStatID(0x_0_06_02_003, nameof(BreachRepairSkill));

        public static readonly ModStatID EnhancementProficiency = new ModStatID(0x_0_06_03_001, nameof(EnhancementProficiency));
        public static readonly ModStatID EnhancementDurationMultiplier = new ModStatID(0x_0_06_03_002, nameof(EnhancementDurationMultiplier));
        public static readonly ModStatID EnhancementRepairSkill = new ModStatID(0x_1_06_03_003, nameof(EnhancementRepairSkill));

        public static readonly ModStatID ThrowForce = new ModStatID(0x_0_06_04_001, nameof(ThrowForce));

        public static readonly ModStatID ClassAbilityDuration = new ModStatID(0x_0_06_05_001, nameof(ClassAbilityDuration));
        public static readonly ModStatID ClassAbilityCooldown = new ModStatID(0x_0_06_05_002, nameof(ClassAbilityCooldown));
        public static readonly ModStatID GrapplingHookRange = new ModStatID(0x_0_06_05_003, nameof(GrapplingHookRange));

        public static readonly ModStatID TargetLockDamage = new ModStatID(0x_0_06_06_001, nameof(TargetLockDamage));
        public static readonly ModStatID TargetLockCount = new ModStatID(0x_1_06_06_002, nameof(TargetLockCount));
        public static readonly ModStatID ScanRange = new ModStatID(0x_0_06_06_003, nameof(ScanRange));
        public static readonly ModStatID ScanSpeedIncreased = new ModStatID(0x_1_06_06_004, nameof(ScanSpeedIncreased));
    }
}