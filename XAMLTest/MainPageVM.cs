using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace XAMLTest
{

    public class MainPageVM : INotifyPropertyChanged
    {

        public MainPageVM() // พิมพ์ ctor แล้วกด Tab สองครั้ง จะเป็นการสร้าง Constructor ให้อัตโนมัติ
        {

            // กำหนด event handler ด้วย anonymous function (function signature จะต้องตรงกันกับ event handler deligate)
            Notes.CollectionChanged += (s, e) =>
            {
                if (Employees.Count < 3)
                {
                    Employees.Add(new Employee { Name = s.ToString(), Surname = e.ToString() });
                }
                else
                {
                    Employees.Clear();
                }
            };

            SaveCommand = new Command(() => {
                Notes.Add(note);
                Note = string.Empty;
            });
        }

        public ObservableCollection<string> Notes { get; set; } = new ObservableCollection<string>();

        //Object initializer ด้วย employees 2 รายการ
        public ObservableCollection<Employee> Employees { get; set; } = new ObservableCollection<Employee>()
        {
            new Employee { Name = "Nipont", Surname = "S." },
            new Employee {Name="John", Surname="Doe"}
        };

        //ประกาศอีเว้นท์ เพื่อให้ออบเจ็คอื่นมา Subscribe
        public event PropertyChangedEventHandler PropertyChanged;

        //สร้างเมธอดขึ้นมา สำหรับการ Notify อีเว้นท์ ที่อาร์กิวเมนต์ propertyName มีการกำกับด้วยแอตทริบิวต์ [CallerMemberName] ทำให้มีการเติม propertyName ให้โดยอัตโนมัติได้
        void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            //เรียกเมธอดที่เป็น EventHandler ของออบเจ็คที่ได้ Subscribe มาทำงาน
            //จริงๆ เขียนเป็น PropertyChanged(this, new PropertyChangedEventArgs(name)); ก็ได้ถ้ามั่นใจว่า PropertyChanged ไม่เป็นค่าว่าง
            PropertyChanged?.Invoke(this /*sender*/, new PropertyChangedEventArgs(propertyName) /*EventArgs*/);
        }


        string note = string.Empty; // ถ้าไม่กำหนด Access Modifier ของฟิลด์จะถือว่าเป็น private โดยอัตโนมัติ
        public string Note
        {
            get => note;
            set
            {
                if (note != value)
                {
                    note = value;
                    //RaisePropertyChanged(nameof(Note));
                    // สามารถละอาร์กิวเมนต์ propertyName ได้เนื่องจากมีการกำหนด [CallerMemberName] ไว้ที่นิยามของอาร์กิวเมนต์แล้ว
                    RaisePropertyChanged();

                    //OnPropertyChanged(nameof(DisplayNote)); //เรียกตรงๆ แบบนี้ก็ได้ แต่ไม่เป็นที่นิยม
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DisplayNote)));
                }
            }
        }

        public string DisplayNote => $"Note entered: {note}"; // บรรทัดนี้เป็นฟังก์ชั่น ที่เขียนในรูปแบบ Lambda Expression

        public string fieldNote = "This is field"; // บรรทัดนี้เป็นฟิลด์

        public Command SaveCommand { get; set; }
    }
}
