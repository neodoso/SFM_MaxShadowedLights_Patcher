using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SFM_MaxShadowedLights_Patcher.Functions
{
    internal class BinaryPatcher
    {
        /// <summary>
        /// searches hex code "hex_code" in "filepath" file and replaces hex code by input "replacement_hex_code"
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="hex_code"></param>
        /// <param name="replacement_hex_code"></param>
        /// <returns></returns>
        public bool search_and_replace_HexCode(string filepath, string[] hex_code)
        {
            int index = brSearch(filepath, hex_code[0]);
            // if hex code pattern not found in file
            if(index == -1)
            {
                MessageBox.Show("Error: could not recognize the file to patch. (" + Path.GetFileName(filepath) +")" + "\n\n" + filepath + 
                                 "\n\nThe file data and version might have changed since the patcher tool was created and it cannot recognize to know what to patch. " +
                                   "\nPlease contact the developer of the patcher so it can be updated to support the new version(s) of the unrecognized file(s).");
                return false;
            }

            return brWriteAt(filepath, index, hex_code[1]);
        }

        /// <summary>
        /// Searches a list of hex code bytes in binary stream
        /// return the offset
        /// </summary>
        /// <param name="filepath">filepath to binary file in which we do the hex code search</param>
        /// <param name="search_Hex">input hex code in string format</param>
        /// <returns></returns>
        private int brSearch(string filepath, string search_Hex)
        {
            Stream fs = File.OpenRead(filepath);
            BinaryReader br = new BinaryReader(fs);

            byte[] search_pattern = HexString_ToBytes.ToBytes(search_Hex);

            // from stream positon 0 to stream length - search_pattern length
            for (int i = 0; i < fs.Length - search_pattern.Length; i++)
            {
                fs.Position = i; // set stream position

                // read x bytes (where x = number of bytes to read (search_Bytes.Length) )
                // and compare the followng bytes of the stream to "search_Bytes"
                byte[] test = br.ReadBytes(search_pattern.Length);
                bool pattern_matches = true;

                // compare each byte of byte array test VS search_pattern
                for (int x = 0; x < search_pattern.Length; x++)
                {
                    if(test[x] != search_pattern[x])
                    {
                        pattern_matches = false;
                        break;
                    }
                }
                // if byte(hex) pattern found
                if (pattern_matches)
                {
                    fs.Close();
                    br.Close();
                    
                    // found hex pattern, return i + size of search pattern (since we're patching right after the search pattern)
                    return i + search_pattern.Length;
                }
            }

            fs.Close();
            br.Close();

            // not found, return -1
            return -1;
        }

        /// <summary>
        /// Writes "hex_code" at "position" in file "filepath"
        /// </summary>
        /// <param name="filepath">input binary file to modify</param>
        /// <param name="position">offset of the file where writing begins</param>
        /// <param name="hex_code">string hexadecimal code that will be appended to file at "position"</param>
        /// <returns></returns>
        private bool brWriteAt(string filepath, int position, string hex_code)
        {
            try
            {
                FileStream fs = new FileStream(filepath, FileMode.Open);
                BinaryWriter br = new BinaryWriter(fs);
                // br.Seek(position, SeekOrigin.Begin);
                br.BaseStream.Position = position;
                br.Write(HexString_ToBytes.ToBytes(hex_code));
                fs.Close();
                br.Close();

                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message + "\n\n" + "Could not write to file: " + Path.GetFileName(filepath) + "\n\n" + filepath);
                return false;
            }
        }
    }
}
