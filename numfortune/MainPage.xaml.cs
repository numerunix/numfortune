﻿using System.Collections.ObjectModel;

namespace numfortune;

public partial class MainPage : ContentPage
{
	HttpClient client;
    HttpResponseMessage httpResponse;
    public MainPage()
	{
		InitializeComponent();
		client=new HttpClient();
        tick();
    }

	private async void tick()
	{
        try
        {
            httpResponse = await client.GetAsync("https://api.justyy.workers.dev/api/fortune");
        } catch (Exception ex)
        {
            lblFortune.Text = ex.Message;
            return;
        }

        if (httpResponse.IsSuccessStatusCode)
        {
            String s = await httpResponse.Content.ReadAsStringAsync();
            s = s.Substring(1, s.Length - 2);
            s = s.Replace("\\n", System.Environment.NewLine);
            s = s.Replace("\\t", "	");
            s = s.Replace("\\\"", "\"");
            lblFortune.Text = s;
        } else
        {
            lblFortune.Text = $"The HTTP status code is ${httpResponse.StatusCode}";
        }

    }

    private void OnRefresh_Click(object sender, EventArgs e)
	{
        tick();
    }
}

