using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarmes_Automatico
{
    public partial class Form1 : Form
    {
        private Timer timer;

        public Dictionary<string, int> alarmes = new Dictionary<string, int>
        {
            { "07:30", 27 },
            { "08:20", 7  },
            { "09:10", 7  },
            { "10:00", 7  },
            { "10:20", 27 },
            { "11:10", 7  },
            { "12:00", 7  },
            { "13:00", 27 },
            { "13:50", 7  },
            { "14:40", 7  },
            { "15:00", 27 },
            { "15:50", 7  },
            { "16:40", 7  }
        };

        public Form1()
        {
            InitializeComponent();

            timer = new Timer { Interval = 1000 };
            timer.Tick += timer1_Tick;
            timer.Start();

            FillAlarms();
        }
        public void FillAlarms()
        {
            listBoxAlarmes.Items.Clear();
            
            foreach (var alarme in alarmes)
            {
                listBoxAlarmes.Items.Add($"Hora: {alarme.Key}, Duração: {alarme.Value - 2} segundos");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string horaAtual = DateTime.Now.ToString("HH:mm");

            if (alarmes.ContainsKey(horaAtual))
            {
                int duracao = alarmes[horaAtual];
                bool vinheta = (duracao == 27);

                PlayAlarm(duracao, vinheta);
            }
        }

        private void PlayAlarm(int seconds, bool vinheta)
        {
            using (SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.alarme))
            {
                soundPlayer.Play();

                System.Threading.Thread.Sleep(seconds * 1000);

                soundPlayer.Stop();
            }

            if (vinheta)
            {
                using (SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.vinheta))
                {
                    soundPlayer.Play();
                    System.Threading.Thread.Sleep(29 * 1000);
                    soundPlayer.Stop();
                }
            }

            System.Threading.Thread.Sleep(60 * 1000);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Criado por Jhonatan Cordeiro Lopes (22/02/2025 - 2 Ano A)", "Créditos");
        }

        private void label1_Click(object sender, EventArgs e)
        {
            SoundPlayer soundPlayer = new SoundPlayer(Properties.Resources.vinheta);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modHorario form = new modHorario(this, alarmes);
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
