﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProNatur_BioMarkt_GmbH
{
    
    public partial class ProductScreen : Form
    {
        private SqlConnection databaseConnection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\louai\OneDrive\Dokumente\Pro-Natur Biomarkt GmbH.mdf;Integrated Security=True;Connect Timeout=30");
        private int lastSelectedProductKey;

        public ProductScreen()
        {
            InitializeComponent();
            showProducts();

        }

        private void showProducts()
        {
            databaseConnection.Open();

            string query = "select * from Products";
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, databaseConnection);

            var dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet);

            productDGV.DataSource = dataSet.Tables[0];

            productDGV.Columns[0].Visible = false;

            databaseConnection.Close();
        }

        private void btnProductSave_Click(object sender, EventArgs e)
        {
            if(textBoxProductName.Text == ""
                || textBoxBrand.Text == ""
                || comboBoxProductCategory.Text == ""
                || textBoxProductPrice.Text == "") 
            {

                MessageBox.Show("Bitte fülle alle Werte aus.");
                return; 
            }
            
            string productName = textBoxProductName.Text;
            string productBrand = textBoxBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("insert into Products values('{0}','{1}','{2}','{3}')"
                ,productName, productBrand, productCategory, productPrice);
            executeQuery(query);

            showProducts();
            clearAllFields();
        }

        private void btnProductEdit_Click(object sender, EventArgs e)
        {
            if (lastSelectedProductKey == 0)
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return;
            }
            string productName = textBoxProductName.Text;
            string productBrand = textBoxBrand.Text;
            string productCategory = comboBoxProductCategory.Text;
            string productPrice = textBoxProductPrice.Text;

            string query = string.Format("update Products set Name='{0}','{2}','{3}' where Id={4}"
                , productName, productBrand, productCategory, productPrice, lastSelectedProductKey);
            
            executeQuery(query);

            showProducts();
        }

        private void btnProductClear_Click(object sender, EventArgs e)
        {
            clearAllFields();
        }

        private void btnProductDelete_Click(object sender, EventArgs e)
        {
            if(lastSelectedProductKey == 0) 
            {
                MessageBox.Show("Bitte wähle zuerst ein Produkt aus.");
                return;
            }
            string query = string.Format("delete from Products where Id={0};", lastSelectedProductKey);
            executeQuery(query);

            clearAllFields();
            showProducts();
        }

        private void executeQuery(string query)
        {
            databaseConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, databaseConnection);
            sqlCommand.ExecuteNonQuery();
            databaseConnection.Close();
        }

        private void clearAllFields()
        {
            textBoxProductName.Text = "";
            textBoxBrand.Text = "";
            textBoxProductPrice.Text = "";
            comboBoxProductCategory.Text = "";
            comboBoxProductCategory.SelectedItem = null;
        }

        private void productDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBoxProductName.Text = productDGV.SelectedRows[0].Cells[1].Value.ToString();
            textBoxBrand.Text = productDGV.SelectedRows[0].Cells[2].Value.ToString();
            textBoxProductPrice.Text = productDGV.SelectedRows[0].Cells[3].Value.ToString();
            comboBoxProductCategory.Text = productDGV.SelectedRows[0].Cells[4].Value.ToString();

            lastSelectedProductKey = (int)productDGV.SelectedRows[0].Cells[0].Value;
        }
    }
}
