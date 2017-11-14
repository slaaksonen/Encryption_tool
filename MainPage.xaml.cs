using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace File_encryption
{
    /// <summary> 
    /// An empty page that can be used on its own or navigated to within a Frame. 
    /// </summary> 
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public async void Button_Click(object sender, RoutedEventArgs e)
        {
            ///We select which file will be crypted. 
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            string key = textBox2.Text;

            ///we create a list in which we save characters from text file after being crypted through caesar cipher. 
            List<char> list = new List<char>();
            foreach (char a in text)
            {
                char b = (Cipher(a, Convert.ToInt16(key)));
                list.Add(b);
            }

            ///we Create new crypted file 
            string FileName = textBox.Text;

            Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
            await storageFolder.CreateFileAsync(FileName,
            Windows.Storage.CreationCollisionOption.ReplaceExisting);

            for (int i = 0; i < list.Count; i++)
            {
                string muuttuja = Convert.ToString(list[i]);
                await Windows.Storage.FileIO.AppendTextAsync(sampleFile, Convert.ToString(muuttuja));
            }

        }

        public async void Button_Click2(object sender, RoutedEventArgs e)
        {
            ///We select which file will be crypted. 
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            openPicker.FileTypeFilter.Add(".txt");

            Windows.Storage.StorageFile file = await openPicker.PickSingleFileAsync();

            string text = await Windows.Storage.FileIO.ReadTextAsync(file);
            string key = textBox2.Text;

            ///we Create new crypted file 
            string FileName = textBox.Text;

            Windows.Storage.StorageFolder storageFolder =
            Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile =
            await storageFolder.CreateFileAsync(FileName,
            Windows.Storage.CreationCollisionOption.ReplaceExisting);

            string muuttuja = Encrypt(text, Convert.ToInt16(key));
            await Windows.Storage.FileIO.AppendTextAsync(sampleFile, Convert.ToString(muuttuja));

        }

        /// Caesar 
        private static char Cipher(char ch, int key)
        {
            if (!char.IsLetter(ch))
                return ch;

            char offset = char.IsUpper(ch) ? 'A' : 'a';
            return (char)((((ch + key) - offset) % 26) + offset);
        }

        public static string Encipher(string input, int key)
        {
            string output = string.Empty;

            foreach (char ch in input)
                output += Cipher(ch, key);

            return output;
        }

        /// XOR

        public static string Encrypt(string etext, int ekey)

        {
            StringBuilder inA = new StringBuilder(etext);
            StringBuilder outA = new StringBuilder(etext.Length);
            char c;
            for (int i = 0; i < etext.Length; i++)
            {
                c = inA[i];

                c = (char)(c ^ ekey);

                outA.Append(c);
            }
           return outA.ToString();
        }
    }
}

