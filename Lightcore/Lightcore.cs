namespace Lightcore
{
    using System;
    using System.Windows.Forms;
    using Lightcore.Common.Models;
    using Lightcore.Lighting;
    using Lightcore.View;
    using Lightcore.Viewer;
    using Lightcore.Worlds;
    using Application = UI.Application;

    public partial class LightcoreForm : Form
    {
        Application application;

        public LightcoreForm()
        {
            InitializeComponent();
        }

        private void LightcoreForm_Load(object sender, EventArgs e)
        {
            application = new Application();
            application.Camera = new World(Settings.CameraRererenceFrame);
            application.World = new TorusWorld().ToWorld();
            application.StatusChanged += Status;
            application.SuspendChanged += Suspend;


            this.KeyPreview = true;
            KeyDown += new KeyEventHandler(application.KeyboardService.OnKeyDown);
            RenderButton.Click += new EventHandler(application.ButtonService.OnRenderButton);
            CancelRenderButton.Click += new EventHandler(application.ButtonService.OnCancelRenderButton);

            //application.PreprocessorStack.Add(new LightMapProcessor()); // Enable this processor will render shadows
            application.PreprocessorStack.Add(new LightProcessor());
            //application.PreprocessorStack.Add(new NormalProcessor()); // Used for debugging, will render normals

            application.ProcessorStack.Add(new ShineProcessor());
            application.ProcessorStack.Add(new ProjectProcessor());
            application.ProcessorStack.Add(new ScaleProcessor());
            application.ProcessorStack.Add(new DarkProcessor());
            application.ProcessorStack.Add(new ViewProcessor(ViewPictureBox, 100));
            //application.ProcessorStack.Add(new StatisticsProcessor()); // Used for debugging, will output statistics
        }

        private void LightcoreForm_Shown(Object sender, EventArgs e)
        {
            application.Start();
        }

        void Status(Object sender, string status)
        {
            TryInvoke(() => StripStatusLabel.Text = status);
        }

        void Suspend(Object sender, bool suspended)
        {
            TryInvoke(() => RenderButton.Enabled = !suspended);
            TryInvoke(() => CancelRenderButton.Enabled = suspended);
        }

        void TryInvoke(MethodInvoker action)
        {
            try { 
                if (this.IsHandleCreated)
                {
                    if (this.InvokeRequired)
                    {
                        this.Invoke(action);              
                    }
                    else
                    {
                        action.Invoke();
                    }
                }
            }

            catch
            {

            }
        }

    }
}
