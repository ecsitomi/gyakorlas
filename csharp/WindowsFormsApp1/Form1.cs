using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient; //HOZZÁADNI

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private BookService bookService; //meghívni a betöltés funkciót
        public Form1()
        {
            InitializeComponent();
            booksGrid.AutoGenerateColumns = false; //ne automatikus állítson be az oszlopokat a datagrid
        }

        private void button1_Click(object sender, EventArgs e) //törlés gomb
        {
            if (booksGrid.SelectedRows.Count == 0) //ha nincs kiválaszva semmi
            {
                MessageBox.Show("Törléshez előbb válasszon ki könyvet!");
                return;
            }
            DialogResult result = MessageBox.Show("Biztos szeretné törölni a kiválasztott könyvet?", "Biztos?", MessageBoxButtons.YesNo);
            if (result != DialogResult.Yes) //ha nem akkor kilép 
            {
                return;
            }
            try
            {
                //TÖRLÉS 
                Book selected = booksGrid.SelectedRows[0].DataBoundItem as Book; //a kiválasztott sor egy könyv
                if (bookService.DeleteBook(selected.Id))
                {
                    MessageBox.Show("Sikeres törlés");
                }
                else
                {
                    MessageBox.Show("A könyv már korábban törlésre került", "Hiba a törlés során!");
                }
                RefreshBooksGrid();

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message, "Hiba történt a törlés során!");
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                bookService = new BookService(); //adatok betöltése példányosítás
                RefreshBooksGrid(); //datagrid táblázat frissítése
            }
            catch  (MySqlException ex)
            {
                MessageBox.Show("Hiba történt az adatbázis kapcsolat létrehozásakor");
                this.Close();
            }
        }
        private void RefreshBooksGrid()
        {
            //booksGrid.Columns.Clear();
            //booksGrid.Columns["title"].DataPropertyName = "Title";
            //booksGrid.Columns["author"].DataPropertyName = "Author";
            //booksGrid.Columns["publish_year"].DataPropertyName = "Publish_year";
            //booksGrid.Columns["page_count"].DataPropertyName = "Page_count";
            //FORM DESIGNER BELÜL IS PROPERTIBEN BEÁLLÍTHATÓ

            booksGrid.DataSource = bookService.GetBooks(); //adatforrás a datagridnek 
            //magától is betölti az oszlopokat
            booksGrid.ClearSelection();
        }
    }
}
