﻿using CAnyWhere.Models;
using CAnyWhere.Services;
using CAnyWhere.Validators;
using CAnyWhere.ViewModels;
using Firebase.Database;

namespace CAnyWhere.Views;

public partial class MainPage : ContentPage
{
    string username = "";
    string password = "";

	public MainPage()
	{
		InitializeComponent();
	}
  
    private async void OnLogin(object sender, EventArgs e)
    {
        bool isValid = await LoginViewModel.OnLogin(username, password);
        if (isValid)
        {
            LoginClientService LoginClientService = new();
            string key = username.Replace("@", "atTheRate").Replace(".", "dot").ToUpper();
            User user = await LoginClientService.GetAsync(key);
            if (user != null)
            {
                if(!user.EmailId.Equals(username) || !user.Password.Equals(password))
                {
                    return;
                }
            }
            new DashBoardDataViewModel();
            await Navigation.PushAsync(new HomeScreen());
        }
    }

    private async void OnSignUp(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CreateAccount());
    }

    void OnUserNameEntryTextChanged(object sender, TextChangedEventArgs e)
    {
         username = entryusername.Text;
    }

    void OnUserNameEntryCompleted(object sender, EventArgs e)
    {
        username = ((Entry)sender).Text;
    }

    void OnPasswordEntryTextChanged(object sender, TextChangedEventArgs e)
    {
        password = entrypassword.Text;
    }

    void OnPasswordEntryCompleted(object sender, EventArgs e)
    {
        password = ((Entry)sender).Text;
    }
}

