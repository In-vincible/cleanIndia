﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Data.Json;
using System.Runtime.Serialization;
using System.Net.Http;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Globalization;
using System.Text.RegularExpressions;
using Windows.UI.Xaml.Media.Imaging;

using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.Storage.Streams;
using System.Collections.ObjectModel;

namespace cleanIndia
{
    sealed partial class Registration : Page
    {
         
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Name = uname.Text;
            string Email = email.Text;
            string Number = phone.Text;
            string pass = password.Password;
            RegisterData holder = new RegisterData(Name, Email, Number, pass);
            string url = "http://localhost:8000/api/registration/";
            
            //register(url, holder);
            //test.Text = (App.Current as App).GName;
            //this.Frame(typeof(Registration));
            //Class1.PostRequestaa("http://technexuser.herokuapp.com/api/register/");
        }



        private async static Task<RegisterData> register(string url, RegisterData data)
        {
            IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email",data.email),
                new KeyValuePair<string,string>("name",data.name),
                new KeyValuePair<string,string>("password",data.password),
                new KeyValuePair<string,string>("mobileNumber",data.phoneNumber)
                
            };
            HttpContent q = new FormUrlEncodedContent(emails);
            
            using (HttpClient client = new HttpClient())
            {
                //client.GetAsync()
                // HttpResponseMessage response = client.GetAsync("http://www.google.co.in");
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "x-www-form-urlencoded");
                using (HttpResponseMessage response = await client.PostAsync(url, q))
                {
                    using (HttpContent content = response.Content)
                    {
                        string mycontent = await content.ReadAsStringAsync();
                        HttpContentHeaders heaPOSTders = content.Headers;
                        //JsonValue data = JsonValue.Parse(mycontent);
                        //JsonObject result = data as JsonObject;
                        //JsonObject result = data as JsonObject;
                        JsonObject dataJson = JsonObject.Parse(mycontent);
                        Debug.WriteLine(dataJson);
                        double Status = dataJson.GetNamedNumber("status");
                        string Name = dataJson.GetNamedString("name");
                        string Email = dataJson.GetNamedString("email");
                        string MobileNumber = dataJson.GetNamedString("mobileNumber");
                        Debug.WriteLine(Name);
                        (App.Current as App).GName = Name;
                        Debug.WriteLine((App.Current as App).GName);
                        var d = new MessageDialog((App.Current as App).GName);
                        await d.ShowAsync();
                        RegisterData g = new RegisterData(Name, Email, MobileNumber, "shat");
                        return g;
                        //Debug.WriteLine(g);
                        //Debug.WriteLine(mycontent);
                    }


                }
               
            }
            
            
        }

        public static async Task<string> Upload(byte[] image)
        {
            Debug.WriteLine("code base 1");
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "multipart/form-data");
                using (var content =
                    new MultipartFormDataContent())//"Upload----" + DateTime.Now.ToString(CultureInfo.InvariantCulture)))
                {
                    content.Add(new StreamContent(new MemoryStream(image)), "bilddatei", "upload.jpg");
                    
           IEnumerable<KeyValuePair<string, string>> emails = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string,string>("email","b@c.com"),
                new KeyValuePair<string,string>("longitude","12"),
                new KeyValuePair<string,string>("latitude","12"),
                new KeyValuePair<string,string>("title","whatever"),
                
            };
           HttpContent q = new FormUrlEncodedContent(emails);
           content.Add(q,"data");
           Debug.WriteLine("code base 2");
                    using (
                       var message =
                           await client.PostAsync("http://localhost:8000/api/photoComplaint/", content))
                    {
                        string input = await message.Content.ReadAsStringAsync();
                        JsonObject dataJson = JsonObject.Parse(input);
                        Debug.WriteLine(dataJson.GetNamedString("status"));
                        Debug.WriteLine(input);
                        return !string.IsNullOrWhiteSpace(input) ? Regex.Match(input, @"http://\w*\.directupload\.net/images/\d*/\w*\.[a-z]{3}").Value : null;
                    }
                }
            }
        }

        
    }
}

