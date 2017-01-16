using System;
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
using Windows.Devices.Geolocation;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Media.Animation;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Media.MediaProperties;
using Windows.UI.Xaml.Media.Imaging;
using Windows.ApplicationModel.Core;
using Windows.Storage.Pickers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace cleanIndia
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //this.InitializeCamera();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create FileOpenPicker instance    
            FileOpenPicker fileOpenPicker = new FileOpenPicker();

            // Set SuggestedStartLocation    
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            // Set ViewMode    
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter for file types. For example, if you want to open text files,  
            // you will add .txt to the list.  

            fileOpenPicker.FileTypeFilter.Clear();
            fileOpenPicker.FileTypeFilter.Add(".png");
            fileOpenPicker.FileTypeFilter.Add(".jpeg");
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.FileTypeFilter.Add(".bmp");

            // Open FileOpenPicker    
            StorageFile file = await fileOpenPicker.PickSingleFileAsync();
            if (file != null)
            {
                FileNameTextBlock.Text = file.Name;
                // Open a stream    
                Windows.Storage.Streams.IRandomAccessStream fileStream =
                await file.OpenAsync(FileAccessMode.Read);

                // Create a BitmapImage and Set stream as source    
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.SetSource(fileStream);

                // Set BitmapImage Source    
                img.Source = bitmapImage;
                
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            byte[] hog = new byte[500];
            Registration.Upload(hog);

        }
       
       
       // StorageFile media = null;
        //MediaCapture cameraCapture;


/* Capture code for future purposes      
 * 
 * private async void InitializeCamera()
        {
            cameraCapture = new MediaCapture();
            await cameraCapture.InitializeAsync();

            //capturePreview.Source = cameraCapture;
            //capturePreview.Source = cameraCapture;
            await cameraCapture.StartPreviewAsync();
        }
        */
        /*
        private async void OnTakePhotoClick(object sender, RoutedEventArgs e)
        {
            // Capture a new photo or video from the device.
            //http://msdn.microsoft.com/library/windows/apps/br241124
            //http://azure.microsoft.com/en-us/documentation/articles/mobile-services-windows-store-dotnet-upload-data-blob-storage/

            ImageEncodingProperties imgFormat = ImageEncodingProperties.CreateJpeg();

            // create storage file in local app storage
            media = await ApplicationData.Current.LocalFolder.CreateFileAsync(
                "TestPhoto.jpg", CreationCollisionOption.GenerateUniqueName);

            //CameraCaptureUI not available in universal Windows app
            //http://stackoverflow.com/questions/26392977/no-basic-camera-ui-for-windows-phone-8-1
            //CameraCaptureUI cameraCapture = new CameraCaptureUI();
            //media = await cameraCapture.CaptureFileAsync(CameraCaptureUIMode.PhotoOrVideo);


            await cameraCapture.CapturePhotoToStorageFileAsync(imgFormat, media);

            // Get photo as a BitmapImage
            BitmapImage bmpImage = new BitmapImage(new Uri(media.Path));

            // imagePreivew is a <Image> object defined in XAML
            imagePreivew.Source = bmpImage;

        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            
        }*/
    }
    
}
