using System;
using System.Collections.Generic;
using VMFramework.Core;
using Sirenix.OdinInspector;
using UnityEngine.Scripting;
using VMFramework.GameLogicArchitecture;
using VMFramework.Procedure;

namespace VMFramework.Recipe
{
    [ManagerCreationProvider(ManagerType.OtherCore)]
    public class RecipeQueryManager : UniqueMonoBehaviour<RecipeQueryManager>
    {
        [ShowInInspector]
        public static List<Func<object, IEnumerable<Recipe>>> recipeInputQueryHandlers =
            new();

        [ShowInInspector]
        public static List<Func<object, IEnumerable<Recipe>>> recipeOutputQueryHandlers =
            new();

        public static void RegisterRecipeInputQueryHandler(
            Func<object, IEnumerable<Recipe>> handler)
        {
            recipeInputQueryHandlers.Add(handler);
        }

        public static void RegisterRecipeOutputQueryHandler(
            Func<object, IEnumerable<Recipe>> handler)
        {
            recipeOutputQueryHandlers.Add(handler);
        }

        public static IEnumerable<Recipe> GetRecipesByInput(object item)
        {
            foreach (var handler in recipeInputQueryHandlers)
            {
                foreach (var recipe in handler(item))
                {
                    yield return recipe;
                }
            }
        }

        public static IEnumerable<Recipe> GetRecipesByOutput(object item)
        {
            foreach (var handler in recipeOutputQueryHandlers)
            {
                foreach (var recipe in handler(item))
                {
                    yield return recipe;
                }
            }
        }
    }

    [GameInitializerRegister(typeof(GameInitializationProcedure))]
    [Preserve]
    public sealed class RecipeQueryInitializer : IGameInitializer
    {
        void IInitializer.OnInitComplete(Action onDone)
        {
            foreach (var recipe in GamePrefabManager.GetAllGamePrefabs<Recipe>())
            {
                foreach (var recipeInputQueryPattern in recipe.GetInputQueryPatterns())
                {
                    recipeInputQueryPattern.RegisterCache(recipe);
                }

                foreach (var recipeOutputQueryPattern in recipe
                             .GetOutputQueryPatterns())
                {
                    recipeOutputQueryPattern.RegisterCache(recipe);
                }
            }

            foreach (var recipeInputQueryPatternType in
                     typeof(IRecipeInputQueryPattern).GetDerivedClasses(false,
                         false))
            {
                var method = recipeInputQueryPatternType
                    .GetStaticMethodByAttribute<RecipeInputQueryHandlerAttribute>(false);

                if (method != null)
                {
                    var handler = (Func<object, IEnumerable<Recipe>>)Delegate
                        .CreateDelegate(typeof(Func<object, IEnumerable<Recipe>>), method);

                    RecipeQueryManager.RegisterRecipeInputQueryHandler(handler);
                }
                else
                {
                    throw new Exception(
                        $"{recipeInputQueryPatternType}没找到带有" +
                        $"{nameof(RecipeInputQueryHandlerAttribute)}的静态方法");
                }
            }

            foreach (var recipeOutputQueryPatternType in
                     typeof(IRecipeOutputQueryPattern).GetDerivedClasses(false,
                         false))
            {
                var method = recipeOutputQueryPatternType
                    .GetStaticMethodByAttribute<RecipeOutputQueryHandlerAttribute>(
                        false);

                if (method != null)
                {
                    var handler = (Func<object, IEnumerable<Recipe>>)Delegate
                        .CreateDelegate(typeof(Func<object, IEnumerable<Recipe>>),
                            method);

                    RecipeQueryManager.RegisterRecipeOutputQueryHandler(handler);
                }
                else
                {
                    throw new Exception(
                        $"{recipeOutputQueryPatternType}没找到带有" +
                        $"{nameof(RecipeOutputQueryHandlerAttribute)}的静态方法");
                }
            }

            onDone();
        }
    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RecipeInputQueryHandlerAttribute : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class RecipeOutputQueryHandlerAttribute : Attribute
    {

    }
}
