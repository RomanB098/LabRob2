using XMLProcessor.ViewModels;

namespace XMLProcessor
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private async void InfoButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Info",
                "ООП Лабораторна робота №2\nВаріант 19 \"Кадри науковців (Зарплата)\"\nВиконав Буць Роман К24",
                "OK");
        }

        private async void ExitButton_Clicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Exit", "Are you sure you want to exit?", "Yes", "No");
            if (confirm)
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
        }

    }

}
