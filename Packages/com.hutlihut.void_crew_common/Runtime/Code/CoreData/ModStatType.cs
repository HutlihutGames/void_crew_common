using System.Collections.Generic;

#if UNITY_EDITOR
using System.Linq;
using System.Reflection;
#endif

namespace VC.Common.CoreData
{
    public class ModStatType
    {
        public int Id;
        public string Name;

        public ModStatType() { }

        public ModStatType(int id, string name)
        {
            Id = id;
            Name = name;
        }

#if UNITY_EDITOR
        public static List<ModStatType> GetAllStatTypes()
        {
            return typeof(ModStatType)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => f.FieldType == typeof(ModStatType))
                .Select(f => (ModStatType)f.GetValue(null)!)
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
        public static readonly ModStatType Damage = new ModStatType(0x_0_00_00_001, nameof(Damage));
        public static readonly ModStatType FireRate = new ModStatType(0x_0_00_00_002, nameof(FireRate));
        public static readonly ModStatType Range = new ModStatType(0x_0_00_00_003, nameof(Range));
        public static readonly ModStatType ProjectileSpeed = new ModStatType(0x_0_00_00_004, nameof(ProjectileSpeed));
        public static readonly ModStatType Accuracy = new ModStatType(0x_0_00_00_005, nameof(Accuracy));
        public static readonly ModStatType RotationSpeed = new ModStatType(0x_0_00_00_006, nameof(RotationSpeed));

        public static readonly ModStatType DamageSecondary = new ModStatType(0x_0_00_00_101, nameof(DamageSecondary));

        public static readonly ModStatType MaxZoom = new ModStatType(0x_0_00_01_001, nameof(MaxZoom));
        public static readonly ModStatType ReloadTime = new ModStatType(0x_0_00_01_002, nameof(ReloadTime));
        public static readonly ModStatType MagazineReservoirTick = new ModStatType(0x_0_00_01_003, nameof(MagazineReservoirTick));
        public static readonly ModStatType ActiveReloadThreshold = new ModStatType(0x_0_00_01_004, nameof(ActiveReloadThreshold));
        public static readonly ModStatType HeatPerShot = new ModStatType(0x_0_00_01_005, nameof(HeatPerShot));
        public static readonly ModStatType HeatDissipationPerSec = new ModStatType(0x_0_00_01_006, nameof(HeatDissipationPerSec));
        public static readonly ModStatType AmmoConsumptionEfficiency = new ModStatType(0x_0_00_01_007, nameof(AmmoConsumptionEfficiency));

        // Weapon pips
        public static readonly ModStatType DamagePip = new ModStatType(0x_1_00_10_001, nameof(DamagePip));
        public static readonly ModStatType FireRatePip = new ModStatType(0x_1_00_10_002, nameof(FireRatePip));
        public static readonly ModStatType RangePip = new ModStatType(0x_1_00_10_003, nameof(RangePip));
        public static readonly ModStatType ProjectileSpeedPip = new ModStatType(0x_1_00_10_004, nameof(ProjectileSpeedPip));
        public static readonly ModStatType AccuracyPip = new ModStatType(0x_1_00_10_005, nameof(AccuracyPip));
        public static readonly ModStatType RotationSpeedPip = new ModStatType(0x_1_00_10_006, nameof(RotationSpeedPip));
        
        public static readonly ModStatType MaxZoomPip = new ModStatType(0x_1_00_11_001, nameof(MaxZoomPip));
        public static readonly ModStatType ReloadTimePip = new ModStatType(0x_1_00_11_002, nameof(ReloadTimePip));
        public static readonly ModStatType MagazineReservoirTickPip = new ModStatType(0x_1_00_11_003, nameof(MagazineReservoirTickPip));
        public static readonly ModStatType ActiveReloadThresholdPip = new ModStatType(0x_1_00_11_004, nameof(ActiveReloadThresholdPip));
        public static readonly ModStatType HeatPerShotPip = new ModStatType(0x_1_00_11_005, nameof(HeatPerShotPip));
        public static readonly ModStatType HeatDissipationPerSecPip = new ModStatType(0x_1_00_11_006, nameof(HeatDissipationPerSecPip));

        // Defense category
        public static readonly ModStatType KpdTrackingRange = new ModStatType(0x_0_01_01_001, nameof(KpdTrackingRange));
        public static readonly ModStatType KpdCooldownAfterBurst = new ModStatType(0x_0_01_01_002, nameof(KpdCooldownAfterBurst));
        public static readonly ModStatType ShieldMaxHitPoints = new ModStatType(0x_0_01_02_001, nameof(ShieldMaxHitPoints));
        public static readonly ModStatType ShieldRechargeSpeed = new ModStatType(0x_0_01_02_002, nameof(ShieldRechargeSpeed));
        public static readonly ModStatType ShieldRechargeDelay = new ModStatType(0x_0_01_02_003, nameof(ShieldRechargeDelay));
        public static readonly ModStatType ShieldAbsorption = new ModStatType(0x_0_01_02_004, nameof(ShieldAbsorption));
        public static readonly ModStatType ShieldGenerationEnabled = new ModStatType(0x_1_01_02_005, nameof(ShieldGenerationEnabled));

