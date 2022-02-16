﻿using System.Linq;
using HarmonyLib;
using static SolastaModApi.DatabaseHelper.CharacterClassDefinitions;
using static SolastaModApi.DatabaseHelper.CharacterSubclassDefinitions;

namespace SolastaCommunityExpansion.Multiclass.Patches.SharedCombinedSpells
{
    internal static class RulesetImplementationManagerPatcher
    {
        internal static CharacterClassDefinition RestActivityInvokerClass { get; set; }
        internal static RulesetCharacterHero HeroWithSpellRepertoire { get; set; }
        internal static RulesetSpellRepertoire SpellRepertoire { get; set; }

        // gets hero and repertoire context to be used later on FlexibleCastingItem / ensures Wizard / Sorcerer are using caster level
        [HarmonyPatch(typeof(RulesetImplementationManager), "ApplySpellSlotsForm")]
        internal static class RulesetImplementationManagerApplySpellSlotsForm
        {
            internal static bool Prefix(EffectForm effectForm, RulesetImplementationDefinitions.ApplyFormsParams formsParams)
            {
                if (!Main.Settings.EnableMulticlass)
                {
                    return true;
                }

                var spellSlotsForm = effectForm.SpellSlotsForm;

                if (spellSlotsForm.Type == SpellSlotsForm.EffectType.RecoverHalfLevelUp && RestActivityInvokerClass != null)
                {
                    var sourceCharacter = formsParams.sourceCharacter as RulesetCharacterHero;

                    foreach (var spellRepertoire in sourceCharacter.SpellRepertoires)
                    {
                        var currentValue = sourceCharacter.ClassesAndLevels[RestActivityInvokerClass];
                        var slotsCapital = currentValue % 2 == 0 ? currentValue / 2 : (currentValue + 1) / 2;

                        Gui.GuiService.GetScreen<SlotRecoveryModal>().ShowSlotRecovery(sourceCharacter, formsParams.activeEffect.SourceDefinition.Name, spellRepertoire, slotsCapital, spellSlotsForm.MaxSlotLevel);
                        break;
                    }
                }
                else if (spellSlotsForm.Type == SpellSlotsForm.EffectType.CreateSpellSlot || spellSlotsForm.Type == SpellSlotsForm.EffectType.CreateSorceryPoints)
                {
                    HeroWithSpellRepertoire = formsParams.sourceCharacter as RulesetCharacterHero;
                    SpellRepertoire = HeroWithSpellRepertoire.SpellRepertoires.Find(sr => sr.SpellCastingClass == Sorcerer);

                    Gui.GuiService.GetScreen<FlexibleCastingModal>().ShowFlexibleCasting(HeroWithSpellRepertoire, SpellRepertoire, spellSlotsForm.Type == SpellSlotsForm.EffectType.CreateSpellSlot);
                }
                else if (spellSlotsForm.Type == SpellSlotsForm.EffectType.GainSorceryPoints)
                {
                    HeroWithSpellRepertoire = formsParams.sourceCharacter as RulesetCharacterHero;
                    SpellRepertoire = HeroWithSpellRepertoire.SpellRepertoires.Find(sr => sr.SpellCastingClass == Sorcerer);

                    HeroWithSpellRepertoire.GainSorceryPoints(spellSlotsForm.SorceryPointsGain);
                }
                else if (spellSlotsForm.Type == SpellSlotsForm.EffectType.RecovererSorceryHalfLevelUp)
                {
                    HeroWithSpellRepertoire = formsParams.sourceCharacter as RulesetCharacterHero;
                    SpellRepertoire = HeroWithSpellRepertoire.SpellRepertoires.Find(sr => sr.SpellCastingClass == Sorcerer);

                    var currentValue = HeroWithSpellRepertoire.ClassesAndLevels[Sorcerer];
                    var sorceryPointsGain = currentValue % 2 == 0 ? currentValue / 2 : (currentValue + 1) / 2;

                    HeroWithSpellRepertoire.GainSorceryPoints(sorceryPointsGain);
                }

                return false;
            }
        }
    }
}
