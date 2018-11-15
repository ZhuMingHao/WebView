using System;
using System.IO;
using System.Reflection;
using Xamarin.Forms;

namespace WebViewSample
{
    public partial class EvaluateJavaScriptPage : ContentPage
    {
        public EvaluateJavaScriptPage()
        {
            InitializeComponent();

            _webView.Source = LoadHTMLFileFromResource();
            _webView.Navigating += _webView_Navigating;
            _webView.Navigated += _webView_Navigated;
        }

        private void _webView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            
        }

        private void _webView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            
        }

        HtmlWebViewSource LoadHTMLFileFromResource()
        {
            var source = new HtmlWebViewSource();

            // Load the HTML file embedded as a resource in the .NET Standard library
            var assembly = typeof(EvaluateJavaScriptPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("WebViewSample.index.html");            
            using (var reader = new StreamReader(stream))
            {
                source.Html = reader.ReadToEnd();
            }
            return source;
        }

        async void OnCallJavaScriptButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_numberEntry.Text))
            {
                return;
            }

            int number = int.Parse(_numberEntry.Text);
            string result = await _webView.EvaluateJavaScriptAsync($"factorial({number})");
            _resultLabel.Text = $"Factorial of {number} is {result}.";
        }
    }
}
