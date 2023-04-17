using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Newtonsoft.Json;
using NMEA.Wpf.Extensions;
using NMEA.Wpf.Models;

namespace NMEA.Wpf.Services
{
    public interface ISettingsService
    {
        ProgramSettings Settings { get; set; }
        void Read();
        void Save();
    }

    public class SettingsService : ISettingsService
    {
        private static readonly String _path = PrismManager.ConfigPath;
        private static ProgramSettings _settings;
        public ProgramSettings Settings { get => _settings; set => _settings = value; }

        public void Read()
        {
            if (File.Exists(_path))
            {
                String json = File.ReadAllText(_path);
                _settings = JsonConvert.DeserializeObject<ProgramSettings>(json);
            }
            else
            {
                _settings = new ProgramSettings();
                Save();
            }
        }

        public void Save()
        {
            if (_settings == null) return;
            File.WriteAllText(_path, JsonConvert.SerializeObject(_settings, Formatting.Indented));
        }
    }
}