        public static readonly ModStatType Invulnerability = new ModStatType(0x_1_01_03_000, nameof(Invulnerability));

        public static readonly ModStatType AsphyxiationVulnerability = new ModStatType(0x_0_01_03_001, nameof(AsphyxiationVulnerability));
        public static readonly ModStatType KineticVulnerability = new ModStatType(0x_0_01_03_002, nameof(KineticVulnerability));
        public static readonly ModStatType ElectricVulnerability = new ModStatType(0x_0_01_03_003, nameof(ElectricVulnerability));
        public static readonly ModStatType EnergyVulnerability = new ModStatType(0x_0_01_03_004, nameof(EnergyVulnerability));
        public static readonly ModStatType FireVulnerability = new ModStatType(0x_0_01_03_005, nameof(FireVulnerability));
        public static readonly ModStatType FreezingVulnerability = new ModStatType(0x_0_01_03_006, nameof(FreezingVulnerability));
        public static readonly ModStatType PhysicalVulnerability = new ModStatType(0x_0_01_03_007, nameof(PhysicalVulnerability));
        public static readonly ModStatType RadiationVulnerability = new ModStatType(0x_0_01_03_008, nameof(RadiationVulnerability));
        public static readonly ModStatType VoidVulnerability = new ModStatType(0x_0_01_03_009, nameof(VoidVulnerability));
        public static readonly ModStatType Vulnerability = new ModStatType(0x_0_01_03_010, nameof(Vulnerability));
        public static readonly ModStatType MaxHitPoints = new ModStatType(0x_0_01_04_001, nameof(MaxHitPoints));

        // Movement category
        public static readonly ModStatType ForwardPower = new ModStatType(0x_0_02_00_001, nameof(ForwardPower));
        public static readonly ModStatType YawTorque = new ModStatType(0x_0_02_00_002, nameof(YawTorque));
        public static readonly ModStatType ElevationPower = new ModStatType(0x_0_02_00_003, nameof(ElevationPower));
        public static readonly ModStatType StrafePower = new ModStatType(0x_0_02_00_004, nameof(StrafePower));
        public static readonly ModStatType EnginePower = new ModStatType(0x_0_02_00_005, nameof(EnginePower));
        public static readonly ModStatType PilotAidLevel = new ModStatType(0x_0_02_00_006, nameof(PilotAidLevel));

        public static readonly ModStatType JumpChargeSpeed = new ModStatType(0x_0_02_01_001, nameof(JumpChargeSpeed));
        public static readonly ModStatType VoidJumpCapable = new ModStatType(0x_1_02_01_002, nameof(VoidJumpCapable));
        
        public static readonly ModStatType SignatureVelocity = new ModStatType(0x_0_02_02_001, nameof(SignatureVelocity));
        public static readonly ModStatType SignatureAngularVelocity = new ModStatType(0x_0_02_02_002, nameof(SignatureAngularVelocity));
        
        public static readonly ModStatType ThrusterBoosterDuration = new ModStatType(0x_0_02_03_001, nameof(ThrusterBoosterDuration));
        public static readonly ModStatType ThrusterBoosterCooldown = new ModStatType(0x_0_02_03_002, nameof(ThrusterBoosterCooldown));
        public static readonly ModStatType ThrusterBoosterRechargeSpeed = new ModStatType(0x_0_02_03_003, nameof(ThrusterBoosterRechargeSpeed));

        // Movement pips
        public static readonly ModStatType ForwardPowerPip = new ModStatType(0x_1_02_10_001, nameof(ForwardPowerPip));
        public static readonly ModStatType YawTorquePip = new ModStatType(0x_1_02_10_002, nameof(YawTorquePip));
        public static readonly ModStatType ElevationPowerPip = new ModStatType(0x_1_02_10_003, nameof(ElevationPowerPip));
        public static readonly ModStatType StrafePowerPip = new ModStatType(0x_1_02_10_004, nameof(StrafePowerPip));
        public static readonly ModStatType EnginePowerPip = new ModStatType(0x_1_02_10_005, nameof(EnginePowerPip));
        public static readonly ModStatType PilotAidLevelPip = new ModStatType(0x_1_02_10_006, nameof(PilotAidLevelPip));

        public static readonly ModStatType JumpChargeSpeedPip = new ModStatType(0x_1_02_11_001, nameof(JumpChargeSpeedPip));
        
        public static readonly ModStatType SignatureVelocityPip = new ModStatType(0x_1_02_12_001, nameof(SignatureVelocityPip));
        public static readonly ModStatType SignatureAngularVelocityPip = new ModStatType(0x_1_02_12_002, nameof(SignatureAngularVelocityPip));
        
        public static readonly ModStatType ThrusterBoosterDurationPip = new ModStatType(0x_1_02_13_001, nameof(ThrusterBoosterDurationPip));

