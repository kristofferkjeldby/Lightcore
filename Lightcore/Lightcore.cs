namespace Lightcore
{
    using Lightcore.Common.Models;
    using Lightcore.Lighting;
    using Lightcore.View;
    using Lightcore.Viewer;
    using Lightcore.Worlds;
    using Lightcore.Worlds.Models;
    using System;
    using System.Windows.Forms;
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
            application.StatusChanged += Status;
            application.SuspendChanged += Suspend;
            application.GetWorldBuilder = GetWorldBuilder;

            this.KeyPreview = true;
            KeyDown += new KeyEventHandler(application.KeyboardService.OnKeyDown);
            RenderButton.Click += new EventHandler(application.ButtonService.OnRenderButton);
            AnimateButton.Click += new EventHandler(application.ButtonService.OnAnimateButton);
            CancelRenderButton.Click += new EventHandler(application.ButtonService.OnCancelRenderButton);

            // Enable this processor will render shadows
            // This processor is slow, and I am working on a different
            // approach :(
            // application.PreprocessorStack.Add(new LightMapProcessor()); 

            application.PreprocessorStack.Add(new LightProcessor());
            
            // Used for debugging, will render normals   
            // Remember to set debug = true in the settings file
            //application.PreprocessorStack.Add(new NormalProcessor()); 

            application.ProcessorStack.Add(new ShineProcessor());
            application.ProcessorStack.Add(new ProjectProcessor());
            application.ProcessorStack.Add(new ScaleProcessor());
            application.ProcessorStack.Add(new DarkProcessor());
            application.ProcessorStack.Add(new ViewProcessor(ViewPictureBox, 100));

            // Used for debugging, will output statistics           
            // application.ProcessorStack.Add(new StatisticsProcessor()); 
        }

        private void LightcoreForm_Shown(Object sender, EventArgs e)
        {
            application.Start();
        }

        public WorldBuilder GetWorldBuilder()
        {
            // Register your worldbuilder here
            return new TestWorld();
        }

        void Status(object sender, string status)
        {
            if (Settings.ShowStatus)
                TryInvoke(() => StripStatusLabel.Text = status);
        }

        void Suspend(object sender, bool suspended)
        {
            TryInvoke(() => RenderButton.Enabled = !suspended);
            TryInvoke(() => AnimateButton.Enabled = !suspended);
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
