using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace maintenance
{
    public partial class Form1 : Form
    {
        //globals
        string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + "UserInterface";


        
        public Form1()
        {
            InitializeComponent();
        }
        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        

        private void Form1_Load(object sender, EventArgs e)
        {
            string path = folderPath + "\\" + "Inventory" + ".txt";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            if (!File.Exists(path)) File.CreateText(path);
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        if (line != "")
                        {
                            PopulateDataGridview(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateDataGridview(string line)
        {
            string[] temp = line.Split('/');
            DataGridViewRow rowtoadd = (DataGridViewRow)dataGridView1.Rows[0].Clone();
            rowtoadd.Cells[0].Value = temp[0];
            rowtoadd.Cells[1].Value = temp[1];
            rowtoadd.Cells[2].Value = temp[2];
            if(temp[3] == "1")
            {
                rowtoadd.Cells[3].Value = true;
            }

            else
            {
                rowtoadd.Cells[3].Value = false;
            }
            dataGridView1.Rows.Add(rowtoadd);

        }

        private void savebtn_Click(object sender, EventArgs e)
        {
            string path = folderPath + "\\" + "Inventory" + ".txt";

            try
            {
                using (StreamWriter writer = new StreamWriter(path))
                {
                    string line;
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if(row.Cells[3].Value != null)
                        {
                            line = row.Cells[0].Value.ToString() + "/" + row.Cells[1].Value.ToString() + "/" + row.Cells[2].Value.ToString();
                            if ((bool)row.Cells[3].Value == true)
                            {
                                line += "/" + "1";
                            }

                            else
                            {
                                line += "/" + "0";
                            }
                            writer.WriteLine(line);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
