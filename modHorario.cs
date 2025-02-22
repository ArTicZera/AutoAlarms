using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alarmes_Automatico
{
    public partial class modHorario : Form
    {
        public Dictionary<string, int> alarmes;
        private Form1 mainForm;

        public modHorario(Form1 form, Dictionary<string, int> alarmes)
        {
            InitializeComponent();

            FillAlarms(alarmes);

            mainForm = form;
            this.alarmes = alarmes;
        }

        private void FillAlarms(Dictionary<string, int> alarmes)
        {
            listBoxAlarmes.Items.Clear();

            foreach (var alarme in alarmes)
            {
                listBoxAlarmes.Items.Add($"Hora: {alarme.Key}, Duração: {alarme.Value - 2} segundos");
            }
        }

        public Dictionary<string, int> GetAlarmes()
        {
            return alarmes;
        }

        private void modHorario_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Adicionar
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Por favor, preencha todos os campos.", "Erro");
                return;
            }

            string hora = textBox1.Text.Trim();

            if (!int.TryParse(textBox2.Text.Trim(), out int duracao) || duracao < 0)
            {
                MessageBox.Show("Duração deve ser um número inteiro positivo.", "Erro");
                return;
            }

            if (!alarmes.ContainsKey(hora))
            {
                alarmes.Add(hora, duracao);
                FillAlarms(alarmes);
                MessageBox.Show($"Alarme {hora} adicionado com duração de {duracao} segundos.", "Alarme Adicionado");
            }
            else
            {
                MessageBox.Show("Este horário já está cadastrado.", "Erro");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Remover

            if (listBoxAlarmes.SelectedItem != null)
            {
                string itemSelecionado = listBoxAlarmes.SelectedItem.ToString();

                string hora = itemSelecionado.Split(',')[0].Replace("Hora: ", "").Trim();

                if (alarmes.ContainsKey(hora))
                {
                    alarmes.Remove(hora);
                    FillAlarms(alarmes);
                    MessageBox.Show($"Alarme {hora} removido com sucesso!", "Removido");
                }
            }
            else
            {
                MessageBox.Show("Selecione um alarme para remover.", "Erro");
            }

            GetAlarmes();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Aplicar
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Cancelar
            mainForm.alarmes = alarmes;
            mainForm.FillAlarms();
            this.Close();

            //Atualiza o alarmes do Form1
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            //Horario

        }

        private void textBox2_TextChanged_1(object sender, EventArgs e)
        {
            //Duracao
        }
    }
}
