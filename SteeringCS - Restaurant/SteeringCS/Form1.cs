using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SteeringCS.behaviour;
using SteeringCS.goal_driven_behaviour.ThinkGoals;
using SteeringCS.graph;

namespace SteeringCS
{
    public partial class Form1 : Form
    {
        World world;
        System.Timers.Timer timer;
        
        public const int FPS = 60;
        public const float timeDelta = 1/(float)FPS;

        public Form1()
        {
            InitializeComponent();

            world = new World(w: dbPanel1.Width, h: dbPanel1.Height);

            lbl_ShowGraph.Text = world.graphVisible.ToString();
            lbl_ShowPath.Text = world.pathVisible.ToString();
            lbl_ShowGoals.Text = world.goalsVisible.ToString();

            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 1000.0/FPS;
            timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            world.Update(timeDelta);
            dbPanel1.Invalidate();
        }

        private void dbPanel1_Paint(object sender, PaintEventArgs e)
        {
            world.Render(e.Graphics);
        }
        
        private void dbPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            world.Target.Pos = new Vector2D(e.X, e.Y);


            world.TheBoss.Brain.HandleMessageToBrain("GoToPlayerSpot", e);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case 'd':
                    world.graphVisible = !world.graphVisible;
                    lbl_ShowGraph.Text = world.graphVisible.ToString();
                    break;
                case 'f':
                    world.pathVisible = !world.pathVisible;
                    lbl_ShowPath.Text = world.pathVisible.ToString();
                    break;
                case 'g':
                    world.goalsVisible = !world.goalsVisible;
                    lbl_ShowGoals.Text = world.goalsVisible.ToString();
                    break;
            }
        }
    }
}
