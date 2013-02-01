using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.IO;

// This save/load module uses the Newtonsoft Json library (http://james.newtonking.com/projects/json-net.aspx)
// to serialize and deserialize the SavingsAccount objects into Json text, and back into objects again.
// I seriously recommend everyone learn to use this library, because it's a heck of a lot easier and quicker 
// than trying to serialize anything into XML.

namespace Assignment1
{
    class SaveAndLoad
    {
        string filename;
        public SaveAndLoad(string file)
        {
            filename = file;
        }
        public void Save(List<SavingsAccount> listToSave){
            string output = JsonConvert.SerializeObject(listToSave);
            using (FileStream fs = File.Open(filename, FileMode.OpenOrCreate))
            {
                AddText(fs, output);
            }
        }

        // the follewing I/O section is based on some msdn tutorials I found for reading/writing 
        // data, particularly: http://msdn.microsoft.com/en-us/library/system.io.filestream.aspx

        public List<SavingsAccount> Load()
        {
            string openAccountListJson = null;
            List<SavingsAccount> newlist;
            using (FileStream fs = File.Open(filename, FileMode.Open))
            {
                
                byte[] b = new byte[1024]; // make a byte array
                UTF8Encoding temp = new UTF8Encoding(true); // make a UTF8 Encoding object
                while (fs.Read(b, 0, b.Length) > 0) // while the buffer created by the FileStream is bigger than 0
                                                    // i.e. there are still characters in the stream...
                {
                    openAccountListJson += temp.GetString(b); // add the byte array to the list string in UTF8 encoding
                }
            }
            newlist = JsonConvert.DeserializeObject<List<SavingsAccount>>(openAccountListJson);
            return newlist;
        }
        
        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

    }
}