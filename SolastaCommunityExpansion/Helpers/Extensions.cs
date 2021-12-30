﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SolastaCommunityExpansion.Helpers
{
    internal static class Extensions
    {
        /// <summary>
        /// Makes using RulesetActor.EnumerateFeaturesToBrowse simpler
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actor"></param>
        /// <param name="populateActorFeaturesToBrowse">Set to true to populate actor.FeaturesToBrowse as well as returning features.  false to just return features.</param>
        /// <param name="featuresOrigin"></param>
        /// <returns></returns>
        /// <summary>
        public static ICollection<T> EnumerateFeaturesToBrowse<T>(
            this RulesetActor actor, bool populateActorFeaturesToBrowse = false, Dictionary<FeatureDefinition, RuleDefinitions.FeatureOrigin> featuresOrigin = null)
        {
            var features = populateActorFeaturesToBrowse ? actor.FeaturesToBrowse : new List<FeatureDefinition>();
            actor.EnumerateFeaturesToBrowse<T>(features, featuresOrigin);
            return features.OfType<T>().ToList();
        }

        /// <summary>
        /// CheckIsInvisible extension matching CheckIsEnabled() etc GameGadget methods
        /// </summary>
        /// <param name="gadget"></param>
        /// <returns></returns>
        public static bool CheckIsInvisible(this GameGadget gadget)
        {
            var result = (bool)CheckConditionName.Invoke(gadget, new object[] { "Invisible", true, false });

            Main.Log($"{gadget.UniqueNameId}, Invisible={result}");

            return result;
        }

#pragma warning disable S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
        private static readonly MethodInfo CheckConditionName
            = typeof(GameGadget).GetMethod("CheckConditionName", BindingFlags.Instance | BindingFlags.NonPublic);
#pragma warning restore S3011 // Reflection should not be used to increase accessibility of classes, methods, or fields
    }
}
