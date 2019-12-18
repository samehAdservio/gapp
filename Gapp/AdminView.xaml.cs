using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;
using System.Data.OleDb;
using System.Collections;
namespace Gapp
{
    /// <summary>
    /// Logique d'interaction pour AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        OleDbConnection con;
        DataTable dt;
        public AdminView()
        { con = new OleDbConnection();

            con.ConnectionString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + AppDomain.CurrentDomain.BaseDirectory + "\\guestDb.mdb";
            InitializeComponent();
            BindGrid();
        }
        private void BindGrid()
        {
            DateTime today = DateTime.Now.Date;
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select ID,firstName,lastName,company,departureTime,arrivalTime from guestTable";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime selectedDate = dpick.SelectedDate.Value;
          
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select ID,firstName,lastName,company,departureTime,arrivalTime from guestTable where creationDate = #" + selectedDate + "#";

            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();
        
    }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            OleDbCommand cmd = new OleDbCommand();
            if (con.State != ConnectionState.Open)
                con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select  ID,firstName,lastName,company,departureTime,arrivalTime from guestTable where lastName like '%" + txtSearch.Text + "%'";
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            dt = new DataTable();
            da.Fill(dt);
            gvData.ItemsSource = dt.AsDataView();
        }
    }
}