        // Power category
        public static readonly ModStatType PowerWanted = new ModStatType(0x_1_03_00_001, nameof(PowerWanted));
        public static readonly ModStatType PowerProvided = new ModStatType(0x_1_03_00_002, nameof(PowerProvided));

        public static readonly ModStatType BatteryRechargeAmount = new ModStatType(0x_0_03_01_001, nameof(BatteryRechargeAmount));
        
        public static readonly ModStatType BreakerTemperatureShift = new ModStatType(0x_0_03_02_001, nameof(BreakerTemperatureShift));

        // Utility category
        public static readonly ModStatType ProcessingSpeed = new ModStatType(0x_0_04_00_001, nameof(ProcessingSpeed));

        public static readonly ModStatType HealingSpeed = new ModStatType(0x_0_04_01_001, nameof(HealingSpeed));

        public static readonly ModStatType ActionCooldown = new ModStatType(0x_0_04_02_001, nameof(ActionCooldown));
        public static readonly ModStatType EffectRadius = new ModStatType(0x_0_04_02_002, nameof(EffectRadius));

        public static readonly ModStatType AttractorMaxRange = new ModStatType(0x_0_04_03_001, nameof(AttractorMaxRange));
        public static readonly ModStatType AttractorPullVelocity = new ModStatType(0x_0_04_03_002, nameof(AttractorPullVelocity));

        public static readonly ModStatType LifeSupportEffectivity = new ModStatType(0x_0_04_04_001, nameof(LifeSupportEffectivity));
        public static readonly ModStatType PassiveTemperatureShift = new ModStatType(0x_0_04_04_002, nameof(PassiveTemperatureShift));

        // EVA category
        public static readonly ModStatType JetpackThrust = new ModStatType(0x_0_05_00_001, nameof(JetpackThrust));
        public static readonly ModStatType JetpackOxygenCapacity = new ModStatType(0x_0_05_00_002, nameof(JetpackOxygenCapacity));

        public static readonly ModStatType JetpackDashOxygen = new ModStatType(0x_0_05_01_001, nameof(JetpackDashOxygen));
        public static readonly ModStatType JetpackDashThrust = new ModStatType(0x_0_05_01_002, nameof(JetpackDashThrust));
        public static readonly ModStatType JetpackDashDuration = new ModStatType(0x_0_05_01_003, nameof(JetpackDashDuration));
        public static readonly ModStatType JetpackDashCooldown = new ModStatType(0x_0_05_01_004, nameof(JetpackDashCooldown));

        public static readonly ModStatType JetpackEnhanced = new ModStatType(0x_1_05_02_001, nameof(JetpackEnhanced));

        // Ectype stats category
        public static readonly ModStatType PassiveHealingAmount = new ModStatType(0x_0_06_00_002, nameof(PassiveHealingAmount));
        public static readonly ModStatType PassiveHealingInterval = new ModStatType(0x_0_06_00_003, nameof(PassiveHealingInterval));
        public static readonly ModStatType PassiveHealingCap = new ModStatType(0x_0_06_00_004, nameof(PassiveHealingCap));

        public static readonly ModStatType PushForceCounter = new ModStatType(0x_0_06_01_001, nameof(PushForceCounter));

        public static readonly ModStatType InstantCrypticWinChance = new ModStatType(0x_0_06_02_001, nameof(InstantCrypticWinChance));
        public static readonly ModStatType CrypticSkill = new ModStatType(0x_0_06_02_002, nameof(CrypticSkill));
        public static readonly ModStatType BreachRepairSkill = new ModStatType(0x_0_06_02_003, nameof(BreachRepairSkill));

        public static readonly ModStatType EnhancementProficiency = new ModStatType(0x_0_06_03_001, nameof(EnhancementProficiency));
        public static readonly ModStatType EnhancementDurationMultiplier = new ModStatType(0x_0_06_03_002, nameof(EnhancementDurationMultiplier));
        public static readonly ModStatType EnhancementRepairSkill = new ModStatType(0x_1_06_03_003, nameof(EnhancementRepairSkill));

        public static readonly ModStatType ThrowForce = new ModStatType(0x_0_06_04_001, nameof(ThrowForce));

        public static readonly ModStatType ClassAbilityDuration = new ModStatType(0x_0_06_05_001, nameof(ClassAbilityDuration));
        public static readonly ModStatType ClassAbilityCooldown = new ModStatType(0x_0_06_05_002, nameof(ClassAbilityCooldown));
        public static readonly ModStatType GrapplingHookRange = new ModStatType(0x_0_06_05_003, nameof(GrapplingHookRange));

        public static readonly ModStatType TargetLockDamage = new ModStatType(0x_0_06_06_001, nameof(TargetLockDamage));
        public static readonly ModStatType TargetLockCount = new ModStatType(0x_1_06_06_002, nameof(TargetLockCount));
        public static readonly ModStatType ScanRange = new ModStatType(0x_0_06_06_003, nameof(ScanRange));
        public static readonly ModStatType ScanSpeedIncreased = new ModStatType(0x_1_06_06_004, nameof(ScanSpeedIncreased));
    }
}