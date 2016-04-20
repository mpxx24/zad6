using System;
using System.Runtime.ExceptionServices;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;
using SharpGL;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Quadrics;
using Polygon = SharpGL.SceneGraph.Primitives.Polygon;

namespace zad6 {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public static float[] light_ambient = {0.2f, 0.2f, 0.2f, 1.0f};

        private static Sphere sph;

        private static IntPtr ptr;

        private OpenGL Gl;

        public MainWindow() {
            InitializeComponent();
            var dispTimer = new DispatcherTimer();
            dispTimer.Tick += timer_tick;
            dispTimer.Interval = new TimeSpan(0, 0, 0, 500);

            // Gl.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);
            sph = new Sphere();
        }

        private void timer_tick(object sender, EventArgs e) {
        }
        
        private void OpenGLControl_OnOpenGLDraw(object sender, OpenGLEventArgs args) {
            Gl = new OpenGL();
            Gl = args.OpenGL;
            Gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            Gl.MatrixMode(MatrixMode.Projection);
            Gl.LoadIdentity();

            Gl.Scale(3.6, 3.6, 3.6);
            Gl.Rotate(90, 1.0f, 0, 0);

            ptr = Gl.NewQuadric();
            Gl.QuadricNormals(ptr, OpenGL.GLU_SMOOTH);
            Gl.QuadricNormals(ptr, OpenGL.GL_TRUE);
            Gl.Sphere(ptr, 1.0f, 100, 100);

            Gl.Light(LightName.Light0, LightParameter.Ambient, light_ambient);
            Gl.LightModel(LightModelParameter.Ambient, 1.8f);
            Gl.Light(LightName.Light0, LightParameter.Position, new[] {10.0f, 10.0f, 10.0f, 1.0f});

            Gl.Enable(OpenGL.GL_LIGHTING);
            Gl.Enable(OpenGL.GL_LIGHT0);
            Gl.End();
            Gl.Flush();
        }

        private void OpenGLControl_OnOpenGLInitialized(object sender, OpenGLEventArgs args) {
            args.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);
        }
    }
}