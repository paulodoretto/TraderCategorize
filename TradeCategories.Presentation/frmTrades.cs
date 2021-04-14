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
using TradeCategories.Presentation.Controllers;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;

namespace PlayerUI
{
    public partial class frmTrades : Form
    {
        private List<TraderDTO> listTrades;
        private readonly ITradeService _tradeservice;

        public frmTrades(ITradeService tradeservice)
        {
            _tradeservice = tradeservice;
            InitializeComponent();

            cboSectorClient.Items.Add(ESectorClient.Public);
            cboSectorClient.Items.Add(ESectorClient.Private);

            listTrades = new List<TraderDTO>();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            TraderDTO trades = new TraderDTO();

            trades.Value = Convert.ToDecimal(txtValor.Text);
            trades.SectorClient = (ESectorClient)Enum.Parse(typeof(ESectorClient), cboSectorClient.SelectedItem.ToString());

            listTrades.Add(trades);
            listBox1.Items.Add(trades.Value + " - " + trades.SectorClient.ToString());

        }

        private async void button8_Click(object sender, EventArgs e)
        {

            listBox2.Items.Clear();

            var traderservice = new TraderController(_tradeservice);

            var categories = await traderservice.Categorize(listTrades);

            foreach (var item in categories)
            {
                listBox2.Items.Add(item);
            }

           

                 }
    }
}
