using System.Reflection;
using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;

namespace VietnameseTranslation
{
    public class VietnameseTranslation : ModBehaviour
    {
        public static VietnameseTranslation Instance;

        public static string translationFile = "translations/Translation.xml";

        public void Awake()
        {
            Instance = this;
            // You won't be able to access OWML's mod helper in Awake.
            // So you probably don't want to do anything here.
            // Use Start() instead.
        }

        private void Start()
        {
            // Starting here, you'll have access to OWML's mod helper.
            ModHelper.Console.WriteLine($"My mod {nameof(VietnameseTranslation)} is loaded!", MessageType.Success);

            new Harmony("Nartiohk.VietnameseTranslation").PatchAll(Assembly.GetExecutingAssembly());

            // Example of accessing game code.
            OnCompleteSceneLoad(OWScene.TitleScreen, OWScene.TitleScreen); // We start on title screen
            LoadManager.OnCompleteSceneLoad += OnCompleteSceneLoad;

            var api = ModHelper.Interaction.TryGetModApi<ILocalizationAPI>("xen.LocalizationUtility");
            api.RegisterLanguage(this, "Vietnamese", translationFile);
            api.AddLanguageFont(
            this,
            "Vietnamese",
            "fonts/saira-variablefont_wdth,wght",   // actual bundle file
            "assets/saira-variablefont_wdth,wght.ttf" // font inside the bundle
            );
        }

        public void OnCompleteSceneLoad(OWScene previousScene, OWScene newScene)
        {
            if (newScene != OWScene.SolarSystem) return;
            ModHelper.Console.WriteLine("Loaded into solar system!", MessageType.Success);
        }
    }

}
