using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WebAPIClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GetData();
        }

        ObservableCollection<Students> liststudent = new ObservableCollection<Students>();
        private void GetData()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("api/students").Result;

            if (response.IsSuccessStatusCode)
            {
                var students = response.Content.ReadAsAsync<IEnumerable<Students>>().Result;
                //List<Students1> studentslist = new List<Students1>();
                //foreach(var item in students)
                //{
                //    Students1 stu = new Students1()
                //    {
                //        id = item.id,
                //        name = item.name,
                //        medium_score = item.medium_score,
                //        PhoneNo = item.PhoneNo,
                //        sex = item.sex,
                //    };
                //    studentslist.Add(stu);

                //}
                //this.liststudent = new ObservableCollectiosn<Students>(students);
                var count = students.Count();
                usergrid.ItemsSource = students;
               // usergrid.Columns[0].Visibility = Visibility.Collapsed;

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //txtClassId.Text = usergrid.Columns.Count.ToString();
            //usergrid.Columns[0].Visibility = Visibility.Collapsed;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var student = new Students();

            student.class_id = txtClassId.Text;
            student.id = txtId.Text;
            student.name = txtName.Text;
            student.sex = txtSex.Text;

            student.PhoneNo = txtphone.Text;

            var response = client.PostAsJsonAsync("api/students", student).Result;

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Students Added");
                txtClassId.Text = "";
                txtId.Text = "";
                txtName.Text = "";
                txtSex.Text = "";

                txtphone.Text = "";
                GetData();
            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var id = txtSearch.Text.Trim();

            var url = "api/students/" + id;

            HttpResponseMessage response = client.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var students = response.Content.ReadAsAsync<Students>().Result;

                MessageBox.Show("Student Found : " + students.class_id + " " + students.id);
                //MessageBox.Show("Student Found : " + students.id  );

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");

            var id = txtDelete.Text.Trim();

            var url = "api/students/" + id;
            //
            HttpResponseMessage response = client.DeleteAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {


                MessageBox.Show("Student Deleted");
                GetData();

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }
        //hide column 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.usergrid.Columns[0].Visibility = Visibility.Collapsed;
        }
        //private void columnHeader_Click(object sender, RoutedEventArgs e)
        //{
        //    var columnHeader = sender as DataGridColumnHeader;
        //    if (columnHeader != null)
        //    {
        //        PopupInfo p = new PopupInfo();

        //        p.Show();

        //    }

        //}

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           //get all object attribute cua dong duoc select
            DataGridRow data = sender as DataGridRow;
            //lay du lieu cua dong duoc select convert qua kieu students
            var student = data.DataContext as Students;
            // khoi tao doi tuong popupinfo va truyen tham so student
             PopupInfo popup = new PopupInfo(student);
            //tao action de popup call back main window 
            popup.UpdateMainWindow = this.UpdateDataGrid;
            //show popup window
            popup.Show();
        }

        private void UpdateDataGrid(Students Students)
        {
            //var stu = Students;
            //txtName.Text = stu.name;
            //txtId.Text = stu.id;
            //txtphone.Text = stu.PhoneNo;
            //txtSex.Text = stu.sex;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:3000/");
            

            var url = "api/students/" + Students.id;
            //
            //Students stunew = new Students()
            //{
            //    name = Students.name,
            //    medium_score = Students.medium_score,
            //    PhoneNo = Students.PhoneNo,
            //    sex = Students.sex,
            //};

            
            HttpResponseMessage response = client.PutAsJsonAsync(url, Students).Result;

            if (response.IsSuccessStatusCode)
            {


                MessageBox.Show("Update Complete");
                GetData();

            }
            else
            {
                MessageBox.Show("Error Code" + response.StatusCode + " : Message - " + response.ReasonPhrase);
            }
        }

        private void Txtphone_KeyDown(object sender, KeyEventArgs e)
        {

            TextBox textBox = sender as TextBox;
            MessageBox.Show(textBox.Name);
        }
    }
}
