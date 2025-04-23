using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Tubes_Kelompok_BisaYukk.Modules
{
    internal class FileHandler
    {
        private static string filePath = "data.json";

        public static void SimpanKeFile(Dictionary<string, List<string>> data)
        {
            try
            {
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal menyimpan data: {ex.Message}");
            }
        }

        public static Dictionary<string, List<string>> MuatDariFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<Dictionary<string, List<string>>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gagal membaca data: {ex.Message}");
            }

            return new Dictionary<string, List<string>>(); // Jika gagal, kembalikan dictionary kosong
        }
    }
}
