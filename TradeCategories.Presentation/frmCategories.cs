using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeCategories.Domain.Enums;
using TradeCategories.Presentation.Categories;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;

namespace PlayerUI
{
    public partial class frmCategories : Form
    {

        private readonly ICategoryService _categoryservice;

        public frmCategories(ICategoryService categoryservice)
        {
            _categoryservice = categoryservice;
            InitializeComponent();

            cboSectorClient.Items.Add(ESectorClient.Public);
            cboSectorClient.Items.Add(ESectorClient.Private);
            
            button1_Click(this, new EventArgs());

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void btnInsert_Click(object sender, EventArgs e) 
        {
            var category = new CategoryDTO();

            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                category.Id += long.Parse(r.Cells["Id"].Value.ToString());
            }

            category.Id += 1;
            category.Name = txtName.Text;
            category.ValueInitial = Convert.ToDecimal(txtValueInitial.Text);
            category.ValueFinal = Convert.ToDecimal(txtValueFinal.Text);
            category.SectorClient = (ESectorClient)Enum.Parse(typeof(ESectorClient), cboSectorClient.SelectedItem.ToString());
            var categoryservice = new CategoryController(_categoryservice);
            var userCreated = await categoryservice.Create(category);

            button1_Click(this, new EventArgs());
        }

        public async Task Get()
        {
            var categories = new CategoryController(_categoryservice);
            var list = await categories.Get();

            this.dataGridView1.DataSource = list;
        }

       
        private async void button1_Click(object sender, EventArgs e)
        {
            var categories = new CategoryController(_categoryservice);
            var list = await categories.Get();

            this.dataGridView1.DataSource = list;
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtId.Text = dataGridView1.CurrentRow.Cells["Id"].Value.ToString();
            txtName.Text = dataGridView1.CurrentRow.Cells["Name"].Value.ToString();
            txtValueInitial.Text = dataGridView1.CurrentRow.Cells["ValueInitial"].Value.ToString();
            txtValueFinal.Text = dataGridView1.CurrentRow.Cells["ValueFinal"].Value.ToString();
            cboSectorClient.SelectedItem = (ESectorClient)Enum.Parse(typeof(ESectorClient), dataGridView1.CurrentRow.Cells["SectorClient"].Value.ToString());
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            var categoryservice = new CategoryController(_categoryservice);
            await categoryservice.Remove(long.Parse(txtId.Text));

            button1_Click(this, new EventArgs());
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            var categoryservice = new CategoryController(_categoryservice);
            var category = categoryservice.GetById(long.Parse(txtId.Text));


            category.Result.Name = txtName.Text;
            category.Result.ValueInitial = Convert.ToDecimal(txtValueInitial.Text);
            category.Result.ValueFinal = Convert.ToDecimal(txtValueFinal.Text);
            category.Result.SectorClient = (ESectorClient)Enum.Parse(typeof(ESectorClient), cboSectorClient.SelectedItem.ToString());
            
            await categoryservice.Update(category.Result);

            button1_Click(this, new EventArgs());
        }
    }
}
