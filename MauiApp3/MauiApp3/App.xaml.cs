﻿using Microsoft.Maui;
using Microsoft.Maui.Controls;
using MauiApp3.Views;

namespace MauiApp3
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}