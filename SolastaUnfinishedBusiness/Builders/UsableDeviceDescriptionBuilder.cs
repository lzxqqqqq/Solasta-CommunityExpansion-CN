﻿using UnityEngine.AddressableAssets;
using static EquipmentDefinitions;

namespace SolastaUnfinishedBusiness.Builders;

public class UsableDeviceDescriptionBuilder
{
    private readonly UsableDeviceDescription description;

    public UsableDeviceDescriptionBuilder()
    {
        description = new UsableDeviceDescription
        {
            usage = ItemUsage.ByFunction,
            chargesCapital = ItemChargesCapital.Fixed,
            chargesCapitalNumber = 1,
            chargesCapitalDie = RuleDefinitions.DieType.D1,
            chargesCapitalBonus = 0,
            rechargeRate = RuleDefinitions.RechargeRate.Dawn,
            rechargeNumber = 1,
            rechargeDie = RuleDefinitions.DieType.D1,
            rechargeBonus = 0,
            outOfChargesConsequence = ItemOutOfCharges.Persist,
            magicAttackBonus = 0,
            saveDC = 10,
            onUseParticle = new AssetReference()
        };
    }

    public UsableDeviceDescriptionBuilder SetSaveDc(int DC)
    {
        description.saveDC = DC;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetUsage(ItemUsage usage)
    {
        description.usage = usage;
        return this;
    }

    public UsableDeviceDescriptionBuilder AddFunctions(params DeviceFunctionDescription[] functions)
    {
        description.DeviceFunctions.AddRange(functions);
        return this;
    }

    public UsableDeviceDescription Build()
    {
        return description;
    }

    #region Charge

    public UsableDeviceDescriptionBuilder SetCharges(
        ItemChargesCapital capital = ItemChargesCapital.Fixed,
        int number = 1,
        RuleDefinitions.DieType dieType = RuleDefinitions.DieType.D1,
        int bonus = 0)
    {
        description.chargesCapital = capital;
        description.chargesCapitalNumber = number;
        description.chargesCapitalDie = dieType;
        description.chargesCapitalBonus = bonus;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapital(ItemChargesCapital capital)
    {
        description.chargesCapital = capital;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapitalNumber(int number)
    {
        description.chargesCapitalNumber = number;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapitalDie(RuleDefinitions.DieType dieType)
    {
        description.chargesCapitalDie = dieType;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetChargesCapitalBonus(int bonus)
    {
        description.chargesCapitalBonus = bonus;
        return this;
    }

    #endregion

    #region Recharge

    public UsableDeviceDescriptionBuilder SetRecharge(
        RuleDefinitions.RechargeRate rate = RuleDefinitions.RechargeRate.Dawn,
        int number = 1,
        RuleDefinitions.DieType dieType = RuleDefinitions.DieType.D1,
        int bonus = 0)
    {
        description.rechargeRate = rate;
        description.rechargeNumber = number;
        description.rechargeDie = dieType;
        description.rechargeBonus = bonus;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeRate(RuleDefinitions.RechargeRate rate)
    {
        description.rechargeRate = rate;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeNumber(int number)
    {
        description.rechargeNumber = number;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeDie(RuleDefinitions.DieType dieType)
    {
        description.rechargeDie = dieType;
        return this;
    }

    public UsableDeviceDescriptionBuilder SetRechargeBonus(int bonus)
    {
        description.rechargeBonus = bonus;
        return this;
    }

    #endregion
}
