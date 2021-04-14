using System;
using System.Windows.Forms;
using TradeCategories.Services.Interfaces;

namespace PlayerUI
{
    public partial class frmMenu : Form
    {

        private readonly ICategoryService _categoryservice;
        private readonly ITradeService _tradeService;

        public frmMenu(ICategoryService categoryservice, ITradeService tradeService)
        {
            _categoryservice = categoryservice;
            _tradeService = tradeService;
            InitializeComponent();
            hideSubMenu();
        }

        private void hideSubMenu()
        {
            panelMediaSubMenu.Visible = false;
            panelPlaylistSubMenu.Visible = false;
        }

        private void showSubMenu(Panel subMenu)
        {
            if (subMenu.Visible == false)
            {
                hideSubMenu();
                subMenu.Visible = true;
            }
            else
                subMenu.Visible = false;
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null) activeForm.Close();
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelChildForm.Controls.Add(childForm);
            panelChildForm.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            showSubMenu(panelMediaSubMenu);
        }

        private void btnCategories_Click(object sender, EventArgs e)
        {
            openChildForm(new frmCategories(_categoryservice));
            hideSubMenu();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            showSubMenu(panelPlaylistSubMenu);
        }

        private void btnTrades_Click(object sender, EventArgs e)
        {
            openChildForm(new frmTrades(_tradeService));
            hideSubMenu();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
