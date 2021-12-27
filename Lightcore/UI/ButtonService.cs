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

        public void OnAnimateButton(object sender, EventArgs e)
        {
            application.Animate();
        }

        public void OnCancelRenderButton(object sender, EventArgs e)
        {
            application.Cancel();
        }
    }
}
