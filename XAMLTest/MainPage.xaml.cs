using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace XAMLTest
{
    //[Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public string internalField = "Internal Field";

        public string InternalProperty { get; set; }

        public MainPage()
        {
            InternalProperty = "Before Instantiate";
            InitializeComponent();
            InternalProperty = "After Instantiate";
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DirectValueDataContext());
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MultiSourceBinding());
        }
    }


}
