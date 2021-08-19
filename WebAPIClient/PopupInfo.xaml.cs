using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebAPIClient
{
    /// <summary>
    /// Interaction logic for PopupInfo.xaml
    /// </summary>
    public partial class PopupInfo : Window
    {
        //day la contructor khi new 1 doi tuong se vao
        //truyen vao day
        public Action<Students> UpdateMainWindow { get; set; }
        
        //khoi tao class popupinfo voi tham so students
        public PopupInfo(Students info)
        {
            //truyen du lieu vao name
            
            InitializeComponent();
            this.txt_name.Text = info.name.ToString();
            this.txt_id.Text = info.id.ToString();
            this.txt_medium_score.Text = info.medium_score.ToString();
            this.txt_sex.Text = string.IsNullOrEmpty(info.sex) ? "" : info.sex.ToString();
            this.txt_phoneno.Text = string.IsNullOrEmpty(info.PhoneNo) ? "" : info.PhoneNo.ToString();
        }
        

      

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Students stu = new Students()
            {
                name = this.txt_name.Text,
                id = this.txt_id.Text,
                medium_score = Convert.ToInt32(this.txt_medium_score.Text),
                sex = this.txt_sex.Text,
                PhoneNo = this.txt_phoneno.Text,
                class_id = "",
            };
            this.UpdateMainWindow(stu);
        }

      

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            //tao validate regex khac 0-9
            Regex regex = new Regex("[^0-9]+");
            //kiem tra text input = value cua regex thi dung lai
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
