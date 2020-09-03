namespace Lightcore.UI
{
    using System;

    public class ButtonService
    {
        Application application;

        public ButtonService(Application application)
        {
            this.application = application;
        }

        public void OnRenderButton(object sender, EventArgs e)
        {
            application.Process();
        }

        public void OnCancelRenderButton(object sender, EventArgs e)
        {
            application.Cancel();
        }
    }
}
