using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Exemplo01.Model;

namespace Exemplo01
{
    public partial class TaskMaster : Form
    {
        private tmDBContext tmContext;
        public TaskMaster()
        {
            InitializeComponent();

            tmContext = new tmDBContext();

            var statuses = tmContext.Statuses.ToList();

            foreach (Status status in statuses)
            {
                cbStatus.Items.Add(status);
            }
            RefreshData();
        }

        private void RefreshData()
        {
            BindingSource bs = new BindingSource();

            var query = from tarefa in tmContext.Tasks
                        orderby tarefa.Data
                        select new { tarefa.Id, Tarefa = tarefa.Nome, Status = tarefa.Status.Nome, Prazo = tarefa.Data };

            bs.DataSource = query.ToList();

            dataGridView1.DataSource = bs;
            dataGridView1.Refresh();
        }

        private void btnCriar_Click(object sender, EventArgs e)
        {
            if (cbStatus.SelectedItem != null && txtTarefa.Text != String.Empty)
            {
                var novaTerefa = new Model.Task
                {
                    Nome = txtTarefa.Text,
                    StatusId = (cbStatus.SelectedItem as Model.Status).Id,
                    Data = dtpData.Value
                };

                tmContext.Tasks.Add(novaTerefa);

                tmContext.SaveChanges();
                RefreshData();
                LimparForm();
            }
            else
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
            }
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count != 0)
            {

                var task = tmContext.Tasks.Find(Convert.ToInt32(dataGridView1.SelectedCells[0].Value));
                tmContext.Tasks.Remove(task);
                tmContext.SaveChanges();
                RefreshData();
            }
            else
            {
                MessageBox.Show("Seleciona uma Linha");
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma Linha", "AVISO");
            }
            else
            {

                if (btnAtualizar.Text == "Atualizar")
                {
                    txtTarefa.Text = dataGridView1.SelectedCells[1].Value.ToString();
                    dtpData.Value = (DateTime)dataGridView1.SelectedCells[3].Value;
                    foreach (Status status in cbStatus.Items)
                    {
                        if (status.Nome == dataGridView1.SelectedCells[2].Value.ToString())
                        {
                            cbStatus.SelectedItem = status;
                        }
                    }
                    btnAtualizar.Text = "Salvar";
                }
                else if (btnAtualizar.Text == "Salvar")
                {
                    var tarefa = tmContext.Tasks.Find((int)dataGridView1.SelectedCells[0].Value);
                    tarefa.Nome = txtTarefa.Text;
                    tarefa.StatusId = (cbStatus.SelectedItem as Status).Id;
                    tarefa.Data = dtpData.Value;

                    tmContext.SaveChanges();
                    RefreshData();
                    LimparForm();
                }
            }

        }

        private void LimparForm()
        {
            btnAtualizar.Text = "Atualizar";
            txtTarefa.Clear();
            dtpData.Value = DateTime.Now;
            cbStatus.SelectedIndex = -1;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            LimparForm();
        }
    }
}
