using Components.UI.Screens;

namespace Components.UI
{
    public class LoadingScreen : ScreenComponent
    {
        public static LoadingScreen Instance;

        protected override void Start()
        {
            base.Start();

            if (Instance != null)
            {
                Destroy(Instance);
            }
            Instance = this;
        }
    }
}