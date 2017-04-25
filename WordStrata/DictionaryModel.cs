using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WordStrata
{
    public static class DictionaryModel
    {
        public static HashSet<string> GetDictionary()
        {
            var list = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            string path = "sowpods.txt";
            string line;
            System.IO.StreamReader file = null;

            try
            {
                file = new System.IO.StreamReader(path);
                while ((line = file.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            catch (Exception ex) when (ex is FileNotFoundException || ex is IOException)
            {
                string messageBoxText = "Problem reading dictionary file: " + path + ". Message: " + ex.Message;
                string caption = "File Error";
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBox.Show(messageBoxText, caption, button, icon);
            }
            finally
            {
                if (file != null)
                {
                    file.Close();
                }
            }
            return list;
        }
    }
}
