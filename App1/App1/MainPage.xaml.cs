using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App1.Service;
using App1.Model;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        private User users;
        ApiLaravel api;
        public MainPage()
        {
            InitializeComponent();
            api = new ApiLaravel();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //localiza um registro
            try
            {
                users = await api.GetHighScore();
                if (users.id > 0)
                {                    
                    entName.Text = users.name;
                    btSalvar.Text = "Atualizar";
                }
                else btSalvar.Text = "Cadastrar";
            }
            catch (Exception error)
            {
                await DisplayAlert("Erro", error.Message, "OK");
            }

        }
    }
}
